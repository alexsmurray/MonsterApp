using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTest
{
    internal class MonsterList
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Monster> results { get; set; }
    }

    internal class Monster
    {
        public string name { get; set; }
        public int armor_class { get; set; }
        public int hit_points { get; set; }
        public string challenge_rating { get; set; }

        private IDictionary<string, int> _attackDamage = new Dictionary<string, int>()
        {
            { "0", 2 },
            { "1/8", 4 },
            { "1/4", 6 },
            { "1/2", 8 },
            { "1", 10 },
            { "2", 12 },
            { "3", 14 },
            { "4", 16 },
            { "5", 18 },
            { "6", 20 },
            { "7", 22 },
            { "8", 24 },
            { "9", 26 },
            { "10", 28 },
            { "11", 30 },
            { "12", 32 },
            { "13", 34 },
            { "14", 36 },
            { "15", 38 },
            { "16", 40 },
            { "17", 42 },
            { "18", 44 },
            { "19", 46 },
            { "20", 48 },
            { "21", 50 },
            { "22", 52 },
            { "23", 54 },
            { "24", 56 },
            { "25", 58 },
            { "26", 60 },
            { "27", 62 },
            { "28", 64 },
            { "29", 66 },
            { "30", 68 }

        };
        
        public int AttackDamage
        {
            get { return _attackDamage[challenge_rating]; }
        }
    }
}

