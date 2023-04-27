using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_based_combat
{
    internal class Attack
    {
        Unit attacker;
        Unit defender;
        public Attack(Unit attacker, Unit defender)
        {
            this.attacker = attacker;
            this.defender = defender;
        }
        public void Do()
        {
            this.defender.hitpoints -= this.attacker.damage;
        }
        public void Undo()
        {
            this.defender.hitpoints += this.attacker.damage;
        }
    }
}
