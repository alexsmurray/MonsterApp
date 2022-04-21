using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTest
{
    internal class CombatArena
    {
        private Random rand;

        public CombatArena()
        {
            rand = new Random();
        }

        public CombatResult Combat(Hero hero, Monster fighter)
        {
            var potionCount = 3;
            var cooldown = 0;
            string abilityCooldown;

           

            while (fighter.hit_points > 0 && hero.healthPoints > 0)
            {

                if (cooldown == 0)
                {
                    abilityCooldown = "Ready to use.";

                }
                else
                {
                    abilityCooldown = $"Ready in {cooldown--} rounds.";
                }


                Console.WriteLine("What would you like to do?");
                Console.WriteLine("\t1 - Basic Attack");
                Console.WriteLine($"\t2 - {hero.abilityName} ({abilityCooldown})");
                Console.WriteLine($"\t3 - Health Potion ({potionCount} remaining.)");
                Console.WriteLine("\t4 - Quit Game");

                

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            var attack = rand.Next(1, hero.basicAttack);
                            fighter.hit_points -= attack;

                            Console.WriteLine($"You deal {attack} basic attack damage. {fighter} has {fighter.hit_points} hp remaining.");
                            break;
                        }

                    case "2":
                        {
                            var ability = rand.Next(1, hero.ability);

                            if (cooldown == 0)
                            {
                                fighter.hit_points -= ability;
                                Console.WriteLine($"You deal {ability} damage with {hero.abilityName}. {fighter.hit_points} hp remaining.");
                                cooldown = 2;

                            }else
                            {
                                Console.WriteLine($"Your {hero.abilityName} is on a {cooldown} round cooldown.");
                                  
                            }
                            break;
                        }


                    case "3":
                        {
                            var potion = hero.maxHealthPoints / 2;

                            if (potionCount > 0)
                            { 
                                hero.healthPoints = Math.Min(hero.maxHealthPoints, hero.healthPoints + potion);
                                Console.WriteLine($"You heal for {potion} health. You have {hero.healthPoints} hp remaining.");
                                potionCount--;

                            } else
                            {
                                Console.WriteLine("You are out of health potions.");
                                Console.WriteLine($"You have {hero.healthPoints} hp remaining.");
                            }

                            break;
                        } 
               

                    case "4":
                        {
                            return CombatResult.playerQuits;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid response. Please enter 1, 2, 3, or 4.");
                            break;
                        }

                }

                var damage = rand.Next(fighter.AttackDamage);
                hero.healthPoints -= damage;
                Console.WriteLine($"{fighter} deals {damage} damage to you. You have {hero.healthPoints} hp remaining.");


            }

            if (hero.healthPoints <= 0)
            {
                return CombatResult.monsterWins;
            }
            else
            {
                return CombatResult.playerWins;
            }
        }
    }
}
