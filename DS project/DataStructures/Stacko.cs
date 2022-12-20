using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class StackNode<V> 
    {
        private V data;
        private StackNode<V> next;
        public StackNode(V data, StackNode<V> next)
        {
            this.data = data;
            this.next = next;
        }
        public V Data
        {
            get { return data; }
            set { data = value; }
        }
        public StackNode<V> Next
        {
            get { return next; }
            set { next = value; }
        }

    }
    public class Stacko<V> : IEnumerable<V>
    {
        private StackNode<V> top;
        public int Count { get; set; }
        public Stacko()
        {
            top = null;
        }
        public bool IsEmpty()
        {
            return top == null;
        }
        public void Push(V value)
        {
            top = new StackNode<V>(value, top);
            Count++;
        }
        public V Pop()
        {
            if (IsEmpty() == false)
            {
                V temp = top.Data;
                top = top.Next;
                Count--;
                return temp;
            }
            return top.Data;
        }
        public IEnumerator<V> GetEnumerator()
        {
            return new StackEnumerator(top, Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public struct StackEnumerator : IEnumerator<V>, IEnumerator
        {
            private StackNode<V> head;
            private StackNode<V> currentLink;
            private int length;
            private bool startedFlag;

            public StackEnumerator(StackNode<V> head, int length)
            {
                this.head = head;
                this.currentLink = null;
                this.length = length;
                this.startedFlag = false;
            }

            public V Current
            {
                get { return this.currentLink.Data; }
            }

            public void Dispose()
            {
                this.head = null;
                this.currentLink = null;
            }

            object IEnumerator.Current
            {
                get { return this.currentLink.Data; }
            }

            public bool MoveNext()
            {
                if (this.startedFlag == false)
                {
                    this.currentLink = this.head;
                    this.startedFlag = true;
                }
                else
                {
                    this.currentLink = this.currentLink.Next;
                }

                return this.currentLink != null;
            }
            public void Reset()
            {
                this.currentLink = this.head;
            }
        }
    }
}
