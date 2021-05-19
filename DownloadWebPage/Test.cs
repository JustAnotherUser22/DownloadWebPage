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

        public static void TestGraph()
        {
            Digraph d = new Digraph(7);
            d.addEdge(0, 1);
            d.addEdge(1, 2);
            d.addEdge(2, 3);
            d.addEdge(3, 1);
            d.addEdge(0, 4);
            d.addEdge(4, 5);
            d.addEdge(5, 6);
            d.addEdge(6, 0);

            DirectedCycle dc = new DirectedCycle(d);

            var list = dc.returnCycle();
        }

        public static void TestGraph2()
        {
            FastChange fc = new FastChange();

            for (int i = 0; i < (int)Constant.tassi.LAST_VALUE; i++)
            {
                string[] entry = ((Constant.tassi)i).ToString().Split('_');
                fc.addString(entry[0]);
                fc.addString(entry[1]);
            }

            Digraph d = new Digraph(fc.numberOfItems);

            for (int i = 0; i < (int)Constant.tassi.LAST_VALUE; i++)
            {
                string[] entry = ((Constant.tassi)i).ToString().Split('_');
                d.addEdge(entry[0], entry[1]);
                d.addEdge(entry[1], entry[0]);

            }



        }
    
    }    
}
