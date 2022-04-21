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
                            Console.Clear();
                            Console.WriteLine("Your village is under attack by strange monsters and beings! \n You hear cries for help in the distance and see houses burning. \n You quickly collect yourself and grab your things to help. \n ");

                            HeroGenerator generator = new HeroGenerator();
                            hero = generator.GenerateHero();
                            break;
                        }
                    case "2":
                        Console.WriteLine("What is the mame of the character you wish to load?");
                        var name = Console.ReadLine();
                        try
                        {
                            var save = GameSaver.ReadHeroFromFile(name);
                            hero = GameSaver.LoadSave(save);
                            startingMonster = save.monsterName;
                            startingChallenge = save.challengeIndex;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to load file. Your game either does not exist yet or the name you entered was incorrect.");
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

                startingMonster = null;

                Console.WriteLine($"A {fighter.name} with a challenge rating of {fighter.challenge_rating} and {fighter.hit_points} hp has appeared.");

                var result = arena.Combat(hero, fighter);
                if (result == CombatResult.monsterWins)
                {
                    Console.WriteLine("The monsters have defeated you. Game Over.");
                    break;
                }
                else if (result == CombatResult.playerQuits)
                {
                    SaveFile save = GameSaver.AddSave(hero, i, fighter);
                    GameSaver.WriteHeroToFile(save);

                    Console.WriteLine("Quitting the game. Please press enter.");
                    break;
                }

                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"{hero.heroName} has {hero.healthPoints} health remaining.");

                if (i >= 4)
                {
                    Console.Clear();
                    Console.WriteLine($"Congratulations, {hero.heroName} the {hero.archetype}! \n You have successfully fought off all the monsters! \n Your village has been saved, and the villagers celebrate your name in victory! \n All is well, for now... \n The End.");
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
