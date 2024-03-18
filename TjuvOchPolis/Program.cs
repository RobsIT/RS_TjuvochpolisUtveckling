using System;
using System.ComponentModel.Design;
using System.Drawing;
using TjuvOchPolis;
using static TjuvOchPolis.Draw;

namespace Tjuv_Polis_MinUtveckling26Okt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> personsList = new List<Person>();
            List<string> latestEvents = new List<string>();
            //Mått för staden, fägelsett och fattighuset.
            int cityWidth = 100;
            int cityHeight = 25;
            int prisonPosX = cityWidth + 3;
            int prisonPosY = 1;
            int prisonWidth = 15;
            int prisonHeight = 10;
            int poorHousePosX = cityWidth + 3;
            int poorHousePosY = 15;
            int poorHouseWidth = 40; 
            int poorHouseHeight = 10;

            int policeNum = 10;
            int citizenNum = 30;
            int thiefNum = 20;
            int numOfRobberies = 0;
            int thivesInPrison = 0;
            int citizensInPoorHouse = 0;
            int inventoryRollDelay = 0;
            int inventoryStepCount = 0;
            int eventCounterNr = 1;
            bool autoScroll = true;
            Random random = new Random();
            Console.CursorVisible = false;
            //Gör personer.
            int citizenNameNr = 0;
            int thiefNameNr = 0;
            int policeNameNr = 0;
            for (int i = 0; i < policeNum; i++)
            {
                CreatePerson.CreatePolice(personsList, random, cityWidth, cityHeight, ref policeNameNr);
            }
            for (int i = 0; i < citizenNum; i++)
            {
                CreatePerson.CreateCitizens(personsList, random, cityWidth, cityHeight, ref citizenNameNr);
            }
            for (int i = 0; i < thiefNum; i++)
            {
                CreatePerson.CreateThieves(personsList, random, cityWidth, cityHeight, ref thiefNameNr);
            }
            while (true)
            {
                int totalPeople = policeNum + citizenNum + thiefNum;
                int[] x_Positions = new int[totalPeople];
                int[] y_Positions = new int[totalPeople];
                for (int i = 0; i < totalPeople; i++)
                {
                    x_Positions[i] = personsList[i].X_coord;
                    y_Positions[i] = personsList[i].Y_coord;
                }
                for (int i = 0; i < 5; i++) //Antal gubbar som får ny riktning denna sekvens.
                {
                    personsList[random.Next(totalPeople)].Direction = random.Next(8);
                    personsList[random.Next(totalPeople)].Direction = random.Next(8);
                }
                //Skapa nya karaktärer och styrning av Inventory.
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    char key = char.ToUpper(keyInfo.KeyChar);
                    switch (key)
                    {
                        case 'C':
                            citizenNum++;
                            CreatePerson.CreateCitizens(personsList, random, cityWidth, cityHeight, ref citizenNameNr);
                            break;
                        case 'T':
                            CreatePerson.CreateThieves(personsList, random, cityWidth, cityHeight, ref thiefNameNr);
                            thiefNum++;
                            break;
                        case 'P':
                            CreatePerson.CreatePolice(personsList, random, cityWidth, cityHeight, ref policeNameNr);
                            policeNum++;
                            break;
                        case 'A':
                            if (autoScroll == true) { autoScroll = false; }
                            else if (autoScroll == false) { autoScroll = true; }
                            break;
                        case 'W'://Scrorlla Inventory uppåt.
                            if (autoScroll == true) { autoScroll = false; }
                            if (inventoryStepCount > 0) { inventoryStepCount = inventoryStepCount - 2; }
                            else if (inventoryStepCount == 0) { inventoryStepCount = totalPeople - 1; }
                            break;
                        case 'S'://Scrorlla Inventory nedåt.
                            if (autoScroll == true) { autoScroll = false; }
                            if (inventoryStepCount < totalPeople - 1) { inventoryStepCount = inventoryStepCount + 2; }
                            else if (inventoryStepCount == totalPeople - 1) { inventoryStepCount = 0; }
                            break;
                    }
                }

                //Inventory listan.
                Console.SetCursorPosition(0, cityHeight + 2);
                Console.WriteLine("upp[W] ned[S] auto[A]  INVENTORY: ");
                Console.SetCursorPosition(0, cityHeight + 3);
                int rollListLength = 12;
                if (inventoryStepCount >= totalPeople || inventoryStepCount < 0) { inventoryStepCount = 0; }
                int inventoryCount = inventoryStepCount;
                for (int i = 0; i < rollListLength; i++)
                {
                    Console.Write($"{personsList[inventoryCount].Name}: ");
                    foreach (string item in personsList[inventoryCount].Inventory)
                    {
                        Console.Write(" " + item);
                    }
                    inventoryCount++;
                    if (inventoryCount >= totalPeople) { inventoryCount = 0; }
                    Console.WriteLine();
                }
                inventoryCount = inventoryStepCount;
                if (autoScroll == true)
                {
                    if (inventoryRollDelay < 4) { inventoryRollDelay++; }
                    if (inventoryRollDelay == 3)
                    {
                        inventoryStepCount++; inventoryRollDelay = 0;
                    }
                }
                //Skriv ut de senaste händelserna, och vänder på listan.
                Console.SetCursorPosition(cityWidth - 15, cityHeight + 2);
                Console.WriteLine("SENASTE HÄNDELSER:");
                if (latestEvents.Count > 12)
                {
                    latestEvents.RemoveAt(0);
                }
                for (int i = 0; i < 12; i++)
                {
                    int index = latestEvents.Count - 1 - i;
                    if (index >= 0)
                    {
                        Console.SetCursorPosition(cityWidth - 15, cityHeight + 3 + i);
                        Console.WriteLine($"{latestEvents[index]}");
                    }
                }
               
                //Ritar upp allt.
                Draw.DrawStatistics(prisonWidth, numOfRobberies, thivesInPrison, citizensInPoorHouse, citizenNum, thiefNum, policeNum);
                Draw.DrawCity(cityWidth, cityHeight);
                Draw.DrawPrison(prisonPosX, prisonPosY, prisonWidth, prisonHeight);
                Draw.DrawPoorHouse(poorHousePosX, poorHousePosY, poorHouseWidth, poorHouseHeight);
                Thread.Sleep(400);
                Console.Clear();

                for (int i = 0; i < totalPeople; i++)
                {
                    int direction = personsList[i].Direction;
                    Person person = personsList[i];

                    int originalForegroundColor = (int)Console.ForegroundColor;
                    if (person is Police) { Console.ForegroundColor = ConsoleColor.Blue; }
                    else if (person is Citizen) { Console.ForegroundColor = ConsoleColor.Green; }
                    else if (person is Thief) { Console.ForegroundColor = ConsoleColor.Red; }
                    //Uppdaterar positioner till alla personer i staden, fängelset och fattighuset.
                    //Gör så att personerna kommer ut i andra änden av spelytorna.
                    Helper.UpdatePosition(ref x_Positions[i], ref y_Positions[i], direction);
                    if (personsList[i].PoorHouseInmate == true)
                    {
                        if (y_Positions[i] > poorHousePosY + (poorHouseHeight - 1)) { y_Positions[i] = poorHousePosY + 1; }
                        else if (y_Positions[i] < poorHousePosY + 1) { y_Positions[i] = poorHousePosY + (poorHouseHeight - 1); }
                        if (x_Positions[i] > poorHousePosX + (poorHouseWidth - 1)) { x_Positions[i] = poorHousePosX + 1; }
                        else if (x_Positions[i] < poorHousePosX + 1) { x_Positions[i] = poorHousePosX + (poorHouseWidth - 1); }
                    }
                    else if (personsList[i].PrisonInmate == true)
                    {
                        if (y_Positions[i] > prisonPosY + (prisonHeight - 1)) { y_Positions[i] = prisonPosY + 1; }
                        else if (y_Positions[i] < prisonPosY + 1) { y_Positions[i] = prisonPosY + (prisonHeight - 1); }
                        if (x_Positions[i] > prisonPosX + (prisonWidth - 1)) { x_Positions[i] = prisonPosX + 1; }
                        else if (x_Positions[i] < prisonPosX + 1) { x_Positions[i] = prisonPosX + (prisonWidth - 1); }
                    }
                    else
                    {
                        if (y_Positions[i] > cityHeight) { y_Positions[i] = 1; }
                        else if (y_Positions[i] < 1) { y_Positions[i] = cityHeight; }
                        if (x_Positions[i] > cityWidth) { x_Positions[i] = 1; }
                        else if (x_Positions[i] < 1) { x_Positions[i] = cityWidth; }
                    }
                    Console.SetCursorPosition(x_Positions[i], y_Positions[i]);
                    Console.Write(personsList[i].Symbol);
                    Console.ForegroundColor = (ConsoleColor)originalForegroundColor;
                }
                //Kontrollera om flera gubbar befinner sig på samma position.
                for (int i = 0; i < totalPeople; i++)
                {
                    for (int j = i + 1; j < totalPeople; j++)
                    {
                        if (x_Positions[i] == x_Positions[j] && y_Positions[i] == y_Positions[j])
                        {
                            var meet_1 = personsList[i];
                            var meet_2 = personsList[j];
                            if (meet_1 is Police && meet_2 is Thief)
                            {
                                Police police = (Police)meet_1;
                                police.CatchThief(meet_1, meet_2, personsList, latestEvents, y_Positions, x_Positions, ref thivesInPrison, j, i, ref eventCounterNr);
                            }
                            else if (meet_1 is Citizen && meet_2 is Thief)
                            {
                                Thief thief = (Thief)meet_2;
                                thief.Steal(meet_1, meet_2, latestEvents, ref numOfRobberies, ref eventCounterNr);
                            }
                            else if (meet_1 is Police && meet_2 is Citizen)
                            {
                                Police police = (Police)meet_1;
                                police.AdmitToPoorHouse(meet_1, meet_2, latestEvents, ref citizensInPoorHouse, ref eventCounterNr);
                            }
                        }
                    }
                }
                for (int i = 0; i < totalPeople; i++)//Sparar dom nya positionerna i Arrayerna.
                {
                    personsList[i].X_coord = x_Positions[i];
                    personsList[i].Y_coord = y_Positions[i];
                }
            }
        }
    }
}