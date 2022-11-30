using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_based_combat
{
    public class ColorPrinter
    {
        // Color constants
        public const ConsoleColor defaultColor = ConsoleColor.Gray;
        public const ConsoleColor noteColor = ConsoleColor.Yellow;
        public const ConsoleColor errorColor = ConsoleColor.Red;
        public const ConsoleColor playerColor = ConsoleColor.Blue;
        public const ConsoleColor enemyColor = ConsoleColor.DarkRed;

        public static ConsoleColor TeamToColor(Team team)
        {
            switch (team)
            {
                case Team.Player:
                    return playerColor;
                case Team.Enemy:
                    return enemyColor;
                case Team.Neutral:
                    return defaultColor;
                default:
                    return defaultColor;
            }
        }

        public static void ColorWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
        }

        public static void ColorWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            LineEnd();
        }

        public static void LineEnd()
        {
            Console.Write("\n");
        }

        public static void PrintArmy(string text, Army army)
        {
            ColorWriteLine(text, noteColor);

            int number = 1;
            foreach (Unit unit in army.Units)
            {
                PrintUnit(number, unit);
                number++;
            }
        }

        public static void PrintUnit(int number, Unit unit)
        {
            if (unit.hitpoints > 0)
            {
                ColorWrite(number + ": ", defaultColor);
                ColorWrite(unit.name, TeamToColor(unit.team));
                ColorWrite(" Dmg: " + unit.damage + " HP:" + unit.hitpoints, defaultColor);
                LineEnd();
            }
            else
            {
                ColorWriteLine(number + ": [Defeated] " + unit.name, defaultColor);
            }
        }

        public static void PrintAttack(Unit attacker, Unit defender)
        {
            ColorWrite(attacker.name, TeamToColor(attacker.team));
            ColorWrite(" attacks ", defaultColor);
            ColorWrite(defender.name, TeamToColor(defender.team));
            ColorWrite(" Dealing " + attacker.damage + " damage!", noteColor);
            LineEnd();

            if (defender.hitpoints <= 0)
            {
                defender.hitpoints = 0;
                ColorWriteLine(defender.name + " is defeated!", noteColor);
            }
        }
    }
}
