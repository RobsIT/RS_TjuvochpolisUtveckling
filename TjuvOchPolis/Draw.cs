using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjuv_Polis_MinUtveckling26Okt;

namespace TjuvOchPolis
{
    internal class Draw
    {
        public static void DrawCity(int width, int height)
        {
            for (int i = 0; i <= width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
                Console.SetCursorPosition(i, height + 1);
                Console.Write("-");
            }
            for (int i = 0; i <= height + 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(width + 1, i);
                Console.Write("|");
            }
        }
        public static void DrawPrison(int prisonX, int prisonY, int prisonWidth, int prisonHeight)
        {
            Console.WriteLine("");
            Console.SetCursorPosition(prisonX + 3, 0);
            Console.Write("FÄNGELSET");
            for (int i = 0; i <= prisonWidth; i++)
            {
                Console.SetCursorPosition(prisonX + i, prisonY);
                Console.Write("-");
                Console.SetCursorPosition(prisonX + i, prisonY + prisonHeight);
                Console.Write("-");
            }
            for (int i = 0; i <= prisonHeight; i++)
            {
                Console.SetCursorPosition(prisonX, prisonY + i);
                Console.Write("|");
                Console.SetCursorPosition(prisonX + prisonWidth, prisonY + i);
                Console.Write("|");
            }
        }
        public static void DrawPoorHouse(int poorHouseX, int poorHouseY, int poorHouseWidth, int poorHouseHeight)
        {
            Console.WriteLine("");
            Console.SetCursorPosition(poorHouseX + 3, poorHouseY - 1);
            Console.Write("FATTIGHUSET");
            for (int i = 0; i <= poorHouseWidth; i++)
            {
                Console.SetCursorPosition(poorHouseX + i, poorHouseY);
                Console.Write("-");
                Console.SetCursorPosition(poorHouseX + i, poorHouseY + poorHouseHeight);
                Console.Write("-");
            }
            for (int i = 0; i <= poorHouseHeight; i++)
            {
                Console.SetCursorPosition(poorHouseX, poorHouseY + i);
                Console.Write("|");
                Console.SetCursorPosition(poorHouseX + poorHouseWidth, poorHouseY + i);
                Console.Write("|");
            }
        }
        public static void DrawStatistics(int prisonWidth, int numOfRobberies, int thivesInPrison, int citizensInPoorHouse, int citizenNum, int thiefNum, int policeNum)
        {
            Console.SetCursorPosition(103 + (prisonWidth + 5), 1);
            Console.WriteLine($"Rån: {numOfRobberies}");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 2);
            Console.WriteLine($"Fängelset: {thivesInPrison}");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 3);
            Console.WriteLine($"Fattighuset: {citizensInPoorHouse}");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 4);
            Console.WriteLine($"Medborgare: {citizenNum}   [C]++");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 5);
            Console.WriteLine($"Tjuvar: {thiefNum}       [T]++");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 6);
            Console.WriteLine($"Poliser: {policeNum}      [P]++");
        }
        public class CreatePerson
        {
            public static readonly string[] PoliceNames = {
                "Polisen Svensson", "Polisen Johnsson", "Polisen Davidsson", "Polisen Willhelmsson", "Polisen Andersson",
                "Polisen Martinsson", "Polisen Börjesson", "Polisen Göransson", "Polisen Carlberg", "Polisen Nilsson",
                "Polisen Falk", "Polisen Johnson", "Polisen Eriksen", "Polisen Olofsson", "Polisen Lindberg",
                "Polisen Henriksson", "Polisen Andersson", "Polisen Mårtensson", "Polisen Bergqvist", "Polisen Magnusson",
                "Polisen Larsson", "Polisen Persson", "Polisen Eriksson", "Polisen Karlsson", "Polisen Johansson",
                "Polisen Bergström", "Polisen Gustafsson", "Polisen Lundqvist", "Polisen Nyström", "Polisen Holm",
                "Polisen Ahlström", "Polisen Larsson", "Polisen Sjöberg", "Polisen Andersson", "Polisen Gustavsson",
                "Polisen Wallin", "Polisen Karlberg", "Polisen Bergman", "Polisen Lindström", "Polisen Persson", "Polisen Sandberg"
            };
            private static readonly string[] CitizenNames = {
                "Medborgare Simonsson", "Medborgare Karlsson", "Medborgare Jonsson", "Medborgare Davidsson", "Medborgare Williamsson",
                "Medborgare Andersson", "Medborgare Martinsson", "Medborgare Börjesson", "Medborgare Klarksson", "Medborgare Andersson",
                "Medborgare Jenssen", "Medborgare Grenborg", "Medborgare Klausson", "Medborgare Waldemarsson", "Medborgare Tunberg",
                "Medborgare Mauritz", "Medborgare Hallberg", "Medborgare Larsson", "Medborgare Garcia", "Medborgare Rodriguez",
                "Medborgare Scottsson", "Medborgare Ljungberg", "Medborgare Klingberg", "Medborgare Wright", "Medborgare Adamsson",
                "Medborgare Klausson", "Medborgare Grenberg", "Medborgare Redström", "Medborgare Cartelberg", "Medborgare Hallström",
                "Medborgare Jakobsson", "Medborgare Bergqvist", "Medborgare Lindberg", "Medborgare Persson", "Medborgare Nyström",
                "Medborgare Svensson", "Medborgare Söderström", "Medborgare Johansson", "Medborgare Holmberg", "Medborgare Pettersson",
                "Medborgare Olofsson", "Medborgare Berglund", "Medborgare Gustafsson", "Medborgare Ekström", "Medborgare Eriksson",
                "Medborgare Malmström", "Medborgare Forsberg", "Medborgare Nordström", "Medborgare Öberg", "Medborgare Isaksson"
            };
            private static readonly string[] ThiefNames = {
              "Tjuven Tommy", "Tjuven Susie", "Tjuven Bobby", "Tjuven Steve", "Tjuven Vicky",
                "Tjuven Danny", "Tjuven Rita", "Tjuven Eddie", "Tjuven Maggie", "Tjuven Frankie",
                "Tjuven Lenny", "Tjuven Connie", "Tjuven Ronny", "Tjuven Lucy", "Tjuven Harry",
                "Tjuven Penny", "Tjuven Vinny", "Tjuven Mia", "Tjuven Johnny", "Tjuven Gina",
                "Tjuven Larry", "Tjuven Freddy", "Tjuven Sally", "Tjuven Tony", "Tjuven Wendy",
                "Tjuven Mickey", "Tjuven Cindy", "Tjuven Bobby", "Tjuven Rosie", "Tjuven Joey"
            };

            public static List<Police> CreatePolice(List<Person> personsList, Random random, int cityWidth, int cityHeight, ref int policeNameNr)
            {
                var policeList = new List<Police>();
                Police police = new Police(PoliceNames[policeNameNr % PoliceNames.Length], random.Next(1, cityWidth - 1), random.Next(1, cityHeight - 1), 'P', random.Next(8));
                personsList.Add(police);
                policeList.Add(police);
                policeNameNr++;
                return policeList;
            }
            public static List<Citizen> CreateCitizens(List<Person> personsList, Random random, int cityWidth, int cityHeight, ref int citizenNameNr)
            {
                var citizensList = new List<Citizen>();
                Citizen citizen = new Citizen(CitizenNames[citizenNameNr % CitizenNames.Length], random.Next(1, cityWidth - 1), random.Next(1, cityHeight - 1), 'C', random.Next(8), false);
                citizen.Inventory.Add("Nycklar");
                citizen.Inventory.Add("Mobil");
                citizen.Inventory.Add("Plånbok");
                citizen.Inventory.Add("Klocka");
                personsList.Add(citizen);
                citizenNameNr++;
                return citizensList;
            }
            public static List<Thief> CreateThieves(List<Person> personsList, Random random, int cityWidth, int cityHeight, ref int thiefNameNr)
            {
                var thievesList = new List<Thief>();
                Thief thief = new Thief(ThiefNames[thiefNameNr % ThiefNames.Length], random.Next(1, cityWidth - 1), random.Next(1, cityHeight - 1), 'T', false, random.Next(8));
                personsList.Add(thief);
                thiefNameNr++;
                return thievesList;
            }
        }
    }
}

