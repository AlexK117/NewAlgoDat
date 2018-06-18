using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
  class MultiSetUnsortedArray : Array, IMultiSet
  {
    public bool Search(int elem)
    {
      int index = _search(elem);
      if (index != -1)
      {
        if (array[index] == elem && elem != 0)
        {
          return true;
        }
      }
      return false;
    }

    public virtual bool Insert(int elem)
    {            
        int index = _search(0);
        if (index != -1)
        {
          array[index] = elem;
          return true;
        }
        return false;       
    }

    public bool Delete(int elem)
    {
      int i = _search(0) - 1;

      if (i == -2)
      {
        i = array.Length - 1;
      }

      int index = _search(elem);

      if (index != -1 && elem != 0)
      {
        array[index] = array[i];
        array[i] = 0;
        return true;
      }
      return false;
    }
  }

  class SetUnsortedArray : MultiSetUnsortedArray, ISet
  {
    public override bool Insert(int elem)
    {
      if (Search(elem))
      {
        return false;
      }
      return base.Insert(elem);
    }
  }

  class MultiSetSortedArray : Array, IMultiSetSorted
  {
    public virtual bool Search( int elem) 
  {
      int index = _search(elem);
      if (index != -1)
      {
        if (array[index] == elem && elem != 0)
        {
          return true;
        }
      }
      return false;
      
    }

  public virtual bool Insert(int elem)
  {
      int index = _searchsorted(elem); // Position suchen 

      if (index != array.Length - 1) // falls es nicht an letzte Position eingefügt werden muss
      {// speichert Wert der um eins nach hinten verschoben werden muss
        int aktval = array[index];

        for (int i = index + 1; i < array.Length - 1; i++)
        {
          int nextval = array[i]; // speichert den Wert nach aktval
          array[i] = aktval; // aktval wird um eins nach rechts verschoben
          aktval = nextval;
        }
        array[index] = elem; // Element wird eingefügt
        return true;
      }
      else // falls es an letzter Position eingefügt wird 
      {
        array[index] = elem;
        return true;
      }
    }

  public bool Delete(int elem)
  {
      int index = _search(elem);
      if (index == -1 || elem == 0) // falls Wert nicht vorhanden 
      {
        return false;
      }
      else 
      {
       for(int i= index; i < array.Length -1; i++)
       {
          array[i] = array[i + 1]; // Wert von rechts wird wird nach links verschoben und überschreibt/löscht Element
       }
      }
      return true;
    }

  }

  class SetSortedArray : MultiSetSortedArray, IMultiSetSorted
  {
    public override bool Insert(int elem)
    {
      if (Search(elem))
      {
        return false;
      }
      return base.Insert(elem);
    }
  }
}
