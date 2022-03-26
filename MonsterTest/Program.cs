using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Collections;

namespace MonsterTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HeroGenerator generator = new HeroGenerator();
            Hero hero = generator.GenerateHero();

            // master loop for generating monsters for combat
            var monsterGenerator = new MonsterGenerator();
            monsterGenerator.GetMonsters();

            // selects a random monster within a specified scope then displays it
            string[] challengeRating = new string[] {"0","1/8","1/4", "1/2", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
                                                         "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                                                         "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"};

            CombatArena arena = new CombatArena();

            for (int i = 0, challengeIndex = 4; i < 10; i++, challengeIndex++)
            {
                var fighter = monsterGenerator.GetAMonster(challengeRating[challengeIndex]);

                Console.WriteLine($"A {fighter.name} with a challenge rating of {fighter.challenge_rating} and {fighter.hit_points} hp has appeared.");

                var result = arena.Combat(hero, fighter);
                if (result == CombatResult.monsterWins)
                {
                    Console.WriteLine("The monsters have defeated you. Game Over.");
                    break;
                }
                else if (result == CombatResult.playerQuits)
                {
                    Console.WriteLine("Quitting the game...");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"{hero.heroName} has {hero.healthPoints} health remaining.");
            }

            Console.ReadLine();
        }
    }
}
