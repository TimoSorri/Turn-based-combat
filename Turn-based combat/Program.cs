using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Turn_based_combat;

namespace turn_based_combat
{
    class Program
    {
        static Stack<Attack> attackActions = new Stack<Attack>();

        public static void Main()
        {
            /*Army playerArmy = new Army("Player army", Team.Player);
            playerArmy.AddUnit(new Unit("dwarf", 15, 150, 150));
            playerArmy.AddUnit(new Unit("Elf", 20, 50, 50));
            playerArmy.AddUnit(new Unit("Human", 90, 99, 99));*/

            Army enemyArmy = new Army("Enemy army", Team.Enemy);
            enemyArmy.AddUnit(new Unit("Orc", 15, 15, 15));
            enemyArmy.AddUnit(new Unit("Skeleton", 20, 10, 10));
            enemyArmy.AddUnit(new Unit("God", 1, 1, 1));

            /*Army NeutralArmy = new Army("Animal army", Team.Neutral);
            NeutralArmy.AddUnit(new Unit("Bear", 50, 50, 50));
            NeutralArmy.AddUnit(new Unit("Dragon", 99, 99, 99));
            NeutralArmy.AddUnit(new Unit("Cat", 1, 1, 1));*/

            Army playerArmy = new Army("Player army", Team.Player);


            Random randomGenerator = new Random();

            ColorPrinter.ColorWriteLine("Welcome to my Turn-Based Game!", ColorPrinter.noteColor);
            Console.WriteLine();
            ColorPrinter.ColorWriteLine("Oh look, There's enemies ahead. Go Kill them", ColorPrinter.noteColor);
            Console.WriteLine();
            ColorPrinter.ColorWriteLine("But first you need an army", ColorPrinter.noteColor);
            Console.WriteLine();
            Console.WriteLine("Press any key to go into army select!");
            Console.ReadKey();
            Console.Clear();

            ColorPrinter.ColorWriteLine("Welcome to the army select", ColorPrinter.noteColor);
            Console.WriteLine();


            int armyChoice;
            bool isValidChoice = false;

            while (!isValidChoice)
            {
                // Display the available armies
                ColorPrinter.ColorWriteLine("Select your army by pressing the corresponding number (1 or 2):", ColorPrinter.noteColor);
                Console.WriteLine("");


                // Display the unit stats for each army
                ColorPrinter.ColorWriteLine("1. Human army:",ColorPrinter.noteColor);
                ColorPrinter.ColorWriteLine("Unit name: Dwarf DMG: 15 HP: 150", ColorPrinter.playerColor);
                ColorPrinter.ColorWriteLine("Unit name: Elf DMG: 20 HP: 50", ColorPrinter.playerColor);
                ColorPrinter.ColorWriteLine("Unit name: Human DMG: 90 HP: 90", ColorPrinter.playerColor);
                ColorPrinter.ColorWriteLine("", ColorPrinter.noteColor);

               ColorPrinter.ColorWriteLine("2. Animal army:", ColorPrinter.noteColor);
                ColorPrinter.ColorWriteLine("Unit name: Bear DMG: 50 HP: 50", ColorPrinter.playerColor);
                ColorPrinter.ColorWriteLine("Unit name: Dragon DMG: 99 HP: 99", ColorPrinter.playerColor);
                ColorPrinter.ColorWriteLine("Unit name: Cat DMG: 1 HP: 1", ColorPrinter.playerColor);

                // Get the player's choice
                isValidChoice = int.TryParse(Console.ReadLine(), out armyChoice);

                // Handle the player's choice using a switch statement
                switch (armyChoice)
                {
                    case 1:
                        Console.WriteLine("Human army selected");
                        playerArmy.AddUnit(new Unit("dwarf", 15, 150, 150));
                        playerArmy.AddUnit(new Unit("Elf", 20, 50, 50));
                        playerArmy.AddUnit(new Unit("Human", 90, 99, 99));
                        break;
                    case 2:
                        Console.WriteLine("Animal army selected");
                        playerArmy.AddUnit(new Unit("Bear", 50, 50, 50));
                        playerArmy.AddUnit(new Unit("Dragon", 99, 99, 99));
                        playerArmy.AddUnit(new Unit("Cat", 1, 1, 1));
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                        isValidChoice = false;
                        break;
                }

                // Clear the console after the player has made their choice or entered an invalid choice
                Console.Clear();
            }




            ColorPrinter.ColorWriteLine("Battle begins!", ColorPrinter.noteColor);
            ColorPrinter.ColorWriteLine("Give the number you want to choose as attacker and then give the number of who you want to attack, you can also press z to undo your action", ColorPrinter.noteColor);
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
            Console.Clear();
            ColorPrinter.ColorWriteLine("WOW. you actually finished the game. Congratulations now leave!", ColorPrinter.noteColor);
            ColorPrinter.ColorWriteLine("Press enter two times and you are free to go on with your day", ColorPrinter.noteColor);
            Console.ReadLine();
        }
        public static Unit selectUnit(string prompt, Army army)
        {
            bool hasMoved = false;

            while (true)
            {
                ColorPrinter.PrintArmy(prompt, army);
                string valinta = Console.ReadLine();

                if (valinta == "z")
                {
                    if (hasMoved = true)
                    {
                        UndoLastAttack();
                        hasMoved = false;
                    }
                    else
                    {
                        ColorPrinter.ColorWriteLine("Cannot undo without making a move first.", ColorPrinter.errorColor);
                    }
                }
                else
                {
                    int numero = Convert.ToInt32(valinta);
                    if (numero > 0 && numero <= army.Units.Count)
                    {
                        int index = numero - 1;
                        Unit selected = army.Units[index];
                        if (selected.hitpoints > 0)
                        {
                            // Ok!
                            hasMoved = true;
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
        }

        private static void UndoLastAttack()
        {
            if (attackActions.Count > 0)
            {
                Attack last = attackActions.Pop();
                last.Undo();
            }
            else
            {
                ColorPrinter.ColorWriteLine("No moves to undo.", ColorPrinter.errorColor);
            }
        }


        /*public static Unit selectUnit(string prompt, Army army)
        {
            int attackNumber = 0;

            while (true)
            {
                ColorPrinter.PrintArmy(prompt, army);

                string valinta = Console.ReadLine();

                if (valinta == "z" && attackNumber > 0)
                {
                    Attack last = attackActions.Pop();
                    last.Undo();
                    attackNumber--;
                }
                else if (valinta == "z" && attackNumber == 0)
                {
                    ColorPrinter.ColorWriteLine("You haven't done anything", ColorPrinter.errorColor);
                    break;
                }
                else if (!int.TryParse(valinta, out int numero))
                {
                    ColorPrinter.ColorWriteLine("Invalid input. Try again.", ColorPrinter.errorColor);
                }
                else if (numero < 1 || numero > army.Units.Count)
                {
                    ColorPrinter.ColorWriteLine("Invalid unit number. Try again.", ColorPrinter.errorColor);
                }
                else
                {
                    Unit selected = army.Units[numero - 1];
                    if (selected.hitpoints <= 0)
                    {
                        ColorPrinter.ColorWriteLine("Cannot choose a dead unit.", ColorPrinter.errorColor);
                    }
                    else
                    {
                        attackNumber++;
                        return selected;
                    }
                }
            }

            return null;
        }
        */


        /*public static Unit selectUnit(string prompt, Army army)
        {
            Unit selectedUnit = null;
            int selectedUnitCount = 0;
            while (true)
            {
                ColorPrinter.PrintArmy(prompt, army);
                string valinta = Console.ReadLine();

                if (valinta == "z" && selectedUnitCount >0)
                {
                    Attack last = attackActions.Pop();
                    last.Undo();
                    selectedUnitCount--;
                }
                else
                {
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
                while (selectedUnit == null) ;
                return selectedUnit;
            }
        }*/

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

                // !
                // Store the attack that just happened
                Attack attack = new Attack(attacker, defender);
                attackActions.Push(attack);
            }
        }
    }
}