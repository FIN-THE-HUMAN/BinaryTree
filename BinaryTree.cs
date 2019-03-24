using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeProject
{
    public class Node<K, T> where K : IComparable
    {
        public K Key { get; set; }
        public T Data { get; set; }
        public Node<K, T> Left { get; set; }
        public Node<K, T> Right { get; set; }

        public Node(K key, T data)
        {
            Key = key;
            Data = data;
        }

        public void Insert(K key, T data)
        {
            if (key == null || key.CompareTo(Key) == 0) return;
            if (Key == null) Key = key;

            if (key.CompareTo(Key) < 0)
            {
                if (Left == null)
                {
                    Left = new Node<K, T>(key, data);
                    return;
                }
                Left.Insert(key, data);
            }

            if (key.CompareTo(Key) > 0)
            {
                if (Right == null)
                {
                    Right = new Node<K, T>(key, data);
                    return;
                }
                Right.Insert(key, data);
            }
        }
    }

    public class BinaryTree<K, T> where K : IComparable
    {
        public Node<K, T> Root { get; private set; }

        public BinaryTree()
        {

        }

        public BinaryTree(K key, T data)
        {
            Root = new Node<K, T>(key, data);
        }

        public void Insert(K key, T data)
        {
            if (Root == null) Root = new Node<K, T>(key, data);

            var temp = Root;
            if (temp.Key.CompareTo(key) == 0) return;

            if (key.CompareTo(temp.Key) < 0)
            {
                if (temp.Left == null) temp.Left = new Node<K, T>(key, data);
                temp.Left.Insert(key, data);
            }

            if (key.CompareTo(temp.Key) > 0)
            {
                if (temp.Right == null) temp.Right = new Node<K, T>(key, data);
                temp.Right.Insert(key, data);
            }
        }

        public void ShowKeys()
        {
            if (Root == null) { Console.WriteLine("-Пустое дерево-"); return; }
            Console.Write("Root:");
            _ShowKeys(Root, 0);
        }

        private void _ShowKeys(Node<K, T> temp, int i)
        {
            if (temp == null) return;
            Console.WriteLine(temp.Key.ToString()); i++;
            if (temp.Left != null) { Console.Write(new string('_', i) + "Left:"); _ShowKeys(temp.Left, i); }
            if (temp.Right != null) { Console.Write(new string('_', i) + "Right:"); _ShowKeys(temp.Right, i); }
        }

        public void ShowValues()
        {
            if (Root == null) { Console.WriteLine("-Пустое дерево-"); return; }
            Console.Write("Root:");
            _ShowValues(Root, 0);
        }

        private void _ShowValues(Node<K, T> temp, int i)
        {
            if (temp == null) return;
            Console.WriteLine(temp.Data.ToString()); i++;
            if (temp.Left != null) { Console.Write($"{i}) Left:"); _ShowValues(temp.Left, i); }
            if (temp.Right != null) { Console.Write($"{i}) Right:"); _ShowValues(temp.Right, i); }
        }

        private List<K> _GetKeys(Node<K, T> temp, List<K> keys)
        {
            if (temp == null) return keys;
            keys.Add(temp.Key);

            if (temp.Left != null) { _GetKeys(temp.Left, keys); }
            if (temp.Right != null) { _GetKeys(temp.Right, keys); }
            return keys;
        }

        public List<K> GetKeys()
        {
            if (Root == null) return null;
            List<K> keys = new List<K>();
            _GetKeys(Root, keys);
            return keys;
        }

        private List<T> _GetValues(Node<K, T> temp, List<T> values)
        {
            if (temp == null) return values;
            values.Add(temp.Data);

            if (temp.Left != null) { _GetValues(temp.Left, values); }
            if (temp.Right != null) { _GetValues(temp.Right, values); }
            return values;
        }

        public List<T> GetValues()
        {
            if (Root == null) return null;
            List<T> values = new List<T>();
            _GetValues(Root, values);
            return values;
        }

        private T _Find(K key, Node<K, T> temp, ref T result)
        {
            if (key.CompareTo(temp.Key) < 0)
            {
                if (temp.Left != null) result = _Find(key, temp.Left, ref result);
            }

            if (key.CompareTo(temp.Key) > 0)
            {
                if (temp.Right != null) result = _Find(key, temp.Right, ref result);
            }

            if (key.CompareTo(temp.Key) == 0) result = temp.Data;
            return result;
        }

        public T Find(K key)
        {
            T result = default(T);
            return _Find(key, Root, ref result);
        }
        
        private Node<K, T> _FindNode(K key, Node<K, T> temp, ref Node<K, T> result)
        {
            if (key.CompareTo(temp.Key) < 0)
            {
                if (temp.Left != null) result = _FindNode(key, temp.Left, ref result);
            }

            if (key.CompareTo(temp.Key) > 0)
            {
                if (temp.Right != null) result = _FindNode(key, temp.Right, ref result);
            }

            if (key.CompareTo(temp.Key) == 0) result = temp;
            return result;
        }

        private Node<K, T> FindNode(K key)
        {
            Node<K, T> result = null;
            return _FindNode(key, Root, ref result);
        }

        private Node<K, T> GetMinNode(Node<K, T> node)   //правильно
        {
            var temp = node;
            if (temp.Left == null) return temp;
            temp = GetMinNode(temp.Left);
            return temp;
        }

        private void _Delete(Node<K, T> parent, K key)
        {
            if (parent.Right != null)
            {
                if (key.CompareTo(parent.Right.Key) == 0)
                {
                    if (parent.Right.Right != null)
                    {
                        var min = GetMinNode(parent.Right.Right);
                        min.Left = parent.Right.Left;
                        parent.Right = parent.Right.Right;
                    }
                    else { parent.Right = parent.Right.Left; }
                }
                else if (key.CompareTo(parent.Right.Key) > 0)
                {
                    _Delete(parent.Right, key);
                }
                else if (key.CompareTo(parent.Right.Key) < 0 && key.CompareTo(parent.Key) > 0)
                {
                    _Delete(parent.Right, key);
                }
            }
            if (parent.Left != null)
            {
                if (key.CompareTo(parent.Left.Key) == 0)
                {
                    if (parent.Left.Right != null)
                    {
                        var min = GetMinNode(parent.Left.Right);
                        min.Left = parent.Left.Left;
                        parent.Left = parent.Left.Right;
                    }
                    else { parent.Left = parent.Left.Left; }
                }
                else if (key.CompareTo(parent.Left.Key) < 0)
                {
                    _Delete(parent.Left, key);
                }
                else if (key.CompareTo(parent.Left.Key) > 0 && key.CompareTo(parent.Key) < 0)
                {
                    _Delete(parent.Left, key);
                }
            }
        }

        public void Delete(K key)
        {
            if (Root == null) return;

            if (key.CompareTo(Root.Key) == 0)
            {
                if (Root.Right != null)
                {
                    var min = GetMinNode(Root.Right);
                    min.Left = Root.Left;
                    Root = Root.Right;
                }
                else if (Root.Left != null)
                {
                    Root = Root.Left;
                }
                else Root = null;
            }
            else
            {
                _Delete(Root, key);
            }
        }

        public string ToString(string keysFormat, string valuesFormat)
        {
            StringBuilder sb = new StringBuilder();
            var keys = GetKeys();
            var values = GetValues();
            for(int i = 0; i < keys.Count; i++)
            {
                sb.Append($"{string.Format(keysFormat, keys[i])} {string.Format(valuesFormat, values[i])}");
            }
            return sb.ToString();
        }

        private string _ToString(Node<K, T> temp, int i, Func<int, K, T, string> func, StringBuilder sb)
        {
            if (temp == null) return "";
            sb.Append(func(i, temp.Key,temp.Data)); i++;

            if (temp.Left != null) { _ToString(temp.Left, i, func, sb); }
            if (temp.Right != null) { _ToString(temp.Right, i, func, sb); }
            return sb.ToString();
        }

        public string ToString(Func<int, K, T, string> func)
        {
            if (Root == null) return "";
            StringBuilder sb = new StringBuilder();
            return _ToString(Root, 0, func, sb);
        }

        private Dictionary<int, List<KeyValuePair<K, T>>> _GetNodesByLevels(Node<K, T> temp, Dictionary<int, List<KeyValuePair<K, T>>> values, int i)
        {
            if (temp == null) return values;
            if (values.ContainsKey(i))
                values[i].Add(new KeyValuePair<K, T>(temp.Key, temp.Data));
            else
                values.Add(i, new List<KeyValuePair<K, T>>() { new KeyValuePair<K, T>(temp.Key, temp.Data)});
            i++;
            if (temp.Left != null) { _GetNodesByLevels(temp.Left, values, i); }
            if (temp.Right != null) { _GetNodesByLevels(temp.Right, values, i); }
            return values;
        }

        public Dictionary<int, List<KeyValuePair<K, T>>> GetNodesByLevels()
        {
            if (Root == null) return null;
            Dictionary<int, List<KeyValuePair<K, T>>> values = new Dictionary<int, List<KeyValuePair<K, T>>>();
            _GetNodesByLevels(Root, values, 0);
            return values;
        }

        public void ForEach(Action<K, T> action)
        {
            var keys = GetKeys();
            var values = GetValues();
            for(int i = 0; i < keys.Count; i++)
                action(keys[i], values[i]);
        }

        public void Clear()
        {
            Root = null;
        }

        //На основании индексов, которые получаются при обходе дерева в методе ShowKeys используя порядок элементов в GetKeys
        //можно организовать построение дерева в буферном экране и передать дерево в основной экран
        // в виде фигуры (ScreenShape).
        //Всё это может быть организовано в отдельном модуле адаптере для связи дерева с экраном

    }
}
