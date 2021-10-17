using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_tree
{
    internal class Tree
    {
        private Node root;
        private int BalanceFactor(Node node)
        {
            return Height(node.Right) - Height(node.Left);
        }
        private int Height(Node node)
        {
            return (node != null) ? node.Height : 0;
        }
        private void FixHeight(Node node)
        {
            int hl = Height(node.Left),
                hr = Height(node.Right);
            node.Height = (hl > hr ? hl : hr) + 1;
        }
        private Node RotateRight(Node p)
        {
            Node q = p.Left;
            p.Left = q.Right;
            q.Right = p;
            FixHeight(p);
            FixHeight(q);
            return q;
        }
        private Node RotateLeft(Node q)
        {
            Node p = q.Right;
            q.Right = p.Left;
            p.Left = q;
            FixHeight(q);
            FixHeight(p);
            return p;
        }
        private Node Balance(Node node)
        {
            FixHeight(node);
            if (BalanceFactor(node) == 2)
            {
                if (BalanceFactor(node.Right) < 0)
                {
                    node.Right = RotateRight(node.Right);
                }
                return RotateLeft(node);
            }
            if (BalanceFactor(node) == -2)
            {
                if (BalanceFactor(node.Left) > 0)
                {
                    node.Left = RotateLeft(node.Left);
                }
                return RotateRight(node);
            }
            return node;
        }
        private Node Insert(Node node, int key)
        {
            if (node == null) return new Node(key);
            if (key < node.Key)
            {
                node.Left = Insert(node.Left, key);
            }
            else
            {
                node.Right = Insert(node.Right, key);
            }
            return Balance(node);
        }
        public void Add(int key)
        {
            root = Insert(root, key);
        }
        private void PrintTree(Node node, string indent = "", Side? side = null)
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
            if (root == null)
            {
                Console.WriteLine("Tree is empty!");
            }
            else
            {
                PrintTree(root);
            }
        }
        private Node FindMin(Node node)
        {
            return (node.Left != null) ? FindMin(node.Left) : node;
        }
        private Node RemoveMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            node.Left = RemoveMin(node.Left);
            return Balance(node);
        }
        public void Remove(int key)
        {
            root = Remove(root, key);
        }
        private Node Remove(Node node, int key)
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
                Node left = node.Left,
                     right = node.Right;
                if (right == null)
                {
                    return left;
                }
                Node min = FindMin(right); 
                min.Right = RemoveMin(right);
                min.Left = left;
                return Balance(min);

            }
            return Balance(node);
        }
    }
}
