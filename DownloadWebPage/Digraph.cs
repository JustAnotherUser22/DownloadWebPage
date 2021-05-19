using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadWebPage
{
    class Digraph
    {
        private int _V;
        private int _E;
        //private Bag<Integer>[] adj;
        public List<int>[] adj;
        
        public Digraph(int V)
        {
            _V = V;
            _E = 0;
            //adj = (Bag<Integer>[]) new Bag[V];
            //for (int v = 0; v < V; v++)
            //adj[v] = new Bag<Integer>();

            adj = new List<int>[V];
            for(int i = 0; i < V; i++)
                adj[i] = new List<int>();
        }
        
        public int V() 
        { 
            return _V; 
        }
        
        public int E()
        { 
            return _E; 
        }
        
        public void addEdge(int v, int w)
        {
            adj[v].Add(w);
            _E++;
        }
        
        /*public Iterable<Integer> adj(int v)
        { 
            return adj[v]; 
        }*/
        
        /*public Digraph reverse()
        {
            Digraph R = new Digraph(V);
            for (int v = 0; v < V; v++)
            for (int w : adj(v))
            R.addEdge(w, v);
            return R;
        }*/
    }
}
