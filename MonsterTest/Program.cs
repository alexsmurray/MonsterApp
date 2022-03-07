using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace MonsterTest
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Hero hero = new Hero();

            if (GetHeroName(hero))
            {
                Console.WriteLine($"Hello, {hero.HeroName}. Please select a class.");
                if (GetArchetype(hero))
                {
                    Console.WriteLine($"Your class is {hero.Archetype}.");
                }
            }

            /*
            // generates a random number
            var rand = new Random((int)DateTime.Now.TimeOfDay.TotalMilliseconds);

            // api list for monsters
            var monsters = new List<Monster>();
            var request = "https://api.open5e.com/monsters";

            Console.WriteLine("Fetching monsters...");
            using (var client = new HttpClient())
            {
                MonsterList? result = null;

                do
                {
                    try
                    {
                        using (var response = client.GetAsync(request).Result)
                        {
                            response.EnsureSuccessStatusCode();

                            var json = response.Content.ReadAsStringAsync().Result;
                            result = System.Text.Json.JsonSerializer.Deserialize<MonsterList>(json);
                            monsters.AddRange(result.results);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    //goes through each page of api until there are no pages left
                    request = result.next;

                } while (!string.IsNullOrEmpty(result?.next));
            }

            //displays total number of monsters in api
            Console.WriteLine($"Found {monsters.Count} monsters");


            // selects a random monster within a specified scope then displays it
            var fighters = (from m in monsters where m.challenge_rating == "1" select m).ToList();

            var n = rand.Next(0, fighters.Count());

            Console.WriteLine($"A {fighters[n].name} ({fighters[n].challenge_rating}) with {fighters[n].hit_points} hp has appeared.");
            Console.WriteLine("Make an attack");
            Console.ReadLine();

            var attack = rand.Next(0, 10);
            Console.WriteLine($"You deal {attack} damage. {fighters[n].hit_points - attack} hp remaining.");
           */
        }

        static bool GetHeroName(Hero hero)
        {
            var nameisnotvalid = true;
            do
            {
                Console.WriteLine("Please enter your name.");
                hero.HeroName = Console.ReadLine();

                // validate heorName
                if (!string.IsNullOrWhiteSpace(hero.HeroName))
                {
                    nameisnotvalid = false;
                }

            } while (nameisnotvalid);

            return true;
        }

        static bool GetArchetype(Hero hero)
        {
            do
            {
                Console.WriteLine("\t1 - Warrior");
                Console.WriteLine("\t2 - Rogue");
                Console.WriteLine("\t3 - Mage");
                Console.WriteLine("\t4 - Ranger");


                switch (Console.ReadLine())
                {
                    case "1":
                        
                        hero.Archetype = "Warrior";
                        return true;
                    case "2":
                        hero.Archetype = "Rogue";
                        return true;
                    case "3":
                        hero.Archetype = "Mage";
                        return true;
                    case "4":
                        hero.Archetype = "Ranger";
                        return true;
                    default:
                        Console.WriteLine("Invalid response. Please enter 1, 2, 3, or 4.");
                        break;

                }


            } while (true);
        }
    }
}
