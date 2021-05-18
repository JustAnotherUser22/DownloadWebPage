using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadWebPage
{
    class Entry
    {
        public int ID;
        public string name;
        public double acquisto;
        public double vendita;

        public System.DateTime timeStamp;    //ora fornita dal server
        public System.DateTime timeStampLocal;   //ora locale
        public string delta;
        public int NumberOfUpdate;

        public double minDelta;
        public double maxDelta;
        public double totalDelta;
        public double averageDelta;

        public Entry(string name)
        {
            this.name = name;
            ID = 0;
            acquisto = 0;
            vendita = 0;
            //timeStamp = "";
            NumberOfUpdate = 0;
            minDelta = 1000000000;
            maxDelta = 0;
            averageDelta = 0;
            totalDelta = 0;
        }

        public void updateDelta()
        {
            System.TimeSpan span = (timeStampLocal - timeStamp);
            double ms = span.TotalMilliseconds;

            if (minDelta > ms)
                minDelta = ms;

            if (maxDelta < ms)
                maxDelta = ms;

            totalDelta += ms;

            averageDelta = totalDelta / NumberOfUpdate;
        }

        public void PrintEntry()
        {
            /*Console.WriteLine(name);
            Console.WriteLine(acquisto.ToString());
            Console.WriteLine(vendita.ToString());
            Console.WriteLine(timeStamp);*/
            System.TimeSpan span = (timeStampLocal - timeStamp);
            //double s = span.TotalSeconds;
            double ms = span.TotalMilliseconds;
            delta = /*s.ToString() + "," + */ms.ToString();
            Console.WriteLine("{0, 5} {1, 10} {2, 10} {3, 10} {4, 15} {5, 15} {6, 5}", ID.ToString(), 
                                                                                       name, 
                                                                                       acquisto,  
                                                                                       vendita,
                                                                                       DateToShort(timeStamp),
                                                                                       DateToShort(timeStampLocal),
                                                                                       delta);
        }

        private static string DateToShort(DateTime d)
        {
            return d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + ":" + d.Millisecond.ToString();
        }
    }
}
