using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadWebPage
{
    class DirectedCycle
    {
        private bool[] marked;
        private int[] edgeTo;
        //private Stack<Integer> cycle; // vertices on a cycle (if one exists)
        private List<int> cycle;
        private bool[] onStack; // vertices on recursive call stack

        public DirectedCycle(Digraph G)
        {
            onStack = new bool[G.V()];
            edgeTo = new int[G.V()];
            marked = new bool[G.V()];
            for (int v = 0; v < G.V(); v++)
                if (!marked[v]) dfs(G, v);
            //cycle = new List<int>();
        }

        private void dfs(Digraph G, int v)
        {
            onStack[v] = true;
            marked[v] = true;
            
            //for (int w : G.adj(v))
            foreach(var w in G.adj[v])
                //if (this.hasCycle())  //tolgo questa per contare tutti i possibili cicli
                    //return;
            /*else*/ if (!marked[w])
            { 
                edgeTo[w] = v; 
                dfs(G, w); 
            }
            else if (onStack[w])
            {
                //cycle = new Stack<Integer>();
                cycle = new List<int>();
                for (int x = v; x != w; x = edgeTo[x])
                    cycle.Add(x);
                cycle.Add(w);
                cycle.Add(v);
                printCycle();
            }
            onStack[v] = false;
        }

        public bool hasCycle()
        { return cycle != null; }

        public List<int> returnCycle()
        { return cycle; }

        private void printCycle()
        {
            foreach(int i in cycle)
                Console.Write("{0}->", i.ToString());
            Console.WriteLine();
        }
    }
}
