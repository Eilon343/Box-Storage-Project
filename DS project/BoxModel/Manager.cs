using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BoxModel
{
    public class Manager
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        public Manager()
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 5);
            _timer.Tick += DeleteExpireEveryDay;
            _timer.Start();
        }
        /// <summary>
        /// checking every day if there is a box that expired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteExpireEveryDay(object sender, EventArgs e)
        {
            //checking the queue and if the head of the queue is expired, removing it and continue to next box,
            //if not break
            while (true)
            {
                var FirstBox = StorageManagment.Instance.ExpiredQueue.Peek();
                if (FirstBox != null && FirstBox.ExpirationDate.Second < DateTime.Now.Second)
                {
                    StorageManagment.Instance.RemoveBox(FirstBox);
                    StorageManagment.Instance.ExpiredQueue.Dequeue();
                }
                else
                    break;
            }
        }
        public DoubleLinkedList<Box> CheckBoxesThatHasntBought()
        {
            DoubleLinkedList<Box> ExpiredBoxes = new DoubleLinkedList<Box>();
            while (true)
            {
                var FirstBox = StorageManagment.Instance.ExpiredQueue.Peek();
                if (FirstBox != null && FirstBox.ExpirationDate.Day < DateTime.Now.Day )
                {
                    ExpiredBoxes.AddFirst(FirstBox);
                    StorageManagment.Instance.ExpiredQueue.Dequeue();
                }
                else
                    break;
            }
            return ExpiredBoxes;
        }
        /// <summary>
        /// printing all of the boxes in the data
        /// </summary>
        public void ShowBoxes()
        {
            //going over the two trees to print all of the box details
            foreach (BinarySearchTree<double, Box> inner in StorageManagment.Instance.MainTree.ValueEnumerator())
            {
                foreach (Box item in inner.ValueEnumerator())
                {
                    Console.WriteLine($"Box Width: {item.Width}");
                    Console.WriteLine($"Box Height: {item.Height}");
                    Console.WriteLine($"Box Quantitity: {item.Quantity}");
                    Console.WriteLine($"Box Manufacturing Date: {item.ManufacturingDate:d}");
                    Console.WriteLine($"Box Expiration Date: {item.ExpirationDate:d}");
                    Console.WriteLine("----------------------------------");
                }
            }
        }
        /// <summary>
        /// Getting a list of boxes to buy and amount to buy and removing from the data
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="BuyBoxList"></param>
        public void BuyBox(int amount, Stacko<Box> BuyBoxStack)
        {
            //getting a list of boxes that the user want to buy, if the quantity is above 0, --on quantitu until 
            //its 1 or until the amount of boxes ran out
            //if quantity is 0 removing the box from the data
            foreach (var Box in BuyBoxStack)
            {
                foreach (var item in StorageManagment.Instance.ExpiredQueue.List)
                {
                    //foreach on the list and if b == item moving this item to last in the queue beacause the
                    //expiration date updated
                    if (item == Box)
                    {
                        StorageManagment.Instance.ExpiredQueue.MoveToLast(item);
                        break;
                    }
                }
                while (Box.Quantity > 0 && amount > 0)
                {
                    Box.Quantity--;
                    amount--;
                    UpdateExpirationDate(Box);
                    if (Box.Quantity == 0)
                    {
                        StorageManagment.Instance.RemoveBox(Box);
                        StorageManagment.Instance.ExpiredQueue.List.RemoveLast();
                    }
                }
            }
        }
        /// <summary>
        /// Gets a width and height and returning the best possible box
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public Box FindBestBox(double Width, double Height)
        {
            double BiggerWidth = 0;
            double BiggerHeight = 0;
            //The height and width cant go above these variables
            double MaxWidthToSearch = Width * StorageManagment.Instance.Config.Data.MaxWidthToSearch;
            double MaxHeightToSearch = Height * StorageManagment.Instance.Config.Data.MaxHeightToSearch;
            if (StorageManagment.Instance.SearchBox(Width, Height, out Box b))//if given box exists return it
                return b;
            else
            {
                while (BiggerWidth <= MaxWidthToSearch)
                {
                    if (StorageManagment.Instance.MainTree.Search(Width))//if box does not exists but thw width is exist searching for bigger heights
                    {
                        while (BiggerHeight <= MaxHeightToSearch)//if height is above the biggest possible getting higher width
                        {
                            //getting bigger height and searching for a box with that height and width
                            BiggerHeight = GetBiggerHeight(Height, Width);
                            if (BiggerHeight < MaxHeightToSearch && BiggerHeight != 0)
                            {
                                if (StorageManagment.Instance.SearchBox(Width, BiggerHeight, out Box box))
                                    return box;
                            }
                            //if not finding a height increasing the width and then searching for heights inside that width
                            else
                            {
                                while (BiggerWidth <= MaxWidthToSearch)
                                {
                                    BiggerWidth = GetBiggerWidth(Width);
                                    if (BiggerWidth <= MaxWidthToSearch)
                                    {
                                        if (BiggerWidth == 0)
                                            return null;
                                        if (StorageManagment.Instance.SearchBox(BiggerWidth, Height, out Box box))
                                            return box;
                                        else
                                        {
                                            BiggerHeight = GetBiggerHeight(Height, BiggerWidth);
                                            if (BiggerHeight == 0)
                                                return null;
                                            if (BiggerHeight <= MaxHeightToSearch)
                                                if (StorageManagment.Instance.SearchBox(BiggerWidth, BiggerHeight, out Box B))
                                                    return B;
                                        }
                                        Width = BiggerWidth;
                                    }
                                }
                            }

                        }
                    }//if width not exists increasing the width to the next bigger
                    Width = GetBiggerWidth(Width);
                    if (Width == 0)
                        break;
                    BiggerWidth = Width;
                }
            }
            return null;
        }
        /// <summary>
        /// getting width the height and amount of boxes and returning a list with the best matches
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Stacko<Box> BuyAmount(double Width, double Height, int amount, out Stacko<int> BoxCounters)
        {
            Stacko<Box> BoxList = new Stacko<Box>();
            Stacko<int> Counters = new Stacko<int>();
            int counter = 0;
            //cant cross
            double MaxWidthToSearch = Width * StorageManagment.Instance.Config.Data.MaxWidthToSearch;
            double MaxHeightToSearch = Height * StorageManagment.Instance.Config.Data.MaxHeightToSearch;
            double BiggerHeight = 0.1;
            for (int i = 0; i < amount; i++)
            {
                //finding best box
                var b = FindBestBox(Width, Height);
                if (b != null)
                {
                    if (Width <= MaxWidthToSearch)
                    {
                        //adding it to the list and if the amount is 0 stop
                        BoxList.Push(b);
                        for (int j = 0; j < b.Quantity; j++)
                        {
                            amount--;
                            counter++;
                            if (amount == 0)
                                break;
                        }
                        Counters.Push(counter);
                        counter = 0;
                        //if the amount is not 0 getting a bigger height
                        BiggerHeight = GetBiggerHeight(b.Height, b.Width);
                        while (BiggerHeight <= MaxHeightToSearch && BiggerHeight != 0 && amount > 0)
                        {
                            if (BiggerHeight <= MaxHeightToSearch && StorageManagment.Instance.SearchBox(b.Width, BiggerHeight, out Box box))
                            {
                                //if bigger height fits adding it to the list until amount is 0 or until quantity ends
                                BoxList.Push(box);
                                for (int k = 0; k < box.Quantity; k++)
                                {
                                    amount--;
                                    counter++;
                                    if (amount == 0)
                                        break;
                                }
                                Counters.Push(counter);
                                counter = 0;
                                //if amount is still not 0 getting a bigger width and iterete through above
                                BiggerHeight = GetBiggerHeight(BiggerHeight, box.Width);
                            }
                        }
                    }
                    Width = GetBiggerWidth(b.Width);
                }
                else
                    break;
            }
            BoxCounters = Counters;
            return BoxList;
        }
        /// <summary>
        /// Getting a box and updating it's expiration date
        /// </summary>
        /// <param name="b"></param>
        private void UpdateExpirationDate(Box b)
        {
            b.LastBoughtDate = DateTime.Now;
            b.ExpirationDate = b.LastBoughtDate.AddDays(30);
        }
        private double GetBiggerHeight(double Height, double Width)
        {
            var node = StorageManagment.Instance.MainTree.GetNode(Width);
            var ClosestHeight = node.GetBiggerKey(Height);
            return ClosestHeight;
        }
        private double GetBiggerWidth(double Width)
        {
            var ClosestWidth = StorageManagment.Instance.MainTree.GetBiggerKey(Width);
            return ClosestWidth;
        }
    }
}
