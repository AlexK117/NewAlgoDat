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
        /*Preorder(current);
        Console.WriteLine();
        Postorder(current);
        Console.WriteLine();*/
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
      if (root == null)
      {
        root = new Node(data);
        return true;
      }

      Node tmp = new Node(data);
      Node tmp_Parent = _searchPosAbove(data);

      if (tmp_Parent != null)
      {
        tmp.parent = tmp_Parent;

        if (tmp_Parent.data < data)
        {
          tmp_Parent.right = tmp;
          return true;
        }
        else
        {
          tmp_Parent.left = tmp;
          return true;
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
      Node parent = current.parent;

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
          current.right.parent = null;
        }

        else
        {
          if (parent.data > current.data)
          {
            parent.left = current.right;
            current.right.parent = parent;
          }
          else if (parent.data < current.data)
          {
            parent.right = current.right;
            current.right.parent = parent;
          }
        }
      }

      //Case 3: The current has no right child
      else if (current.right == null)
      {
        if (parent == null)
        {
          root = current.left;
          current.left.parent = null;
        }

        else
        {
          if (parent.data > current.data)
          {
            parent.left = current.left;
            current.left.parent = parent;
          }
          else if (parent.data < current.data)
          {
            parent.right = current.left;
            current.left.parent = parent;
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
          current.left.parent = mostright;
          mostright.right = current.right;
          current.right.parent = mostright;

          if (parent == null)
          {
            root = mostright;
            mostright.parent = null;
          }
          else
          {
            if (parent.data > current.data)
            {
              parent.left = mostright;
              mostright.parent = parent;

            }
            if (parent.data < current.data)
            {
              parent.right = mostright;
              mostright.parent = parent;

            }
          }
        }
        else
        {
          if (parent == null)
          {
            current.left.right = root.right;
            current.left.parent = null;
            root = current.left;

          }
          else
          {
            if (parent.data > current.data)
            {
              parent.left = current.left;
              current.left.parent = parent;
              current.left.right = current.right;
            }
            if (parent.data < current.data)
            {
              parent.right = current.left;
              current.left.parent = parent;
              current.left.right = current.right;
            }
          }
        }
      }
      return true;
    }

    protected void Rotate(Node current)
    {
      if (current.data < current.parent.data)
      {
        rightRotate(current, current.parent);
      }
      else
      {
        leftRotate(current, current.parent);
      }
    }

    private void rightRotate(Node current, Node parent)
    {
      parent.left = current.right;
      if (current.right != null)
      {
        current.right.parent = parent;
      }
      current.right = parent;
      //parent.parent = current;

      if (parent.parent != null)
      {
        rotationFinish(current, parent, parent.parent);
      }
      else
      {
        root = current;
        parent.parent = current;
        current.parent = null;
      }
    }

    private void leftRotate(Node current, Node parent)
    {
      parent.right = current.left;
      if (current.left != null)
      {
        current.left.parent = parent;
      }
      current.left = parent;
      //parent.parent = current;

      if (parent.parent != null)
      {
        rotationFinish(current, parent, parent.parent);
      }
      else
      {
        root = current;
        parent.parent = current;
        current.parent = null;
      }
    }

    private void rotationFinish(Node current, Node parent, Node grandParent)
    {

      if (parent.data < grandParent.data)
      {
        grandParent.left = current;

      }
      else
      {
        grandParent.right = current;
      }

      current.parent = grandParent;
      parent.parent = current;
    }
  }

  class AVLTree : BinSearchTree
  {
    public override bool Insert(int data)
    {
      if (!base.Insert(data))
        return false;

      Node item = _search(data);

      while (item.parent != null)
      {
        int currentBalance = setBalance(item, item.parent);

        //is the tree balanced? Note: not always!!
        if (currentBalance == 0) //yes
          break;
        else if (currentBalance > 1 || currentBalance < -1) //no
        {
          balance(item); //mehrere balances??!!
        }

        if (item.parent != null)
        {
          item = item.parent;
        }
      }
      return true;
    }

    private int setBalance(Node item, Node parent)
    {
      if (item.data < parent.data)
      {
        return parent.balance -= 1;
      }
      else
      {
        return parent.balance += 1;
      }
    }

    private void balance(Node item)
    {
      //Hier evtl Fallunterscheidung ob man RL oder LR oder normal L oder normal R rot machen muss.
      if (item.parent.balance > 1) //Ab parent hängt Baum nach rechts
      {
        if (item.balance > 0) //danach hängt Baum auch nach rechts => einzel-links-rotation
        {
          Rotate(item);
        }
        else
        {
          Rotate(item.left);
        }
      }
      else if (item.parent.balance < -1) //Ab parent hängt Baum nach links
      {

        if (item.balance < 0) // danach hängt Baum auch nach links => einzel links rotation
        {
          Rotate(item);
        }
        else
        {
          Rotate(item.right);
        }
      }
    }

		public override bool Delete(int data)
		{
			return base.Delete(data);

			//ausgleichs shit
		}
	}

  class Treap : BinSearchTree
  {

    public override bool Insert(int data)
    {
      if (!base.Insert(data))
      {
        return false;
      }

      Node Current = _search(data);

      //Set Priority of our inserted Node;
      Random rnd = new Random();
      //int prio = rnd.Next(0, 65536);
      int prio = rnd.Next(0, 100);

      Current.priority = prio;
      Console.WriteLine(Current.priority);

      //Rotate the current node up until the priority is at the correct position
      while (Current.parent != null && Current.priority < Current.parent.priority)
      {
        Rotate(Current);
        Current = _search(data);
      }


      return true;
    }

    public override bool Delete(int data)
    {
      Node Current = _search(data);

      //Node gets rotated down until its a leaf, then gets deleted
      if (Current != null)
      {
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

          Current = _search(data);
        }

        base.Delete(data);
        return true;
      }
      return false;
    }
  }
}
