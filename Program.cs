using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace FoodTrailerJsonApp
{
    public class CrewMember
    {
        public string Name { get; set; } = "";
        public string Role { get; set; } = "";
        public string Shift { get; set; } = "";
        public bool Certified { get; set; }
    }

    class Program
    {
        static void Main()
        {
            string filePath = "crew.json";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("crew.json file was not found.");
                return;
            }

            string jsonText = File.ReadAllText(filePath);

            List<CrewMember>? crewMembers =
                JsonSerializer.Deserialize<List<CrewMember>>(jsonText);

            if (crewMembers == null || crewMembers.Count == 0)
            {
                Console.WriteLine("No crew members found.");
                return;
            }

            Console.WriteLine("=== Food Trailer Crew Schedule ===\n");

            foreach (CrewMember member in crewMembers)
            {
                Console.WriteLine($"Name: {member.Name}");
                Console.WriteLine($"Role: {member.Role}");
                Console.WriteLine($"Shift: {member.Shift}");
                Console.WriteLine($"Certified: {(member.Certified ? "Yes" : "No")}");
                Console.WriteLine("------------------------------");
            }
        }
    }
}