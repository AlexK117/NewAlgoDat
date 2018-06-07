using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
  class Program
  {
    static void Main(string[] args)
    {
      int switch_on = 0;
      do
      {
        Console.WriteLine("Welche Datenstruktur möchten Sie testen?");
        Console.WriteLine("Array  --> 1");
        Console.WriteLine("Liste  --> 2");
        Console.WriteLine("Baum   --> 3");
        Console.WriteLine("Hash   --> 4");
        try
        {
          switch_on = Convert.ToInt32(ReadLine());
        }
        catch (System.FormatException) { switch_on = 0; }

        IDictionary Test = null;
        string datastruct = "";

        switch (switch_on)
        {
          case 1:
            datastruct = "Array";
            Console.WriteLine($"Welche Art von {datastruct} möchten sie testen?");
            Console.WriteLine("MultiSetUnsorted  --> 1");
            Console.WriteLine("MultiSetSorted    --> 2");
            Console.WriteLine("SetUnsorted       --> 3");
            Console.WriteLine("SetSorted         --> 4");
            try
            {
              switch_on = Convert.ToInt32(ReadLine());
            }
            catch (System.FormatException) { switch_on = 0; }

            switch (switch_on)
            {
              case 1:
                Test = new MultiSetUnsortedArray();
                break;
              case 2:
                Test = new MultiSetSortedLinkedList();    //Ändern, wenn Arrays fertig
                break;
              case 3:
                Test = new SetUnsortedLinkedList();
                break;
              case 4:
                Test = new SetSortedLinkedList();
                break;

              default:
                switch_on = 0;
                Console.WriteLine("Nö\n");
                break;
            }
            break;

          case 2:
            datastruct = "Liste";
            Console.WriteLine($"Welche Art von {datastruct} möchten sie testen?");
            Console.WriteLine("MultiSetUnsorted  --> 1");
            Console.WriteLine("MultiSetSorted    --> 2");
            Console.WriteLine("SetUnsorted       --> 3");
            Console.WriteLine("SetSorted         --> 4");
            try
            {
              switch_on = Convert.ToInt32(ReadLine());
            }
            catch (System.FormatException) { switch_on = 0; }

            switch (switch_on)
            {
              case 1:
                Test = new MultiSetUnsortedLinkedList();
                break;
              case 2:
                Test = new MultiSetSortedLinkedList();
                break;
              case 3:
                Test = new SetUnsortedLinkedList();
                break;
              case 4:
                Test = new SetSortedLinkedList();
                break;

              default:
                switch_on = 0;
                Console.WriteLine("Nö\n");
                break;
            }
            break;

          case 3:
            datastruct = "Baum";
            Console.WriteLine($"Welche Art von {datastruct} möchten sie testen?");
            Console.WriteLine("BinarySearchTree  --> 1");
            Console.WriteLine("Treap             --> 2");
            Console.WriteLine("AVL               --> 3");
            try
            {
              switch_on = Convert.ToInt32(ReadLine());
            }
            catch (System.FormatException) { switch_on = 0; }

            switch (switch_on)
            {
              case 1:
                Test = new BinSearchTree();
                break;
              case 2:
                Test = new Treap();
                break;
              case 3:
                Test = new AVLTree();
                break;
              default:
                switch_on = 0;
                Console.WriteLine("Nö\n");
                break;
            }
            break;

          case 4:
            datastruct = "Hash";
            Console.WriteLine($"Welche Art von {datastruct} möchten sie testen?");
            Console.WriteLine("BinarySearchTree  --> 1");
            Console.WriteLine("Treap             --> 2");
            Console.WriteLine("AVL               --> 3");
            try
            {
              switch_on = Convert.ToInt32(ReadLine());
            }
            catch (System.FormatException) { switch_on = 0; }

            switch (switch_on)
            {
              case 1:
                Test = new BinSearchTree();     //Anpassen, wenn Hash fertig
                break;
              case 2:
                Test = new Treap();
                break;

              default:
                switch_on = 0;
                Console.WriteLine("Nö\n");
                break;
            }
            break;

          default:
            switch_on = 0;
            Console.WriteLine("Nö\n");
            break;
        }

        if (switch_on != 0)
        {
          string s = "";
          do
          {
            Console.WriteLine("Welche Operation soll es denn sein? ('0' zum ändern der Struktur)");
            Console.WriteLine("Einfugen  --> 1");
            Console.WriteLine("Suchen    --> 2");
            Console.WriteLine("Loschen   --> 3");
            Console.WriteLine("Ausgeben  --> 4");

            try
            {
              switch_on = Convert.ToInt32(ReadLine());
            }
            catch (System.FormatException) { switch_on = 50; }

            switch (switch_on)
            {
              case 1:
                Console.WriteLine("Geben sie die Zahlen zum Einfügen ein (zum ändern der Operation '+' drücken)");
                s = ReadLine();

                while (s != "+")
                {
                  try
                  {
                    Console.WriteLine(Test.Insert(Convert.ToInt32(s)));
                  }
                  catch (FormatException)
                  {
                    Console.WriteLine("Nur Zahlen eingeben:");
                  }
                  s = ReadLine();
                }
                break;
              case 2:
                Console.WriteLine("Geben sie die Zahlen zum Suchen ein (zum ändern der Operation '+' drücken)");
                s = ReadLine();

                while (s != "+")
                {
                  try
                  {
                    Console.WriteLine(Test.Search(Convert.ToInt32(s)));
                  }
                  catch (FormatException)
                  {
                    Console.WriteLine("Nur Zahlen eingeben:");
                  }
                  s = ReadLine();
                }
                break;
              case 3:
                Console.WriteLine("Geben sie die Zahlen zum Löschen ein (zum ändern der Operation '+' drücken)");
                s = ReadLine();

                while (s != "+")
                {
                  try
                  {
                    Console.WriteLine(Test.Delete(Convert.ToInt32(s)));
                  }
                  catch (FormatException)
                  {
                    Console.WriteLine("Nur Zahlen eingeben:");
                  }
                  s = ReadLine();
                }
                break;
              case 4:
                Console.WriteLine();
                Test.Print();
                Console.WriteLine();
                break;
              case 0:
                s = "OP_switch";
                break;

              default:
                Console.WriteLine("Nö\n");
                break;
            }
          }
          while (s != "OP_switch");
        }
      }
      while (true);   //Endlose wiederholung

      //MultiSetSortedLinkedList neu = new SetSortedLinkedList();
      //neu.Insert(3);
      //neu.Insert(2);
      //neu.Insert(4);
      //neu.Insert(1);
      //neu.Insert(1);
      //neu.Insert(1);
      //neu.Insert(7);
      //Console.WriteLine(neu.Search(79));
      //neu.Insert(7);
      //neu.Insert(7);
      //neu.Insert(5);
      //neu.Insert(6);
      //Console.WriteLine(neu.Insert(7));
      //Console.WriteLine(neu.Insert(1));
      //neu.Insert(4);
      //neu.DeleteAll(1);
      //neu.DeleteAll(7);
      //neu.Delete(7);
      //neu.Delete(1);
      //neu.Delete(7);
      //Console.WriteLine(neu.Delete(7));
      //neu.Insert(-8);
      //neu.Print();
      //Console.WriteLine(neu.Search(5));

      //BinSearchTree tree = new BinSearchTree();


      //tree.Insert(1);
      //tree.Insert(3);
      //tree.Insert(8);
      //tree.Insert(6);
      //tree.Insert(0);
      //tree.Insert(2);
      //tree.Insert(4);
      //tree.Insert(19);
      //tree.Insert(29);
      //tree.Insert(28);
      //tree.Insert(30);
      //tree.Insert(26);
      //tree.Insert(-1);
      //tree.Insert(-3);
      //tree.Insert(-2);
      //tree.Insert(-4);
      //tree.Insert(-6);
      //tree.Insert(-9);
      //tree.Insert(-5);
      //tree.Insert(-7);
      //WriteLine(tree.Insert(8));
      //WriteLine(tree.Insert(0));
      //tree.Print();
      //Console.WriteLine(tree.Search(9));
      //Console.WriteLine(tree.Search(1));
      //Console.WriteLine(tree.Search(6));
      //tree.Printv();
      //Console.ReadKey();
    }
  }
}
