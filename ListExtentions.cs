using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeProject
{
    public static class ListExtentions
    {
        public static string ToString<T>(this List<T> list, Func<T, string> func)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var e in list)
                sb.Append(func(e));
            return sb.ToString();  
        }
    }
}
