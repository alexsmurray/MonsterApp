using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTest
{
    internal class MonsterGenerator
    {
        private List<Monster> monsters;
        private Random rand;

        public MonsterGenerator()
        {
            rand = new Random();
        }

        public Monster GetAMonster(string challengeRating)
        {
            var fighters = (from m in monsters where m.challenge_rating == challengeRating select m).ToList();

            var n = rand.Next(0, fighters.Count());
            return (Monster)fighters[n].Clone();
        }

        public Monster GetMonsterByName(string name)
        {
            return (from m in monsters where m.name == name select m).FirstOrDefault();
        }

        public void GetMonsters()
        {
            // api list for monsters
            monsters = new List<Monster>();
            var request = "https://api.open5e.com/monsters";

            Console.WriteLine("A monster is appearing...");
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
            //Console.WriteLine($"Found {monsters.Count} monsters");
        }
    }
}
