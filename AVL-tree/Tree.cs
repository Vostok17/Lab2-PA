using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_tree
{
    internal class Tree
    {
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
    }
}
