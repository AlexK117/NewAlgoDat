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
        if (array[index] == elem)
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

      if (index != -1)
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

  class MultiSetSortedArray : Array//, IMultiSetSorted
  {
        public bool Search(int elem)
        {
            int index = _search(elem);

            if(index!=-1)
            {
                return true;
            }
            return false;
        }
        public virtual bool Insert(int elem)
        {
            
            int index = _searchsorted(elem);

            if (index != array.Length - 1)
            {
                int aktval = array[index];

                for (int i = index + 1; i < array.Length - 1; i++)
                {
                    int nextval = array[i];
                    array[i] = aktval;
                    aktval = nextval;
                }
                array[index] = elem;
                return true;
            }

            array[index] = elem;                       
            return true;
        }     

        public bool Delete(int elem)
        {
            int index = _search(elem);
            
            if(index==-1)
            {
                return false;
            }

            else
            {
                for (int i = index; i < array.Length - 1; i++)
                {
                    array[i] = array[i + 1];                    
                }
            }
            return true;
        }
  }

  class SetSortedArray : MultiSetSortedArray//, ISetSorted
  {
        public override bool Insert(int elem)
        {
            if(_search(elem)!=-1)
            {
                return false;
            }
            else
            {
                return base.Insert(elem);
            }
        }
  }
}
