using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Collections;

namespace MonsterTest
{
    internal class Program
    {
        static Random rand;


        static void Main(string[] args)
        {
            Hero hero = GetArchetype();

           GetHeroName(hero);
            Console.WriteLine(hero.heroName);
            Console.WriteLine(hero.archetype);
            Console.WriteLine(hero.healthPoints);
            Console.WriteLine(hero.basicAttack);



            // generates a random number
            rand = new Random((int)DateTime.Now.TimeOfDay.TotalMilliseconds);

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
            var fighter = fighters[n];

            Console.WriteLine($"A {fighters[n].name} ({fighters[n].challenge_rating}) with {fighters[n].hit_points} hp has appeared.");

            Combat(hero, fighter);

            Console.ReadLine();

           
            

        }


        static bool GetHeroName(Hero hero)
        {
            var nameisnotvalid = true;
            do
            {
                Console.WriteLine("Please enter your name.");
                hero.heroName = Console.ReadLine();

                // validate heroName
                if (!string.IsNullOrWhiteSpace(hero.heroName))
                {
                    nameisnotvalid = false;
                }

            } while (nameisnotvalid);

            return true;
        }

        static Hero GetArchetype()
        {
            do
            {
                Console.WriteLine($"Hello, please select a class.");
                Console.WriteLine("\t1 - Warrior");
                Console.WriteLine("\t2 - Rogue");
                Console.WriteLine("\t3 - Mage");
                Console.WriteLine("\t4 - Ranger");


                switch (Console.ReadLine())
                {
                    case "1":
                       return new Warrior();
                    case "2":
                        return new Rogue();
                    case "3":
                        return new Mage();
                    case "4":
                        return new Ranger();
                    default:
                        Console.WriteLine("Invalid response. Please enter 1, 2, 3, or 4.");
                        break;

                }


            } while (true);
        }

        static void Combat(Hero hero, Monster fighter)
        {
            while (fighter.hit_points > 0  && hero.healthPoints > 0)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("\t1 - Basic Attack");
                Console.WriteLine($"\t2 - {hero.abilityName}");
                Console.WriteLine("\t3 - Health Potion");
                Console.WriteLine("\t4 - Quit Game");

                
                switch (Console.ReadLine())
                {
                    case "1":
                        var attack = rand.Next(1, hero.basicAttack);
                        fighter.hit_points -= attack;
                        
                        Console.WriteLine($"You deal {attack} basic attack damage. {fighter.name} has {fighter.hit_points} hp remaining.");
                        
                        break;

                    case "2":
                        var ability = rand.Next(1, hero.ability);
                        fighter.hit_points -= ability;

                        Console.WriteLine($"You deal {ability} damage with {hero.abilityName}. {fighter.hit_points} hp remaining.");

                        break;

                    case "3":
                        var potion = hero.maxHealthPoints / 2;
                        hero.healthPoints += potion;
                        Console.WriteLine($"You heal for {potion} health. You have {hero.healthPoints} hp remaining.");
                        break;

                    case "4":
                        Console.WriteLine("quit now");
                        return;

                    default:
                        Console.WriteLine("Invalid response. Please enter 1, 2, 3, or 4.");
                        break;

                }

                var damage = rand.Next(fighter.AttackDamage);
                hero.healthPoints -= damage;
                Console.WriteLine($"{fighter.name} deals {damage} damage to you. You have {hero.healthPoints} hp remaining.");
            } 
        }

    }
}
