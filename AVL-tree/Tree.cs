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
            return node.Right.Height - node.Left.Height;
        }
        private void FixHeight(Node node)
        {
            int hl = node.Left.Height,
                hr = node.Right.Height;
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
            Insert(root, key);
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
            else Console.WriteLine("Tree is empty!");
        }
        public void PrintTree()
        {
            PrintTree(root);
        }
    }
}
