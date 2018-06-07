using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
  interface IDictionary
  {
    bool Search(int elem);
    bool Insert(int elem);
    bool Delete(int elem);
    void Print();
  }

  interface IMultiSet : IDictionary { }
  interface ISet : IMultiSet { }
  interface IMultiSetSorted : IDictionary { }
  interface ISetSorted : IMultiSetSorted { }

}
