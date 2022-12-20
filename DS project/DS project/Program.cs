using BoxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxDal;
using DataStructures;
using System.Windows.Threading;

namespace DS_project
{
    public class Program
    {
        static void Main(string[] args)
        {
            Screen screen = new Screen();
            DB.Instance.Init();
            Console.WriteLine("Welcome to Packing Boxes Arrangement Warehouse");
            Console.WriteLine("----------------------------------------------");
            screen.MainMenu();
        }
    }
}