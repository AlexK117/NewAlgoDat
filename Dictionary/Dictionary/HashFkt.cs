using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
  class HashTabSepChain : HashFkt, ISet
  {
    //Array von Listen fuer Hahtabelle mit Verketteten Listen
    public SetUnsortedLinkedList[] hashTableLL = new SetUnsortedLinkedList[hashTableSize];

    public bool Search(int element)
    {
      int bucket = GetHashValue(element);
      if (hashTableLL[bucket] != null)
      {
        return hashTableLL[bucket].Search(element);
      }
      else
      {
        return false;
      }
    }

    public bool Insert(int element)
    {
      int bucket = GetHashValue(element);

      if (hashTableLL[bucket] == null)
      {
        hashTableLL[bucket] = new SetUnsortedLinkedList();
        bool inserted = hashTableLL[bucket].Insert(element);
        return inserted;
      }
      else
      {
        bool inserted = hashTableLL[bucket].Insert(element);
        return inserted;
      }
    }

    public bool Delete(int element)
    {
      int bucket = GetHashValue(element);

      if (hashTableLL[bucket] != null)
      {
        bool deleted = hashTableLL[bucket].Delete(element);
        return deleted;
      }
      else
      {
        return false;
      }
    }

    //Methode um Hashtabelle mit Verketteten Listen auszugeben
    public new void Print()
    {
      for (int i = 0; i < hashTableLL.Length; i++)
      {
        Console.Write(i + " --> ");
        if (hashTableLL[i] != null)
        {
          hashTableLL[i].Print();

        }
        Console.WriteLine();
      }
    }
  }

  class HashTabQuadProb : HashFkt, ISet
  {
    //Array fuer Hashtabelle mit quadratischer sondierung
    protected SetUnsortedArray hashTableQP = new SetUnsortedArray();

    protected int probeSequenceCounter;

    //Kennzeichnet gelöschte Felder
    public bool[] deleteMarker;
    //Kennzeichnet besuchte Felder um Endlosschleifen bei der sondierung zu verhindern
    public bool[] visited;

    public HashTabQuadProb()
    {
      deleteMarker = new bool[hashTableSize];
      visited = new bool[hashTableSize];

      for (int i = 0; i < deleteMarker.Length; i++)
      {
        deleteMarker[i] = false;

      }
    }
    public void resetVisited()
    {
      for (int i = 0; i < deleteMarker.Length; i++)
      {
        visited[i] = false;
      }
    }

    public bool Search(int element)
    {
      int bucket = GetHashValue(element);
      probeSequenceCounter = 0;
      bool notFound = false;

      while (deleteMarker[bucket] || (hashTableQP[bucket] != 0 && hashTableQP[bucket] != element) || visited[bucket])
      {
        if (visited[bucket])
        {
          resetVisited();
          notFound = true;
          break;
        }
        visited[bucket] = true;
        bucket = GetQuadProbBucket(element);
      }

      resetVisited();

      if (deleteMarker[bucket] || (hashTableQP[bucket] == 0) || notFound)
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    public bool Insert(int element)
    {
      int bucket = GetHashValue(element);
      probeSequenceCounter = 0;
      bool dontInsert = hashTableQP.Search(0);

      while (hashTableQP[bucket] != 0 && dontInsert)
      {
        if (hashTableQP[bucket] == element)
        {
          dontInsert = false;
        }
        bucket = GetQuadProbBucket(element);
      }

      if (!dontInsert)
      {
        return false;
      }
      else
      {
        hashTableQP[bucket] = element;
        deleteMarker[bucket] = false;
        return true;
      }


    }

    public bool Delete(int element)
    {
      int bucket = GetHashValue(element);
      probeSequenceCounter = 0;
      bool notFound = false;

      while (deleteMarker[bucket] || (hashTableQP[bucket] != 0 && hashTableQP[bucket] != element) || visited[bucket])
      {
        if (visited[bucket])
        {
          resetVisited();
          notFound = true;
          break;
        }
        visited[bucket] = true;
        bucket = GetQuadProbBucket(element);
      }

      resetVisited();

      if (notFound || hashTableQP[bucket] == 0)
      {
        return false;
      }

      else
      {
        hashTableQP[bucket] = 0;
        deleteMarker[bucket] = true;

        return true;
      }
    }

    public override void Print()
    {
      hashTableQP.Print();
    }

    //----------------Hilfmethoden----------------

    //Berechnet den naechsten Bucket mit quadratischer sondierung
    protected int GetQuadProbBucket(int element)
    {
      int nextSquare = -(int)(Math.Pow(-1, probeSequenceCounter) * Math.Pow((2 * probeSequenceCounter + Math.Pow(-1, probeSequenceCounter) + 3) / 4, 2));
      int bucket = CanonicalMod(GetHashValue(element) + nextSquare, hashTableSize);
      probeSequenceCounter++;

      return bucket;
    }

    //Hilfsklasse für Kanonischen Modulo
    protected int CanonicalMod(int x, int m)
    {
      return (x % m + m) % m;
    }
  }
}
