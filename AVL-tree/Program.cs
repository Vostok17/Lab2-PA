using System;

namespace AVL_tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int NUM_OF_CELLS = 10000;
            FileManager fm = new FileManager(@"C:\Users\Artem\Desktop\ПА\Lab2\AVL-tree\files\data.txt", NUM_OF_CELLS);
            //fm.GenerateDataFile();

            Tree<string> tree = new();
            fm.FillTree(tree);

            //tree.PrintTree();
            tree.Search(45);

            tree.Remove(45);
            tree.Search(45);

            Console.ReadKey();
        }
    }
}
