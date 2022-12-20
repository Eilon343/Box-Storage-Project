using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class MyQueue<T>
    {
        private int _index;
        public DoubleLinkedList<T> List { get; set; }
        public MyQueue()
        {
            List = new DoubleLinkedList<T>();
        }
        public void Enqueue(T item)
        {
            List.AddFirst(item);
            _index++;
        }
        public void Dequeue()
        {
             List.RemoveFirst();
            _index--;
        }
        public void MoveToLast(T item)
        {
            List.RemoveAt(item);
            List.AddLast(item);
        }
        public T Peek()
        {
            if (List.Head != null)
            {
                return List.Head.Data;
            }
            return default;
        }
    }
}
