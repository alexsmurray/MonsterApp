using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTest
{
    internal class HeroGenerator
    {

        public Hero GenerateHero()
        {
            Hero hero = GetArchetype();
            hero.heroName = GetHeroName(hero);

            Console.Clear();
            Console.WriteLine($"Hello, {hero.heroName} the {hero.archetype}.");
            Console.WriteLine($"You have { hero.maxHealthPoints} health and can deal up to { hero.basicAttack} basic attack damage.");
            Console.WriteLine($"You have an ability called {hero.abilityName} that can deal up to {hero.ability} damage and has a 2 round cooldown between uses.");
            Console.WriteLine("You have 3 health potions to use per encounter that will heal you for half of your maximum health.");

            return hero;
        }


        private string GetHeroName(Hero hero)
        {
            var name = string.Empty;
            
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please enter your name.");
                name = Console.ReadLine();
            }

            return name;
        }

        private Hero GetArchetype()
        {
            while (true)
            {
                Console.WriteLine($"Please select your class.");
                Console.WriteLine("\t1 - Warrior");
                Console.WriteLine("\t2 - Rogue");
                Console.WriteLine("\t3 - Mage");
                Console.WriteLine("\t4 - Ranger");


                switch (Console.ReadLine())
                {
                    case "1":
                        { return new Warrior(); }
                    case "2":
                        { return new Rogue(); }
                    case "3":
                        { return new Mage(); }
                    case "4":
                        { return new Ranger(); }
                    default:
                        {
                            Console.WriteLine("Invalid response. Please enter 1, 2, 3, or 4.");
                            break;
                        }

                }
            }
        }




          
    }
}
