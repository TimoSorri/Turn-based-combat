using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_based_combat
{
    internal class AttackAction
    {
        public Unit attacker;
        public Unit defender;


        public AttackAction(Unit attacker, Unit defender)
        {
            this.attacker = attacker;
            this.defender = defender;
        }

        public void Undo()
        {
            defender.hitpoints += attacker.hitpoints;
        }
    } 

}
