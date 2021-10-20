using System;
using System.Diagnostics;

namespace AVL_tree
{
    internal class Tree<T>
    {
        public Node<T> Root { get; private set; }
        // Height and balance factor
        private int BalanceFactor(Node<T> node)
        {
            return Height(node.Right) - Height(node.Left);
        }
        private int Height(Node<T> node)
        {
            return (node != null) ? node.Height : 0;
        }
        private void FixHeight(Node<T> node)
        {
            int hl = Height(node.Left),
                hr = Height(node.Right);
            node.Height = (hl > hr ? hl : hr) + 1;
        }
        // Rotation and balance
        private Node<T> RotateRight(Node<T> p)
        {
            Node<T> q = p.Left;
            p.Left = q.Right;
            q.Right = p;
            FixHeight(p);
            FixHeight(q);
            return q;
        }
        private Node<T> RotateLeft(Node<T> q)
        {
            Node<T> p = q.Right;
            q.Right = p.Left;
            p.Left = q;
            FixHeight(q);
            FixHeight(p);
            return p;
        }
        private Node<T> Balance(Node<T> node)
        {
            FixHeight(node);
            if (BalanceFactor(node) == 2) // right tree unbalanced
            {
                if (BalanceFactor(node.Right) < 0) // left child is higher
                {
                    node.Right = RotateRight(node.Right);
                }
                return RotateLeft(node);
            }
            if (BalanceFactor(node) == -2) // left tree unbalanced
            {
                if (BalanceFactor(node.Left) > 0) // right child is higher
                {
                    node.Left = RotateLeft(node.Left);
                }
                return RotateRight(node);
            }
            return node; // no need to balance
        }
        // Add
        private Node<T> Insert(Node<T> node, int key, T value)
        {
            if (node == null) return new Node<T>(key, value);
            if (key < node.Key)
            {
                node.Left = Insert(node.Left, key, value);
            }
            else
            {
                node.Right = Insert(node.Right, key, value);
            }
            return Balance(node);
        }
        public void Add(int key, T value)
        {
            Root = Insert(Root, key, value);
        }
        // Print
        private void PrintTree(Node<T> node, string indent = "", Side? side = null)
        {
            if (node != null)
            {
                string nodeSide = (side == null) ? "+" : (side == Side.Left) ? "L" : "R";
                Console.WriteLine($"{indent} [{nodeSide}]- {node.Key}");
                indent += new string(' ', 3);
                PrintTree(node.Left, indent, Side.Left);
                PrintTree(node.Right, indent, Side.Right);
            }
        }
        public void PrintTree()
        {
            if (Root == null)
            {
                Console.WriteLine("Tree is empty!");
            }
            else
            {
                PrintTree(Root);
            }
        }
        // Search
        public void Search(int key)
        {
            time.Start();
            Console.WriteLine("Looking for key {0}...", key);

            Node<T> foundNode = Search(Root, key);
            if (foundNode != null)
            {
                Console.WriteLine("Found!");
                Console.WriteLine($"key = {foundNode.Key}, value = {foundNode.Value}");
            }
            else
            {
                Console.WriteLine("Not found!");
            }

            time.Stop();
            Console.WriteLine("Elapsed time {0} ms\n", time.Elapsed.TotalMilliseconds);
            time.Reset();
        }
        private Node<T> Search(Node<T> node, int key)
        {
            if (node == null)
            {
                return null;
            }
            if (key == node.Key)
            {
                return node;
            }
            else if (key < node.Key)
            {
                return Search(node.Left, key);
            }
            else
            {
                return Search(node.Right, key);
            }
        }
        // Remove
        private Node<T> FindMin(Node<T> node)
        {
            return (node.Left != null) ? FindMin(node.Left) : node;
        }
        private Node<T> RemoveMin(Node<T> node)
        {
            if (node.Left == null)
            {
                return node.Right; // becomes left child of the previous node
            }
            node.Left = RemoveMin(node.Left);
            return Balance(node);
        }
        public void Remove(int key)
        {
            time.Start();
            Console.WriteLine("Removing key {0}...", key);

            Root = Remove(Root, key);

            Console.WriteLine("Elapces time {0} ms\n", time.Elapsed.TotalMilliseconds);
            time.Stop();
        }
        private Node<T> Remove(Node<T> node, int key)
        {
            if (node == null)
            {
                return null;
            }
            if (key < node.Key)
            {
                node.Left = Remove(node.Left, key);
            }
            else if (key > node.Key)
            {
                node.Right = Remove(node.Right, key);
            }
            else
            {
                Node<T> left = node.Left,
                     right = node.Right;
                if (right == null) // if right subtree is empty, then in left could be only one node (tree is balanced)
                {                  // so its okey to replace current node with left (even if it's null)
                    return left;
                }
                // take the smallest key in right subtree and replace current node
                Node<T> min = FindMin(right);
                min.Right = RemoveMin(right); // right subtree without min key
                min.Left = left;
                return Balance(min);

            }
            return Balance(node);
        }
        private Stopwatch time = new Stopwatch();
    }
}
