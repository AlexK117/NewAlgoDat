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
    private delegate bool op(int data);

    static void Main(string[] args)
    {
      int switch_on = 0;
      IDictionary Test = null;
      string interfaceTyp = "";

      void askMultiSetSorted()
      {
        interfaceTyp = "MultiSetSorted";
        Console.WriteLine($"Welche Art von {interfaceTyp} möchten sie testen?");
        Console.WriteLine("MultiSetSortedLinkedList  --> 1");
        Console.WriteLine("MultiSetSortedArray       --> 2");

        try
        {
          switch_on = Convert.ToInt32(ReadLine());
        }
        catch (System.FormatException) { switch_on = 0; }

        switch (switch_on)
        {
          case 1:
            Test = new MultiSetSortedLinkedList();
            break;
          case 2:
            //Test = new MultiSetSortedArray();    
            break;

          default:
            switch_on = 0;
            Console.WriteLine("Nö\n");
            break;
        }
      }

      void askMultiSetUnsorted()
      {
        interfaceTyp = "MultiSetUnsorted";
        Console.WriteLine($"Welche Art von {interfaceTyp} möchten sie testen?");
        Console.WriteLine("MultiSetUnsortedLinkedList  --> 1");
        Console.WriteLine("MultiSetUnSortedArray       --> 2");

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
            Test = new MultiSetUnsortedArray();
            break;

          default:
            switch_on = 0;
            Console.WriteLine("Nö\n");
            break;
        }
      }

      void askSetSorted()
      {
        interfaceTyp = "SetSorted";
        Console.WriteLine($"Welche Art von {interfaceTyp} möchten sie testen?");
        Console.WriteLine("SetSortedLinkedList  --> 1");
        Console.WriteLine("SetSortedArray       --> 2");
        Console.WriteLine("BinSearchTree        --> 3");
        Console.WriteLine("Treap                --> 4");
        Console.WriteLine("AVL-Tree             --> 5");

        try
        {
          switch_on = Convert.ToInt32(ReadLine());
        }
        catch (System.FormatException) { switch_on = 0; }

        switch (switch_on)
        {
          case 1:
            Test = new SetSortedLinkedList();
            break;
          case 2:
            //Test = new SetSortedArray();
            break;
          case 3:
            Test = new BinSearchTree();
            break;
          case 4:
            Test = new Treap();
            break;
          case 5:
            Test = new AVLTree();
            break;
          default:
            switch_on = 0;
            Console.WriteLine("Nö\n");
            break;
        }
      }

      void askSetUnsorted()
      {
        interfaceTyp = "SetUnsorted";
        Console.WriteLine($"Welche Art von {interfaceTyp} möchten sie testen?");
        Console.WriteLine("SetUnsortedLinkedList  --> 1");
        Console.WriteLine("SetUnsortedArray       --> 2");
        Console.WriteLine("HashTabSepChain        --> 3");
        Console.WriteLine("HashTabQuadProb        --> 4");

        try
        {
          switch_on = Convert.ToInt32(ReadLine());
        }
        catch (System.FormatException) { switch_on = 0; }

        switch (switch_on)
        {
          case 1:
            Test = new SetUnsortedLinkedList();
            break;
          case 2:
            Test = new SetUnsortedArray();
            break;
          case 3:
            //Test = new HashTabSepChain();
            break;
          case 4:
            // Test = new HashTabQuadProb();
            break;

          default:
            switch_on = 0;
            Console.WriteLine("Nö\n");
            break;
        }
      }

      do
      {
        Console.WriteLine("Welchen Typ möchten Sie testen?");
        Console.WriteLine("MultiSetSorted    --> 1");
        Console.WriteLine("MultiSetUnsorted  --> 2");
        Console.WriteLine("SetSorted         --> 3");
        Console.WriteLine("SetUnsorted       --> 4");

        try
        {
          switch_on = Convert.ToInt32(ReadLine());
        }
        catch (System.FormatException) { switch_on = 0; }

        switch (switch_on)
        {
          case 1:
            askMultiSetSorted();
            break;

          case 2:
            askMultiSetUnsorted();
            break;

          case 3:
            askSetSorted();
            break;

          case 4:
            askSetUnsorted();
            break;

          default:
            switch_on = 0;
            Console.WriteLine("Nö\n");
            break;
        }

        //OP-Ausführen
        if (switch_on != 0)
        {
          string eingabe = "";

          do
          {
            string methode = "";
            op operation = null;

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

            //OP_Switch
            switch (switch_on)
            {
              case 1:
                methode = "Einfügen";
                operation = new op(Test.Insert);
                break;
              case 2:
                methode = "Suchen";
                operation = new op(Test.Search);
                break;
              case 3:
                methode = "Löschen";
                operation = new op(Test.Delete);
                break;
              case 4:
                Console.WriteLine();
                Test.Print();
                Console.WriteLine();
                break;
              case 0:
                eingabe = "OP_switch";
                break;

              default:
                Console.WriteLine("Nö\n");
                break;
            }

            if (operation != null)
            {
              Console.WriteLine($"Geben sie die Zahlen zum {methode} ein (zum ändern der Operation '+' drücken)");
              eingabe = ReadLine();
              while (eingabe != "+")
              {
                try
                {
                  Console.WriteLine(operation(Convert.ToInt32(eingabe)));
                }
                catch (FormatException)
                {
                  Console.WriteLine("Nur Zahlen eingeben:");
                }
                eingabe = ReadLine();
              }
            }
          }
          while (eingabe != "OP_switch");
        }
      }
      while (true);   //Endlose wiederholung*/


      MultiSetUnsortedArray test = new MultiSetUnsortedArray();
      test.Insert(2);
      test.Insert(1);
      test.Insert(3);
      test.Insert(2);
      test.Insert(-1);
      //Console.WriteLine(test.Delete(5));

      test.Print();

      test.Delete(2);
      test.Print();
    }
  }
}
