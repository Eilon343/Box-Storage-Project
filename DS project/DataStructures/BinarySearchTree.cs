using DataStructures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxModel
{
    public class BinarySearchTree<K, V>: IEnumerable where K : IComparable<K>
    {
        public class NestedNode
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public NestedNode Left { get; set; }
            public NestedNode Right { get; set; }
            public NestedNode(K key, V value, NestedNode t)
            {
                Key = key;
                Value = value;
            }
        }
        private NestedNode root;
        public NestedNode Root
        {
            get { return root; }
        }

        public BinarySearchTree()
        {
            root = null;
        }
        public void AddNode(K key, V value)
        {
            if (root == null)
                root = new NestedNode(key, value, root);
            else
                AddNode(key, value, root);
        }
        private void AddNode(K key, V Value, NestedNode t)
        {
            var CompareResult = key.CompareTo(t.Key);
            if (CompareResult >= 0)
            {
                if (t.Right == null)
                    t.Right = new NestedNode(key, Value, t);
                else
                    AddNode(key, Value, t.Right);
            }
            else
            {
                if (t.Left == null)
                    t.Left = new NestedNode(key, Value, t);
                else
                    AddNode(key, Value, t.Left);
            }
        }
        public bool Search(K key)
        {
            return Search(key, root);
        }
        private bool Search(K key, NestedNode t)
        {
            if (t == null)
                return false;
            var CompareResult = key.CompareTo(t.Key);
            if (CompareResult == 0)
                return true;
            if (CompareResult < 0)
                return Search(key, t.Left);
            else
                return Search(key, t.Right);
        }
        public V GetNode(K key)
        {
            return GetNode(key, root);
        }
        private V GetNode(K key, NestedNode t)
        {
            if (t == null)
                return default;
            if (key.CompareTo(t.Key) == 0)
                return t.Value;
            if (key.CompareTo(t.Key) < 0)
                return GetNode(key, t.Left);
            else
                return GetNode(key, t.Right);
        }
        public K GetMinimum(NestedNode curr)
        {
            K min = curr.Key;
            while (curr.Left != null)
            {
                min = curr.Left.Key;
                curr = curr.Left;
            }
            return min;
        }
        public void Remove(K Key)
        {
            root = Remove(root, Key);
        }
        private NestedNode Remove(NestedNode Parent, K key)
        {
            if (Parent == null)
                return Parent;

            if (key.CompareTo(Parent.Key) < 0)
                Parent.Left = Remove(Parent.Left, key);
            else if (key.CompareTo(Parent.Key) > 0)
                Parent.Right = Remove(Parent.Right, key);
            else
            {
                if (Parent.Left == null)
                    return Parent.Right;
                else if (Parent.Right == null)
                    return Parent.Left;

                Parent.Key = GetMinimum(Parent.Right);
                Parent.Right = Remove(Parent.Right, Parent.Key);
            }
            return Parent;
        }
        public K GetBiggerKey(K key)
        {
            return GetBiggerKey(key, root, root);
        }
        private K GetBiggerKey(K key, NestedNode tempNode, NestedNode node)
        {
            int comper = node.Key.CompareTo(key);

            if (comper > 0)
            {
                if (node.Left != null)
                {
                    tempNode = node;
                    return GetBiggerKey(key, tempNode, node.Left);
                }
                return node.Key;

            }
            //if (comper<0)

            if (node.Right != null)
            {
                return GetBiggerKey(key, tempNode, node.Right);
            }
            if (tempNode.Key.CompareTo(key) > 0)
            {
                return tempNode.Key;
            }
            //if(tempNode.Key.CompareTo(key)>0

            return default;
        }
        public IEnumerable<V> ValueEnumerator()
        {
            Stacko<NestedNode> stack = new Stacko<NestedNode>();
            NestedNode t = this.root;
            while (stack.Count > 0 || t != null)
            {
                //if node is found, inserting it to stack and going left
                if (t != null)
                {
                    stack.Push(t);
                    t = t.Left;
                }
                //if node is null, deleting last node from stack, returning it's key, and going right
                else
                {
                    t = stack.Pop();
                    yield return t.Value;
                    t = t.Right;
                }
            }
            yield break;
        }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
