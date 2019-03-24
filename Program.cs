using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeProject
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int, List<bool>> t = new BinaryTree<int, List<bool>>();
            t.Insert(10, new List<bool>() { true, false});
            t.Insert(11, new List<bool>() { true, false });
            t.Insert(9, new List<bool>() { true, false });
            StringBuilder sb = new StringBuilder();
            t.ForEach((i, e) => sb.AppendLine(e.ToString()));
            Console.WriteLine(sb.ToString());
            Console.ReadKey();
        }
    }
}
