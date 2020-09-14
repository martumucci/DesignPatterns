using System;
using static System.Console;

namespace Iterator.IteratorObject
{
    //  It is an object that facilitates the traversal of a data structure.

    //  An Iterator specified how you can traverse an object
    //  Generally, an IEnumerable<T> returning method is enough

    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;
            left.Parent = right.Parent = this;
        }
    }

    public class InOrderIterator<T> // C++ style
    {
        private readonly Node<T> root;
        public Node<T> Current;
        private bool yieldedStart;
        public InOrderIterator(Node<T> root)
        {
            this.root = root;
            Current = root;
            while (Current.Left != null)
            {
                Current = Current.Left;
            }
        }

        public bool MoveNext()
        {
            if (!yieldedStart)
            {
                yieldedStart = true;
                return true;
            }

            if(Current.Right != null)
            {
                Current = Current.Right;
                while (Current.Left != null)
                    Current = Current.Left;
                return true;
            }
            else
            {
                var p = Current.Parent;
                while(p != null && Current == p.Right)
                {
                    Current = p;
                    p = p.Parent;
                }
                Current = p;
                return Current != null;
            }
        }

        public void Reset()
        {
            Current = root;
            yieldedStart = false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //   1
            // 2   3

            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));
            var it = new InOrderIterator<int>(root);
            while (it.MoveNext())
            {
                Write(it.Current.Value);
                Write(',');
            }
            WriteLine();

        }
    }
}
