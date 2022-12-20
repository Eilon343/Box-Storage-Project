using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures;

namespace BoxModel
{
    public class StorageManagment
    {
        private Configuration _config = new Configuration();
        private MyQueue<Box> _expiredQueue = new MyQueue<Box>();
        private BinarySearchTree<double, BinarySearchTree<double, Box>> _maintree = new BinarySearchTree<double, BinarySearchTree<double, Box>>();
        private BinarySearchTree<double, Box> _inner;
        private static StorageManagment _instance;

        public Configuration Config
        {
            get { return _config; }
        }
        public MyQueue<Box> ExpiredQueue
        {
            get { return _expiredQueue; }
            set { _expiredQueue = value; }
        }
        public BinarySearchTree<double, BinarySearchTree<double, Box>> MainTree
        {
            get { return _maintree; }
        }
        public static StorageManagment Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StorageManagment();
                return _instance;
            }
        }
        private StorageManagment()
        {

        }
        public bool SearchBox(double Width, double Height, out Box b)
        {
            if (SearchWidth(Width, out _inner))
            {
                var node = MainTree.GetNode(Width);
                if (node.Search(Height))
                {
                    b = node.GetNode(Height);
                    return true;
                }
            }
            b = null;
            return false;
        }
        private bool SearchWidth(double Width, out BinarySearchTree<double, Box> inner)
        {
            if (_maintree.Search(Width))
            {
                inner = MainTree.GetNode(Width);
                return true;
            }
            inner = null;
            return false;
        }
        /// <summary>
        /// Adding a box to relevent data structers and returning a messege to the cutsomer if the box got to its max capacity
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string AddBox(Box b) //adding a box to the data structers, if the box is already exists, adding to it's quantity
            //if the box is not exists adding it to the relevent data structers(binary trees, queue)
        {
            bool found = SearchWidth(b.Width, out _inner);
            if (SearchBox(b.Width, b.Height, out Box B))
            {
                if (B.Quantity < _config.Data.MaxBoxes)
                {
                    B.Quantity++;
                }
            }
            else
            {
                if (found)
                {
                    _inner.AddNode(b.Height, b);
                }
                else
                {
                    _inner = new BinarySearchTree<double, Box>();
                    _maintree.AddNode(b.Width, _inner);
                    _inner.AddNode(b.Height, b);
                }
                _expiredQueue.Enqueue(b);

            }

            return $"The box with Width: {b.Width} and Height {b.Height} exceed the max amount so we giving it back";
        }
        /// <summary>
        /// Removing given box from the binary tree
        /// </summary>
        /// <param name="b"></param>
        public void RemoveBox(Box b) //getting the node of the main tree, removing the height from that secondry tree,
            //if the root of that height which is the width is null then removing the node from the main tree
        {
            var node = _maintree.GetNode(b.Width);
            node.Remove(b.Height);
            if (node.Root == null)
                _maintree.Remove(b.Width);
        }
    }
}

