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

        protected void Rotate(Node Bogdan)
        {
            if (Bogdan.data < Bogdan.parent.data)
            {
                rightRotate(Bogdan, Bogdan.parent);
            }
            else
            {
                leftRotate(Bogdan, Bogdan.parent);
            }
        }

        private void rightRotate(Node Bogdan, Node parent)
        {
            parent.left = Bogdan.right;
            Bogdan.right = parent;

            rotationFinish(Bogdan, parent, parent.parent);
        }

        private void leftRotate(Node Bogdan, Node parent)
        {
            parent.right = Bogdan.left;
            Bogdan.left = parent;


            rotationFinish(Bogdan, parent, parent.parent);
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

                item = item.parent;
            }
            return true;
        }

        public override bool Delete(int data)
        {
            return base.Delete(data);

            //ausgleichs shit
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
            if (item.parent.balance > 1)
            {
                if (item.balance > 0)
                {
                    Rotate(item);
                }
            }
        }
    }

    class Treap : BinSearchTree
    {

        public override bool Insert(int data)
        {
            if (!base.Insert(data))
                return false;

            Node Current = _search(data);

            while (Current.parent != null && Current.priority < Current.parent.priority)
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
                }

                Current = null;
                return true;
            }
            return false;
        }
    }
}
