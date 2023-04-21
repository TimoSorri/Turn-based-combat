using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_based_combat
{
    public class Unit
    {
        public string name;
        public int damage;
        public int hitpoints;
        public Team team;  /* <<< Team enum here */

        public Unit(string name, int damage, int hitpoints)
        {
            this.name = name;
            this.damage = damage;
            this.hitpoints = hitpoints;
        }
    }
}