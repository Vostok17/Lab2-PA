using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_tree
{
    internal class Node
    {
        public Node(int key)
        {
            Key = key;
            Left = Right = null;
            Height = 1;
        }
        public int Key {  get; set; }
        public Node Left {  get; set; }
        public Node Right {  get; set; }
        public int Height {  get; set; }
    }
}
