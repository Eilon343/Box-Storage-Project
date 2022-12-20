using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures;
using BoxModel;

namespace DS_project
{
    internal class UiLogic
    {
        private string _userInput;
        public void BuyChoice(int BoxesAmount, Stacko<Box> FitBoxStack, Manager _m)
        {
            Console.WriteLine("Do You want to buy?");
            Console.WriteLine("Press 1 if yes, Press 2 if not");
            _userInput = Console.ReadLine();
            Int32.TryParse(_userInput, out int Result);
            if (Result == 1)
            {
                _m.BuyBox(BoxesAmount, FitBoxStack);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Bought.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (Result == 2)
            {
                Console.Clear();
            }
            if (Result > 2 || Result < 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid number");
                Console.ForegroundColor = ConsoleColor.White;
                BuyChoice(BoxesAmount, FitBoxStack, _m);
            }
        }
        /// <summary>
        /// Gets a list with fitted boxes and a list of how many from each box,printing the boxes and returning their sum
        /// </summary>
        /// <param name="FitBoxes"></param>
        /// <param name="Counters"></param>
        /// <returns></returns>
        public int CheckHowManyBoxes(Stacko<Box> FittedBoxes, Stacko<int> Counters) // checking duplicate boxes and showing them to the customer
        {
            Stacko<Box> TempFitBoxes = new Stacko<Box>();
            foreach (var item in FittedBoxes)
            {
                TempFitBoxes.Push(item);
            }
            int SumBoxes = 0;
            while (TempFitBoxes.Count > 0 && Counters.Count > 0)
            {
                var LastCounter = Counters.Pop();
                var LastBox = TempFitBoxes.Pop();
                Console.WriteLine($"{LastCounter} With Width: {LastBox.Width} and Height: {LastBox.Height}");
                SumBoxes += LastCounter;
            }
            return SumBoxes;
        }
        public void ShowExpiredBoxes(Manager _m)
        {
            var ExpiredBoxes = _m.CheckBoxesThatHasntBought();
            if (ExpiredBoxes.Length > 0)
            {
                foreach (var ExpiredBox in ExpiredBoxes)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Box Width: {ExpiredBox.Width}");
                    Console.WriteLine($"Box Height: {ExpiredBox.Height}");
                    Console.WriteLine($"Box Quantitity: {ExpiredBox.Quantity}");
                    Console.WriteLine($"Box Manufacturing Date: {ExpiredBox.ManufacturingDate:d}");
                    Console.WriteLine($"Box Expiration Date: {ExpiredBox.ExpirationDate:d}");
                    Console.WriteLine("----------------------------------");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("There are no expired boxes");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public void BuyBoxesSection(double _userWidth, double _userHeight, int _boxesAmount, Manager _m)
        {
            try
            {
                Console.WriteLine("Enter the Width of your package");
                _userWidth = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter the Heigth of your package");
                _userHeight = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter the amount of boxes you want to buy");
                _boxesAmount = Convert.ToInt32(Console.ReadLine());
                var FitBoxStack = _m.BuyAmount(_userWidth, _userHeight, _boxesAmount, out Stacko<int> counters);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You asked for {_boxesAmount} boxes with Width: {_userWidth} Height {_userHeight}");
                if (FitBoxStack.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sorry We Couldn't found boxes what will fit your needs");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("We've found boxes that fits you needs: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    int SumBoxes = CheckHowManyBoxes(FitBoxStack, counters);
                    Console.WriteLine($"Which is {SumBoxes} boxes");
                    BuyChoice(_boxesAmount, FitBoxStack, _m);
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a number");
                Console.ForegroundColor = ConsoleColor.White;
                BuyBoxesSection(_userWidth, _userHeight, _boxesAmount, _m);
            }
        }
    }
}
