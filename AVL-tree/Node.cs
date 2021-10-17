using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_tree
{
    internal class Node<T>
    {
        public Node(int key, T value)
        {
            Key = key;
            Value = value;
            Left = Right = null;
            Height = 1;
        }
        public int Key {  get; set; }
        public T Value { get; set; }
        public Node<T> Left {  get; set; }
        public Node<T> Right {  get; set; }
        public int Height {  get; set; }
    }
}
