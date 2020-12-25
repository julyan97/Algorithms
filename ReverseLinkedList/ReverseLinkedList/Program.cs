using System;

namespace ReverseLinkedList
{
    public class Node
    {
        public Node(string data, Node next)
        {
            Data = data;
            Next = next;
        }

        public string Data { get; set; }
        public Node Next { get; set; }



       
    }

    public class Program
    {
        public static void Reverse(ref Node head)
        {
            //a -> b -> c -> d -> null
            var prev = head;
            var cur = head.Next;

            while (cur != null)
            {
                var temp = cur.Next;
                cur.Next = prev;

                prev = cur;
                cur = temp;
            }
            head = prev;
        
        }
        static void Main(string[] args)
        {
            var node = new Node("a", null);
            var node1 = new Node("b", null);
            var node2 = new Node("c", null);
            var node3 = new Node("d", null);

            node.Next = node1;
            node1.Next = node2;
            node2.Next = node3;

            Reverse(ref node);

        }
    }
}
