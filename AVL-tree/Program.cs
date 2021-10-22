using System;

namespace AVL_tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int NUM_OF_CELLS = 10000;
            FileManager fm = new FileManager(@"C:\Users\Artem\Desktop\ПА\Lab2\AVL-tree\files\", NUM_OF_CELLS);
            //fm.GenerateDataFile();

            Tree<string> tree = new();
            fm.FillTree(tree);

            int keyToOperate = 0;
            //tree.PrintTree();
            tree.Search(keyToOperate);

            tree.Remove(keyToOperate);
            tree.Search(keyToOperate);

            fm.Write(tree);

            Console.ReadKey();
        }
    }
}

// for 15 tests

/*Tree<string> tree = new();
            fm.FillTree(tree);

            Random rnd = new();
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("{0} test:", i + 1);
                tree.Search(rnd.Next(0,NUM_OF_CELLS));
                Console.WriteLine("Compares: {0}", tree.Compares);
                Console.WriteLine(new string('=', 30));
            }*/