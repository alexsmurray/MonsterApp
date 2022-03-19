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
        public int maxHealthPoints { get; set; }
        public int healthPoints { get; set; }
        public int basicAttack { get; set; }
        public string abilityName { get; set; }
        public int ability { get; set; }
    }

    internal class Warrior : Hero
    {
        public Warrior()
        {
            archetype = "Warrior";

            maxHealthPoints = 200;

            healthPoints = 200;

            basicAttack = 20;

            abilityName = "Whirlwind";

            ability = 40;

        }

    }

    internal class Rogue : Hero
    {
        public Rogue()
        {
            archetype = "Rogue";

            maxHealthPoints = 160;

            healthPoints = 160;

            basicAttack = 20;

            abilityName = "Backstab";

            ability = 50;
        }

    }

    internal class Mage : Hero
    {
        public Mage()
        {
            archetype = "Mage";

            maxHealthPoints = 145;

            healthPoints = 145;

            basicAttack = 15;

            abilityName = "Fireball";

            ability = 60;
        }
    }

    internal class Ranger : Hero
    {
        public Ranger()
        {

            archetype = "Ranger";

            maxHealthPoints = 175;

            healthPoints = 175;

            basicAttack = 25;

            abilityName = "Volley";

            ability = 30;

        }
    }
}
