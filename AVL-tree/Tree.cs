﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_tree
{
    internal class Tree<T>
    {
        private Node<T> root;
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
            root = Insert(root, key, value);
        }
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
            if (root == null)
            {
                Console.WriteLine("Tree is empty!");
            }
            else
            {
                PrintTree(root);
            }
        }
        public void Search(int key)
        {
            Node<T> foundNode = Search(root, key);
            if (foundNode != null)
            {
                Console.WriteLine($"Key = {foundNode.Key}, value = {foundNode.Value}");
            }
            else
            {
                Console.WriteLine("Not found!");
            }
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
        private Node<T> FindMin(Node<T> node)
        {
            return (node.Left != null) ? FindMin(node.Left) : node;
        }
        private Node<T> RemoveMin(Node<T> node)
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
                if (right == null)
                {
                    return left;
                }
                Node<T> min = FindMin(right); 
                min.Right = RemoveMin(right);
                min.Left = left;
                return Balance(min);

            }
            return Balance(node);
        }
    }
}
