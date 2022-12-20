using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Link<T>
    {
        public Link<T> Next { get; set; }
        public Link<T> Previous { get; set; }
        public T Data { get; set; }
        public Link(T data, Link<T> next, Link<T> previous)
        {
            this.Data = data;
            this.Next = next;
            this.Previous = previous;
        }
    }
    public class DoubleLinkedList<T> : IEnumerable<T>, IEnumerable
    {
        private Link<T> head;
        private Link<T> tail;
        public Link<T> Head
        {
            get { return head; }
            set { head = value; }
        }
        public Link<T> Tail
        {
            get { return tail; }
            set { tail = value; }
        }
        public int Length { get; private set; }

        public DoubleLinkedList()
        {
            this.head = this.tail = null;
            this.Length = 0;
        }
        public void AddLast(T item)
        {
            if (head == null)
            {
                // Becase head is null, the list is empty, so create the first new link
                // and set that as the head and tail. At this point, head and tail 
                // should be null and Length should be 0
                this.head = new Link<T>(item, null, null);
                this.tail = head;
                this.Length++;
            }
            else
            {
                Link<T> newLink = new Link<T>(item, null, this.tail);
                this.tail.Next = newLink;

                this.tail = newLink;
                this.Length++;
            }
        }
        public void AddFirst(T item)
        {
            if (head == null)
            {
                // Becase head is null, the list is empty, so create the first new link
                // and set that as the head and tail. At this point, head and tail 
                // should be null and Length should be 0
                this.head = new Link<T>(item, null, null);
                this.tail = head;
                this.Length++;
            }
            else
            {
                Link<T> newLink = new Link<T>(item, this.head, null);
                this.head.Previous = newLink;
                this.head = newLink;
                this.Length++;
            }
        }
        public void RemoveFirst()
        {
            if (head != null)
            {
                if (this.Length == 1)
                {
                    this.head = null;
                    this.tail = null;
                    this.Length--;
                }
                else
                {
                    this.head.Next.Previous = null;
                    this.head = this.head.Next;
                    this.Length--;
                }
            }
        }
        public void RemoveLast()
        {
            if (head != null)
            {
                if (this.Length == 1)
                {
                    this.head = null;
                    this.tail = null;
                    this.Length--;
                }
                else
                {
                    this.tail.Previous.Next = null;
                    this.tail = this.tail.Previous;
                    this.Length--;
                }
            }
        }
        //need to remove item from a list
        public void RemoveAt(T item)
        {
            Link<T> curr = head, prev = null;
            while (curr.Data.Equals(item) == false && head != null)
            {
                prev = curr;
                curr = curr.Next;
            }
            if (curr != null)
            {
                if (prev == null)
                    head = head.Next;
                else
                    prev.Next = curr.Next;
            }
            Length--;
        }
     
        private DoubleLinkedList<T>.LinkedListEnumerator GetEnumerator()
        {
            return new LinkedListEnumerator(this.head, this.tail, this.Length);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        /// <summary>
        /// Linked list enumerator
        /// </summary>
        public struct LinkedListEnumerator : IEnumerator<T>, IEnumerator
        {
            private Link<T> head;
            private Link<T> tail;
            private Link<T> currentLink;
            private int length;
            private bool startedFlag;

            public LinkedListEnumerator(Link<T> head, Link<T> tail, int length)
            {
                this.head = head;
                this.tail = tail;
                this.currentLink = null;
                this.length = length;
                this.startedFlag = false;
            }

            public T Current
            {
                get { return this.currentLink.Data; }
            }

            public void Dispose()
            {
                this.head = null;
                this.tail = null;
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
