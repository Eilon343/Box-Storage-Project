using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxModel;
using DataStructures;

namespace DS_project
{
    internal class Screen
    {
        private Manager _m = new Manager();
        private UiLogic _uiLogic = new UiLogic();

        private string _userInput;
        private double _userWidth = 0;
        private double _userHeight = 0;
        private int _boxesAmount = 0;
        public void MainMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Show all the boxes in the warehouse");
            Console.WriteLine("2. Buy a box");
            Console.WriteLine("3. Show Expired Boxes");
            Console.WriteLine("4. Exit");
            _userInput = Console.ReadLine();
            while (_userInput != "4")
            {
                if (Int32.TryParse(_userInput, out int Result))
                {

                    switch (Result)
                    {
                        case 1:
                            {
                                PresentBoxes();
                                MainMenu();
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                _uiLogic.BuyBoxesSection(_userWidth,_userHeight,_boxesAmount, _m);
                                MainMenu();
                                break;
                            }
                        case 3:
                            {
                                Console.Clear();
                                _uiLogic.ShowExpiredBoxes(_m);
                                MainMenu();
                                break;
                            }
                    }
                    if (Result > 4 || Result < 1)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter a valid number");
                        Console.ForegroundColor = ConsoleColor.White;
                        MainMenu();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a number.");
                    Console.ForegroundColor = ConsoleColor.White;
                    MainMenu();
                }
            }
            Console.WriteLine("Have a good day (:");
        }
        private void PresentBoxes()
        {
            _m.ShowBoxes();
        }
        
    }
}
