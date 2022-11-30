using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Turn_based_combat;

namespace turn_based_combat
{
    public class Program
    {
        static void Main(string[] args)
        {
            Army playerArmy = new Army("Human army", Team.Player);
            playerArmy.AddUnit(new Unit("Dwarf", 15, 150));
            playerArmy.AddUnit(new Unit("Elf", 20, 50));
            playerArmy.AddUnit(new Unit("Human", 90, 90));

            Army enemyArmy = new Army("Skeleton army", Team.Enemy);
            enemyArmy.AddUnit(new Unit("Orc", 15, 55));
            enemyArmy.AddUnit(new Unit("Skeleton", 20, 10));
            enemyArmy.AddUnit(new Unit("God", 1, 1));

            Random randomGenerator = new Random();

            ColorPrinter.ColorWriteLine("Battle begins!", ColorPrinter.noteColor);
            ColorPrinter.PrintArmy(playerArmy.Name, playerArmy);
            ColorPrinter.PrintArmy(enemyArmy.Name, enemyArmy);

            while (true)
            {
                // Player turn
                if (playerArmy.AreAllDead())
                {
                    break;
                }
                Unit attacker = selectUnit("Select attacker:", playerArmy);
                Unit defender = selectUnit("Select target:", enemyArmy);
                attack(attacker, defender);

                // Enemy turn
                if (enemyArmy.AreAllDead())
                {
                    break;
                }
                attacker = randomizeUnit(enemyArmy, randomGenerator);
                defender = randomizeUnit(playerArmy, randomGenerator);
                attack(attacker, defender);
            }
            ColorPrinter.ColorWriteLine("The battle is over. Press any key...", ColorPrinter.noteColor);
            Console.ReadLine();
        }

        public static Unit selectUnit(string prompt, Army army)
        {
            while (true)
            {
                ColorPrinter.PrintArmy(prompt, army);
                string valinta = Console.ReadLine();
                int numero = Convert.ToInt32(valinta);

                if (numero > 0 && numero <= army.Units.Count)
                {
                    int index = numero - 1;
                    Unit selected = army.Units[index];
                    if (selected.hitpoints > 0)
                    {
                        // Ok!
                        return selected;
                    }
                    else
                    {
                        ColorPrinter.ColorWriteLine("Cannot choose a dead unit.", ColorPrinter.errorColor);
                    }
                }
                else
                {
                    ColorPrinter.ColorWriteLine("Invalid unit number. Try again.", ColorPrinter.errorColor);
                }
            }
        }

        // This returns first unit in case everyone is dead in the army,
        // to avoid infinite loop
        public static Unit randomizeUnit(Army army, Random random)
        {
            if (army.AreAllDead())
            {
                ColorPrinter.ColorWriteLine("Error, cannot select from dead army", ColorPrinter.errorColor);
                return army.Units[0];
            }
            while (true)
            {
                int index = random.Next(army.Units.Count);
                Unit selected = army.Units[index];
                if (selected.hitpoints > 0)
                {
                    // Ok!
                    return selected;
                }
            }
        }

        public static void attack(Unit attacker, Unit defender)
        {
            if (attacker.hitpoints > 0)
            {
                defender.hitpoints -= attacker.damage;
                ColorPrinter.PrintAttack(attacker, defender);
            }
        }
    }
}