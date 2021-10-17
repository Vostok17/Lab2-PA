using System;

namespace AVL_tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tree<string> tree = new();
            tree.Add(22);
            tree.Add(17);
            tree.Add(12);
            tree.Add(18);
            tree.Add(23);
            tree.Add(25);
            tree.PrintTree();

            tree.Remove(17);
            tree.PrintTree();

            tree.Search(18);
            tree.PrintTree();

            Console.ReadKey();
        }
    }
}
