using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
  abstract class List
  {
    protected class ListElement
    {
      public int elem;
      public ListElement prev, next;

      public ListElement(int Elem)
      {
        elem = Elem;
      }

      public static ListElement operator ++(ListElement le) => le.next;
      public static ListElement operator --(ListElement le) => le.prev;
      public override string ToString() => Convert.ToString(elem);
    }
    protected ListElement start, end;

    public virtual bool Search(int elem)
    {
      return true;
    }
    public virtual bool Insert(int elem)
    {
      return true;
    }
    public virtual bool Delete(int elem)
    {
      return true;
    }

    public void Print()
    {
      for (ListElement tmp = start; tmp != null; tmp++)
        Console.WriteLine(tmp);
    }

    /// <summary>
    /// Hilfsmethode zum Suchen
    /// </summary>
    /// <param name="elem"></param>
    /// <returns></returns>
    protected ListElement _search(int elem)
    {
      for (ListElement tmp = start; tmp != null; tmp++)
      {
        if (elem == tmp.elem)
          return tmp;
      }
      return null;
    }

    protected ListElement FindElemIndex(int index)
    {
      ListElement tmp = start;

      for (int i = 0; i < index; i++)
        tmp++;

      return tmp;
    }
    public int this[int Index]
    {
      get
      {
        return FindElemIndex(Index).elem;
      }

      set
      {
        FindElemIndex(Index).elem = value;
      }
    }

    public IEnumerator GetEnumerator()
    {
      for (ListElement tmp = start; tmp != null; tmp++)
        yield return tmp.elem;
    }
  }

  abstract class Array
  {
    protected const int n = 3;
    public int[] array = new int[n];

    public int this[int Index]
    {
      get
      {
        return array[Index];
      }

      set
      {
        array[Index] = value;
      }
    }

    protected int _search(int elem)
    {
      int index;
      for (index = 0; index < array.Length; index++)
      {
        if (array[index] == elem)
          return index;
      }

      return -1;
    }

    public void Print()
    {
      foreach (int elem in array)
      {
        Console.WriteLine(elem);
      }
    }
  }

  abstract class BinaryTree
  {
    internal class Node
    {
      public int data;
      public int priority;

      public Node left { get; set; }
      public Node right { get; set; }
      public Node parent { get; set; }

      public Node() { }

      public Node(int Data)
      {
        data = Data;
      }

      public Node(int Data, int Priority)
      {
        data = Data;
        priority = Priority;
      }


      public override string ToString() => Convert.ToString(data);
    }

    protected Node root;

    protected Node _search(int data)          //Sucht Element
    {
      Node tmp = root;
      while (tmp != null && tmp.data != data)
      {
        if (data < tmp.data)
        {
          tmp = tmp.left;
        }
        else
        {
          tmp = tmp.right;
        }
      }
      return tmp;
    }

    protected Node _searchPosAbove(int data)  //Sucht Position des Vorgängers
    {
      Node above = null;
      Node tmp = root;

      while (tmp != null)
      {
        if (tmp.data == data)                 //Falls Element bereits existiert
          return above;

        if (data < tmp.data)
        {
          if (tmp.left == null)
            return tmp;

          above = tmp;
          tmp = tmp.left;
        }
        else
        {
          if (tmp.right == null)
            return tmp;

          above = tmp;
          tmp = tmp.right;
        }
      }

      return tmp;
    }

    protected void Inorder(Node n)
    {
      if (n == null)
        return;

      Inorder(n.left);
      Console.Write(n + " ");
      Inorder(n.right);
    }

    protected void Preorder(Node n)
    {
      if (n == null)
        return;

      Console.Write(n + " ");
      Preorder(n.left);
      Preorder(n.right);
    }

    protected void Postorder(Node n)
    {
      if (n == null)
        return;

      Postorder(n.left);
      Postorder(n.right);
      Console.Write(n + " ");
    }
  }

  abstract class HashFkt : Array
  {

  }
}
