using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
  class BinSearchTree : BinaryTree, ISetSorted
  {
    public BinSearchTree()    //Ich glaub das ist eh default-Einstellung (Eugen)
    {
      root = null;
    }

    public void Print()
    {
      Node current = root;

      if (current != null)
      {
        Inorder(current);
        Console.WriteLine();
        Preorder(current);
        Console.WriteLine();
        Postorder(current);
        Console.WriteLine();
      }
    }
    public virtual bool Search(int elem)
    {
      if (_search(elem) != null)
        return true;

      return false;
    }


    public virtual bool Insert(int data)
    {
      Node newItem = new Node(data);

      if (root == null)
      {
        root = newItem;
        return true;
      }
      else
      {
        Node current = root;

        Node parent = null;
        while (current != null)
        {
          parent = current;
          if (data < current.data)
          {
            current = current.left;
            if (current == null)//if the current.left is null
            {
              parent.left = newItem;//make parent.left store the new node
              return true;
            }
          }
          else
          {
            current = current.right;
            if (current == null)
            {
              parent.right = newItem;
              return true;
            }
          }
        }
      }
      return false;
    }

    public virtual bool Delete(int data)
    {
      if (root == null)
      {
        return false;
      }

      Node current = _search(data);
      if (current == null) return false;
      Node parent = _searchPosAbove(data);

      //Case 1: The current has no children
      if (current.left == null && current.right == null)
      {
        if (parent == null)
        {
          root = null;

        }
        else if (parent.data > current.data)
        {
          parent.left = null;
        }
        else if (parent.data < current.data)
        {
          parent.right = null;
        }
        current = null;
      }

      //Case 2: The current has no left child
      else if (current.left == null)
      {
        if (parent == null)
        {
          root = current.right;
        }

        else
        {
          if (parent.data > current.data)
          {
            parent.left = current.right;
          }
          else if (parent.data < current.data)
          {
            parent.right = current.right;
          }
        }
      }

      //Case 3: The current has no right child
      else if (current.right == null)
      {
        if (parent == null)
        {
          root = current.left;
        }

        else
        {
          if (parent.data > current.data)
          {
            parent.left = current.left;
          }
          else if (parent.data < current.data)
          {
            parent.right = current.left;
          }
        }
      }

      //Case 4: The current has a left an a right child: Replace current with node with smallest value in left subtree
      else if (current.right != null && current.left != null)
      {

        Node mostright = current.left.right;
        Node mrparent = current.left;

        if (mostright != null)
        {
          while (mostright.right != null)
          {
            mrparent = mostright;
            mostright = mostright.right;
          }

          //mrparents right subtree becomes mostrights left subtree
          mrparent.right = mostright.left;

          mostright.left = current.left;
          mostright.right = current.right;

          if (parent == null)
          {
            root = mostright;
          }
          else
          {
            if (parent.data > current.data)
            {
              parent.left = mostright;
            }
            if (parent.data < current.data)
            {
              parent.right = mostright;
            }
          }
        }
        else
        {
          parent.left = current.left;
        }
      }
      return true;
    }

    public void Rotate(Node Bogdan)
    {
      Node parent = _searchPosAbove(Bogdan.data);


      if (Bogdan.data < parent.data)
      {
        rightRotate(Bogdan, parent);
      }
      else
      {
        leftRotate(Bogdan, parent);
      }
    }

    private void rightRotate(Node Bogdan, Node parent)
    {
      Node grandParent;

      parent.left = Bogdan.right;
      Bogdan.right = parent;

      grandParent = _searchPosAbove(parent.data);

      rotationFinish(Bogdan, parent, grandParent);
    }

    private void leftRotate(Node Bogdan, Node parent)
    {
      Node grandParent;

      parent.right = Bogdan.left;
      Bogdan.left = parent;

      grandParent = _searchPosAbove(parent.data);

      rotationFinish(Bogdan, parent, grandParent);
    }

    private void rotationFinish(Node Bogdan, Node parent, Node grandParent)
    {
      if (grandParent != null)
      {
        if (parent.data < grandParent.data)
        {
          grandParent.left = Bogdan;
        }
        else
        {
          grandParent.right = Bogdan;
        }
      }
    }
  }

  class AVLTree : BinSearchTree
  {
    /*public override bool Insert(int data)
   {
     base.Insert(data);

     if (isBalanced(data))
       return true;
     else
       balance(data);
   }

   public override bool Delete(int data)
   {
     return false;
   }

   //in class AVL 4 the time being... should probably be in a higher class, because Treap uses it as well
   public bool isBalanced(Node n)
   {
     isBalanced();
   }
   */
  }

  class Treap : BinSearchTree
  {

    public override bool Insert(int data)
    {
      base.Insert(data);

      Node Current = _search(data);

      while (Current.priority < Current.parent.priority)
      {
        Rotate(Current);
      }

      Random rnd = new Random();
      int prio = rnd.Next(0, 65536);

      Current.priority = prio;

      return true;
    }

    public override bool Delete(int data)
    {
      Node Current = _search(data);

      while (Current.left != null && Current.right != null)
      {
        if (Current.right == null)
        {
          Rotate(Current.left);
        }
        else
        {
          Rotate(Current.right);
        }
      }

      Current = null;
      return false;

    }
  }
}
