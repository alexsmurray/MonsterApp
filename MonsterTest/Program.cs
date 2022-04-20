using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Collections;
using System.IO;
using Newtonsoft.Json;

namespace MonsterTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hero hero = null;
            string startingMonster = null;
            int startingChallenge = 0;

            while (hero == null)
            {
                Console.WriteLine("1 - Start New Game");
                Console.WriteLine("2 - Load Game");

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            HeroGenerator generator = new HeroGenerator();
                            hero = generator.GenerateHero();
                            break;
                        }
                    case "2":
                        Console.WriteLine("Name?");
                        var name = Console.ReadLine();
                        try
                        {
                            var save = ReadHeroFromFile(name);
                            hero = LoadSave(save);
                            startingMonster = save.monsterName;
                            startingChallenge = save.challengeIndex;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to load file");
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Invalid response. Please enter 1 or 2.");
                            break;
                        }

                }
            }

            // master loop for generating monsters for combat
            var monsterGenerator = new MonsterGenerator();
            monsterGenerator.GetMonsters();

            // selects a random monster within a specified scope then displays it
            string[] challengeRating = new string[] {"0","1/8","1/4", "1/2", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
                                                         "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                                                         "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"};

            CombatArena arena = new CombatArena();
            
            //int i represents the number of combat iterations are ran.
            //challengeIndex represents the index of the challengeRating array values
            for (int i = startingChallenge, challengeIndex = startingChallenge + 4; i < 5; i++, challengeIndex++)
            {
                Monster fighter = null;

                if (!string.IsNullOrEmpty(startingMonster))
                {
                    fighter = monsterGenerator.GetMonsterByName(startingMonster);
                }
                else
                {
                    fighter = monsterGenerator.GetAMonster(challengeRating[challengeIndex]);
                }

                Console.WriteLine($"A {fighter.name} with a challenge rating of {fighter.challenge_rating} and {fighter.hit_points} hp has appeared.");

                var result = arena.Combat(hero, fighter);
                if (result == CombatResult.monsterWins)
                {
                    Console.WriteLine("The monsters have defeated you. Game Over.");
                    break;
                }
                else if (result == CombatResult.playerQuits)
                {
                    SaveFile save = AddSave(hero, i, fighter);
                    WriteHeroToFile(save);
                    
                    Console.WriteLine("Quitting the game.");
                    break;
                }

                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"{hero.heroName} has {hero.healthPoints} health remaining.");
            }

           

            Console.ReadLine();
        }

        public static SaveFile AddSave(Hero hero, int challengeIndex, Monster fighter)
        {
            SaveFile saveFile = new SaveFile()
            {
                heroType = hero.GetType().FullName,
                playerName = hero.heroName,
                playerClass = hero.archetype,
                playerHealth = hero.healthPoints,
                challengeIndex = challengeIndex,
                monsterName = fighter.name
            };
            return saveFile;
        }
        public static Hero LoadSave(SaveFile save)
        {
            Hero hero = null;

            var heroType = System.Reflection.Assembly.GetExecutingAssembly()?.GetType(save.heroType);

            if (heroType != null)
            {
                hero = Activator.CreateInstance(heroType) as Hero;
                if (hero != null)
                {
                    hero.heroName = save.playerName;
                    hero.archetype = save.playerClass;
                    hero.healthPoints = save.playerHealth;
                }
            }

            return hero;
        }

        public static void WriteHeroToFile(SaveFile saveFile)
        {
            string heroAsString = JsonConvert.SerializeObject(saveFile);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + $"{saveFile.playerName}.txt", heroAsString);
            Console.WriteLine("Saving player data.");
        }

        public static SaveFile? ReadHeroFromFile(string playerName)
        {
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + $"{playerName}.txt");
            return JsonConvert.DeserializeObject<SaveFile>(json);
        }


    }
}
