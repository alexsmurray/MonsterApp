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
    }
}

