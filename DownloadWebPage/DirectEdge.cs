using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadWebPage
{
    class DirectEdge
    {
        public int from;
        public int to;
        public double weight;

        public DirectEdge(int v, int w, double weight)
        {
            this.from = v;
            this.to = w;
            this.weight = weight;
        }

        /*public double weight()
        {
            return _weight
        }

        public double from()
        {
            return _v;
        }

        public double to()
        {
            return _w;
        }*/

        public string printPath()
        {
            string toReturn = from + " -> " + to + " , " + weight;
            return toReturn;
        } 
    }
}
