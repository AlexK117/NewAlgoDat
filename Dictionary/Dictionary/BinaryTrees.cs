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
        int currentBalance = setBalanceInsert(item);

        //is the tree balanced? Note: not always!!
        if (currentBalance == 0 || currentBalance > 1 || currentBalance < -1) //yes
          break;

        if (item.parent != null)
        {
          item = item.parent;
        }
      }

      item = _search(data);

      while (item.parent != null)
      {
        balance(item);
				if (item == root)
					break;
        item = item.parent;
      }

      return true;
    }

    private int setBalanceInsert(Node item)
    {
      if (item.data < item.parent.data)
      {
        return item.parent.balance -= 1;
      }
      else
      {
        return item.parent.balance += 1;
      }
    } 
    private int setBalanceDelete(Node item)
    {
      if (item.data < item.parent.data)
      {
        return item.parent.balance += 1;
      }
      else
      {
        return item.parent.balance -= 1;
      }
    }

		/*Hier wird entschieden welche Rotation aufgerufen wird und wie sich die Balancefaktoren dadurch verändern*/
		private void balance(Node item)
    {
			//Ab parent hängt Baum nach rechts
			if (item.parent.balance > 1) 
      {
				//danach hängt Baum auch nach rechts => einzel-links-rotation
				if (item.balance > 0) 
        {
          item.balance = 0;
          item.parent.balance = 0;
          Rotate(item);
        }
				//danach hängt Baum aber nach links => rechts-links doppel-rotation
				else if (item.balance < 0) 
        {
          if (item.left.balance < 0) //Geschwister-Teilbaum hängt nach links
					{
            item.parent.balance = 0;
            item.balance = 1;
            item.left.balance = 0;
          }
          else if (item.left.balance == 0) //Geschwister-Teilbaum ist der Baum ausgeglichen
					{
            item.parent.balance = 0;
            item.balance = 0;
            item.left.balance = 0;
          }
          else if (item.left.balance > 0) //Geschwister-Teilbaum hängt nach rechts
					{
            item.parent.balance = -1;
            item.balance = 0;
            item.left.balance = 0;
          }

          Rotate(item.left);
          Rotate(item.parent);
        }
				//danach ist der Baum ausgeglichen
        else
        {
          balance(item.parent.right);
        }
      }
			//Ab parent hängt Baum nach links
			else if (item.parent.balance < -1) 
      {
				//danach hängt Baum auch nach links => einzel links rotation
				if (item.balance < 0) 
        {
          item.balance = 0;
          item.parent.balance = 0;
          Rotate(item);
        }
				//danach hängt Baum aber nach rechts => links-rechts doppel-rotation
				else if (item.balance > 0) 
        {

          if (item.right.balance < 0) //Geschwister-Teilbaum hängt nach links
          {
            item.parent.balance = 1;
            item.balance = 0;
            item.right.balance = 0;
          }
          else if (item.right.balance == 0) //Geschwister-Teilbaum ist ausgeglichen
          {
            item.parent.balance = 0;
            item.balance = 0;
            item.right.balance = 0;
          }
          else if (item.right.balance > 0) //Geschwister-Teilbaum hängt nach rechts
          {
            item.parent.balance = 0;
            item.balance = -1;
            item.right.balance = 0;
          }

          Rotate(item.right);
          Rotate(item.parent);
				}
				//danach ist der Baum ausgeglichen
				else
				{
          balance(item.parent.left);
        }
      }
    }

    public override bool Delete(int data)
    {
      Node item = _search(data);
			if (item == null)
				return false;
			Node parent = item.parent;
      Node tmp = item;

      if (item.left != null && item.right == null) ////1. Fall item hat linkes child
      {
				//Anpassen der Balancefaktoren bevor das item gelöscht wird
				while (tmp.parent != null)
        {
					int currentBalance;
					if (tmp.parent.balance == 0) // wenn parent balance 0 war dann verändert sich die Höhe dieses Teilbaumes und dadurch die oberen bf's durch das anpassen der bf's nicht also hier stoppen
					{
						currentBalance = setBalanceDelete(tmp);
						break;
					}

					currentBalance = setBalanceDelete(tmp);

					if (currentBalance > 1 || currentBalance < -1)
						break;

					tmp = tmp.parent;
        }

        if (!base.Delete(data))
          return false;

				if (item.parent != null)
				{
					Node sibling = parent.left; //für den Fall das es hier unausgeglichen wird => beim Geschwisterknoten balancen
					balance(sibling);
				}

        return true;
      }

      else if (item.left == null && item.right != null) ////2. Fall item hat rechtes child
      {
				//Anpassen der Balancefaktoren bevor das item gelöscht wird
				while (tmp.parent != null)
        {
					int currentBalance;
					if (tmp.parent.balance == 0) // wenn parent balance 0 war dann verändert sich die Höhe dieses Teilbaumes und dadurch die oberen bf's durch das anpassen der bf's nicht also hier stoppen
					{
						currentBalance = setBalanceDelete(tmp);
						break;
					}

					currentBalance = setBalanceDelete(tmp);

					if (currentBalance > 1 || currentBalance < -1)
            break;

          tmp = tmp.parent;
        }

        if (!base.Delete(data))
          return false;

				if (item.parent != null)
				{
				  Node sibling = parent.right; //für den Fall das es hier unausgeglichen wird => beim Geschwisterknoten balancen
					balance(sibling);
				}

        return true;
      }

      else if (item.left == null && item.right == null) ////3. Fall item hat keine children
      {
        //Anpassen der Balancefaktoren bevor das item gelöscht wird
        while (tmp.parent != null)
        {
					int currentBalance;
					if (tmp.parent.balance == 0) // wenn parent balance 0 war dann verändert sich die Höhe dieses Teilbaumes und dadurch die oberen bf's durch das anpassen der bf's nicht also hier stoppen
					{
						currentBalance = setBalanceDelete(tmp);
						break;
					}

					currentBalance = setBalanceDelete(tmp);

          if (currentBalance > 1 || currentBalance < -1)
            break;

					tmp = tmp.parent;
        }

        if (!base.Delete(data))
          return false;

				if (item.parent != null)
				{
					balance(item);
				}

        return true;
      }
      else  //2. Fall: Gelöschter Node hat 2 children
      {
        Node mostright = item.left;
        Node mrparent = item;
        Node toBalance = mostright.parent.right;

				//Symmetrischer Vorgänger Suche
        while (mostright.right != null)
        {
          mrparent = mostright;
          mostright = mostright.right;
        }

				tmp = mostright;

				//Balance-Abgleich ab Symmetrischem Vorgänger
        while (tmp.parent != null)
        {
          setBalanceDelete(tmp);

          if (tmp.parent != null)
          {
            tmp = tmp.parent;
          }
        }

				//Balancefaktor vom gelöschten Knoten auf den der hier hin verschoben wird übertragen
				mostright.balance = item.balance;

        if (!base.Delete(data))
          return false;

				if (mostright.parent != null)
				{
					toBalance = mostright.parent.left;
					balance(toBalance); 
				}
        return true;
      }
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
