using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drzewo
{
    class Program
    {
        static void Main(string[] args)
        {
            var n9 = new Node(9);
            var n8 = new Node(8);
            var n7 = new Node(7);
            n7.AddChildreen(new Node[] { n8, n9 });
            var n6 = new Node(6);
            var n5 = new Node(5);
            var n4 = new Node(4);
            n4.AddChildreen(new Node[] { n5, n6, n7 });
            var n3 = new Node(3);
            var n2 = new Node(2);
            var n1 = new Node(1);
            var n1a = new Node(1);
            n1.AddChildreen(new Node[] { n2, n3 , n4});
            var n0 = new Node(0);
            n0.AddChildreen(new Node[] {n1, n1a});
            var Tree = new Tree(n0);

            Console.WriteLine("Od dołu do góry: " + Tree.WriteTreeValuesDown());
            Console.WriteLine("Od lewej do prawej: " + Tree.WriteTreeValuesHorizontaly());
        }
    }

    class Tree
    {
        //Referencja do korzenia
        public Node MainNode { get; set; }

        public Tree(Node mainNode)
        {
            MainNode = mainNode;
        }

        //Wypisywanie dzieci od dołu
        public string WriteTreeValuesDown()
        {
            var result = this.MainNode.WriteChildreens();

            return result;
        }

        //Wypisywanie dzieci wpoprzek
        public string WriteTreeValuesHorizontaly()
        {
            var sb = new StringBuilder();
            var Queue = new List<Queue>();
            int x = 0;

            this.MainNode.AddToQueue(Queue, x);
            Queue.Add(new Queue(this.MainNode.Value,x));

            int maxLevel = 0;

            foreach (var item in Queue.ToArray())
            {
                if (item.level > maxLevel)
                {
                    maxLevel = item.level;
                }
            }

            while (maxLevel >= 0)
            {
                foreach (var item in Queue.ToArray())
                {
                    if (item.level == maxLevel)
                        sb.Append(item.value + " ");
                }

                maxLevel--;
            }

            return sb.ToString();
        }
    }

    class Node
    {
        //Przechowywana wartość w węźle
        public int Value { get; set; }

        //Referencja do rodzica
        public Node Parent { get; set; }

        //Referencje do dzieci
        public List<Node> Childreen { get; set; } = new List<Node>();

        public Node(int value)
        {
            Value = value;
        }

        //Dodawanie dziecka na urzytek wenętrzny
        private void AddChild (Node child)
        {
            Childreen.Add(child);
            child.Parent = this;
        }

        //Dodawanie dziecka
        public void AddChildreen(Node[] ToAppend)
        {
            foreach (var item in ToAppend)
            {
                this.AddChild(item);
            }
        }

        public string WriteChildreens()
        {
            var sb = new StringBuilder();

            if (Childreen.Count > 0)
            {
                foreach (var item in this.Childreen)
                {
                    sb.Append(item.WriteChildreens());
                }
            }

            sb.Append(this.Value + " ");

            return sb.ToString();
        }

        public Queue AddToQueue(List<Queue> list, int level)
        {
            if (Childreen.Count > 0)
            {
                foreach (var item in this.Childreen)
                {
                    int lvl = level + 1;
                    list.Add(item.AddToQueue(list, lvl));
                }
            }

            return new Queue(this.Value, level);
        }
    }

    class Queue
    {

        public int value;
        public int level;

        public Queue(int value, int level)
        {
            this.value = value;
            this.level = level;
        }
    }
}
