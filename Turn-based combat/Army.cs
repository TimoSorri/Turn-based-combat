using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_based_combat
{
    public class Army
    {
        public string Name { get; private set; }
        private Team team;
        public List<Unit> Units { get; private set; }

        public Army(string name, Team team)
        {
            this.Name = name;
            this.team = team;
            this.Units = new List<Unit>();
        }
        public void AddUnit(Unit unit)
        {
            unit.team = this.team;
            this.Units.Add(unit);
        }

        public bool AreAllDead()
        {
            foreach (Unit unit in this.Units)
            {
                if (unit.hitpoints > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
