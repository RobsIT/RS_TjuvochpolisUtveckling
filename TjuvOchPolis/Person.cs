using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuv_Polis_MinUtveckling26Okt
{
    class Person
    {
        public string Name { get; set; }
        public int X_coord { get; set; }
        public int Y_coord { get; set; }
        public List<string> Inventory { get; set; }
        public char Symbol { get; set; }
        public bool PrisonInmate { get; set; }
        public bool PoorHouseInmate { get; set; }
        public int Direction { get; set; }
        public Person(string name, char symbol, int direction)
        {
            Name = name;
            Inventory = new List<string>();
            Symbol = symbol;
            Direction = direction;
        }
    }
    class Police : Person
    {
        public Police(string name, int x_Coord, int y_Coord, char symbol, int direction) : base(name, symbol, direction)
        {
            X_coord = x_Coord;
            Y_coord = y_Coord;
            Direction = direction;
        }
        public void CatchThief(Person meet_1, Person meet_2, List<Person> personsList, List<string> latestEvents, int[] y_Positions, int[] x_Positions, ref int thivesInPrison, int j, int i, ref int eventCounterNr)
        {
            if (meet_1 is Police && meet_2 is Thief)
            {
                if (meet_2.Inventory.Count > 0)
                {
                    string eventDescription = $"{eventCounterNr}. {meet_1.Name} har fångat {meet_2.Name}!";
                    latestEvents.Add(eventDescription);
                    eventCounterNr++;
                    personsList[j].PrisonInmate = true;
                    y_Positions[i] = 3;
                    x_Positions[j] = 106;
                    thivesInPrison++;
                    foreach (string stolenItem in meet_2.Inventory)
                    {
                        if (meet_1.Inventory.Count < 8) 
                        {
                            Inventory.Add(stolenItem);
                        }
                    }
                    meet_2.Inventory.Clear();
                    meet_2.PrisonInmate = true;
                    meet_2.Inventory.Add("FÄNGELSET");
                }
            }
        }
        public void AdmitToPoorHouse(Person meet_1, Person meet_2, List<string> latestEvents, ref int citizenInPoorHouse, ref int eventCounterNr)
        {
            if (meet_2.Inventory.Count == 0)
            {
                string eventDescription = $"{eventCounterNr}. {meet_1.Name} Kastar {meet_2.Name} i fattighuset!";
                latestEvents.Add(eventDescription);
                eventCounterNr++;
                meet_2.PoorHouseInmate = true;
                citizenInPoorHouse++;
                meet_2.Inventory.Add("FATTIGHUSET");
            }
        }
    }
    class Citizen : Person
    {
        public Citizen(string name, int x_Coord, int y_Coord, char symbol, int direction, bool poorHouseInmate) : base(name, symbol, direction)
        {
            X_coord = x_Coord;
            Y_coord = y_Coord;
            Direction = direction;
            PoorHouseInmate = poorHouseInmate;
        }
    }
    class Thief : Person
    {
        public Thief(string name, int x_Coord, int y_Coord, char symbol, bool prisonInmate, int direction) : base(name, symbol, direction)
        {
            X_coord = x_Coord;
            Y_coord = y_Coord;
            PrisonInmate = prisonInmate;
            Direction = direction;
        }
        public void Steal(Person meet_1, Person meet_2, List<string> latestEvents, ref int numOfRobberies, ref int eventCounterNr)
        {
            Citizen citizen = (Citizen)meet_1;
            if (citizen.Inventory.Count > 0)
            {
                string eventDescription = $"{eventCounterNr}. {meet_2.Name} har rånat {meet_1.Name}!";
                latestEvents.Add(eventDescription);
                eventCounterNr++;
                numOfRobberies++;
                int Item = new Random().Next(citizen.Inventory.Count);
                string stolenItem = citizen.Inventory[Item];
                if (meet_2.Inventory.Count < 8)
                {
                    Inventory.Add(stolenItem);
                }
                citizen.Inventory.RemoveAt(Item);
            }
        }
    }
}
