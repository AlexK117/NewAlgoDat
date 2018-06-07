using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
  class MultiSetUnsortedLinkedList : List, IMultiSet
  {
    //Interface-Implementierung
    public override bool Search(int elem)
    {
      if (_search(elem) != null)
        return true;

      return false;
    }

    public override bool Insert(int elem)
    {
      ListElement tmp = new ListElement(elem);

      if (start == null)      //Neu Einfügen
        start = end = tmp;
      else
      {
        tmp.prev = end;
        end = end.next = tmp;
      }

      return true;
    }

    public override bool Delete(int elem)
    {
      ListElement tmp = _search(elem);

      if (tmp == null)                //Kein Element
        return false;

      if (tmp == start && tmp == end) //Einziges Element
      {
        start = end = null;
        return true;
      }

      if (tmp == start)               //Start-Element
      {
        tmp.next.prev = null;
        start = tmp.next;
        return true;
      }

      if (tmp == end)                 //End-Element
      {
        tmp.prev.next = null;
        end = tmp.prev;
        return true;
      }

      tmp.prev.next = tmp.next;       //Mitten drin
      tmp.next.prev = tmp.prev;
      return true;
    }

    public void DeleteAll(int elem)
    {
      do { Delete(elem); }
      while (Delete(elem));
    }
  }

  class SetUnsortedLinkedList : MultiSetUnsortedLinkedList, ISet
  {
    public override bool Insert(int elem)
    {
      if (Search(elem))
        return false;
      return base.Insert(elem);
    }
  }

  class MultiSetSortedLinkedList : List, IMultiSetSorted
  {
    public override bool Search(int elem)
    {
      if (_search(elem) != null)
        return true;

      return false;
    }

    public override bool Insert(int elem)
    {
      ListElement tmp = new ListElement(elem);

      if (start == null)      //Neu Einfügen
      {
        start = end = tmp;
        return true;
      }

      if (start.elem > elem)  //Vorne Einfügen
      {
        tmp.next = start;
        start = start.prev = tmp;
        return true;
      }

      if (end.elem <= elem)   //Hinten Einfügen
      {
        tmp.prev = end;
        end = end.next = tmp;
        return true;
      }

      ListElement le;         //Mitten drin
      for (le = start; le != null; le++)
      {
        if (le.elem > elem)               //le gibt Nachfolger an
        {
          tmp.prev = le.prev;             //tmp.prev in Liste einfügen
          tmp.prev.next = le.prev = tmp;  //tmp einfügen
          tmp.next = le;                  //tmp.next einfügen
          return true;
        }
      }

      return false;
    }

    public override bool Delete(int elem)
    {
      ListElement tmp = _search(elem);

      if (tmp == null)                //Kein Element
        return false;

      if (tmp == start && tmp == end) //Einziges Element
      {
        start = end = null;
        return true;
      }

      if (tmp == start)               //Start-Element
      {
        tmp.next.prev = null;
        start = tmp.next;
        return true;
      }

      if (tmp == end)                 //End-Element
      {
        tmp.prev.next = null;
        end = tmp.prev;
        return true;
      }

      tmp.prev.next = tmp.next;       //Mitten drin
      tmp.next.prev = tmp.prev;
      return true;
    }

    public void DeleteAll(int elem)
    {
      do { Delete(elem); }
      while (Delete(elem));
    }
  }

  class SetSortedLinkedList : MultiSetSortedLinkedList, ISetSorted
  {
    public override bool Insert(int elem)
    {
      if (Search(elem))
        return false;
      return base.Insert(elem);
    }
  }
}
