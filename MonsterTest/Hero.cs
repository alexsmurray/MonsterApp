using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTest
{
    internal abstract class Hero
    {
        public string heroName { get; set; }
        public string archetype { get; set; }
        public int healthPoints { get; set; }
        public int basicAttack { get; set; }
        public int ability { get; set; }
    }

    internal class Warrior : Hero
    {
        public Warrior()
        {
            archetype = "Warrior";

            healthPoints = 100;

            basicAttack = 12;

            ability = 20;

        }

    }

    internal class Rogue : Hero
    {
        public Rogue()
        {
            archetype = "Rogue";

            healthPoints = 60;

            basicAttack = 10;

            ability = 30;
        }

    }

    internal class Mage : Hero
    {
        public Mage()
        {
            archetype = "Mage";

            healthPoints = 45;

            basicAttack = 20;

            ability = 50;
        }
    }

    internal class Ranger : Hero
    {
        public Ranger()
        {

            archetype = "Ranger";

            healthPoints = 75;

            basicAttack = 11;

            ability = 25;

        }
    }
}
