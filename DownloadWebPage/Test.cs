using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadWebPage
{
    class Test
    {
        public static void TestDirectEdge()
        {
            DirectEdge d = new DirectEdge(1, 2, 0.1);
            Console.WriteLine(d.printPath());
            d.from = 5;
            Console.WriteLine(d.printPath());
            d.to = 8;
            Console.WriteLine(d.printPath());
            d.weight = 1.234;
            Console.WriteLine(d.printPath());
        }
    
        public static void TestEdgeWeightedDigraph()
        {
            DirectEdge de1 = new DirectEdge(0, 1, 1.1);
            DirectEdge de2 = new DirectEdge(1, 2, 2.2);
            DirectEdge de3 = new DirectEdge(2, 0, 3.3);

            EdgeWeightedDigraph EWD = new EdgeWeightedDigraph(3);
            EWD.AddEdge(de1);
            EWD.AddEdge(de2);
            EWD.AddEdge(de3);

            EWD.PrintDigraph();
            Console.WriteLine("----------");
            //EWD.adj[0][0].weight = 999;
            EWD.PrintDigraph();
        }
    
    }    
}
