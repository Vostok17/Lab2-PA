using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AVL_tree
{
    internal class FileManager
    {
        public FileManager(string path, int size)
        {
            this.path = path;
            this.size = size;
            filename = "data.txt";
        }
        private string path;
        private string filename;
        private int size;
        private string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-#*><";
        Stopwatch time = new Stopwatch();

        public void GenerateDataFile()
        {
            // mix keys
            Random rnd = new Random();
            int[] keys = new int[size];
            for (int i = 0; i < size; i++)
                keys[i] = i;
            double[] order = new double[size];
            for (int ctr = 0; ctr < order.Length; ctr++)
                order[ctr] = rnd.NextDouble();
            Array.Sort(order, keys);
            // write 
            const int VALUE_LENGHT = 4;
            using (StreamWriter sw = new StreamWriter(path + filename))
            {
                for (int ctr = 0; ctr < size; ctr++)
                {
                    // generate value
                    string value = "";
                    for (int i = 0; i < VALUE_LENGHT; i++)
                    {
                        value += allowedChars[rnd.Next(0, allowedChars.Length)];
                    }
                    sw.WriteLine($"{keys[ctr]}, {value}");
                }
            }

        }
        public void FillTree(Tree<string> tree)
        {
            Console.WriteLine("Building tree...");
            time.Start();
            using (StreamReader sr = new StreamReader(path + filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] nodeData = line.Split(',');
                    int key = Convert.ToInt32(nodeData[0]);
                    string value = Convert.ToString(nodeData[1].Trim());
                    tree.Add(key, value);
                }
            }
            time.Stop();
            Console.WriteLine("Elapsed time: {0} ms\n", time.ElapsedMilliseconds);
        }
        public void Write(Tree<string> tree)
        {
            using (StreamWriter sw = new StreamWriter(path + filename))
            {
                Queue<Node<string>> queue = new();
                queue.Enqueue(tree.Root);
                while (queue.Count != 0)
                {
                    Node<string> node = queue.Dequeue();
                    sw.WriteLine($"{node.Key}, {node.Value}");
                    if (node.Left != null)
                    {
                        queue.Enqueue(node.Left);
                    }
                    if (node.Right != null)
                    {
                        queue.Enqueue(node.Right);
                    }
                }
            }
        }
    }
}
