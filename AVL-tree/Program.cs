using System;

namespace AVL_tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new();
            tree.Add(45);
            tree.PrintTree();

            Console.ReadKey();
        }
    }
}
