using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadWebPage
{
    class EdgeWeightedDigraph
    {
        int V;  //numero di vertici
        int E;  //numero di "edges" (bordi)
        //Bag<DirectEdge>[] adj;  //matrice delle "adiacenze"
        public List<DirectEdge>[] adj;

        public EdgeWeightedDigraph(int V)
        {
            this.V = V;
            this.E = 0;
            //adj = new Bag<DirectEdge>[V];
            adj = new List<DirectEdge>[V];
            for (int i = 0; i < V; i++)
                //adj[i] = new Bag<DirectEdge>();
                adj[i] = new List<DirectEdge>();
        }

        public void AddEdge(DirectEdge de)
        {
            adj[de.from].Add(de);
        }

        public void PrintDigraph()
        {
            for (int i = 0; i < V; i++)
            {
                Console.WriteLine(i);
                List<DirectEdge> currentList = adj[i];
                for (int j = 0; j < currentList.Count; j++)
                {
                    Console.Write(" ");
                    Console.Write(currentList[j].printPath());
                    Console.WriteLine();
                }
            }
        }

      


    }
}
