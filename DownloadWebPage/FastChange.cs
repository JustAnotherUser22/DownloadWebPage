using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadWebPage
{
    class FastChange
    {
        Dictionary<string, int> stringToArray;
        string[] arrayToString;
        public int numberOfItems;

        public FastChange()
        {
            stringToArray = new Dictionary<string, int>();
            arrayToString = new string[100];
            numberOfItems = 0;
        }

        public bool addString(string str)
        {
            if (stringToArray.ContainsKey(str))
                return false;

            int lastItem = stringToArray.Count;

            stringToArray.Add(str, lastItem);
            arrayToString[lastItem] = str;

            numberOfItems++;

            return true;
        }

        string getStringAtInt (int index)
        {
            return arrayToString[index];
        }

        int getIntAtString (string str)
        {
            return stringToArray[str];
        }

        public void print()
        {
            Console.WriteLine("--- string to array ---");

            foreach (KeyValuePair<string, int> kv in stringToArray)
                Console.WriteLine("{0}      {1}", kv.Key, kv.Value);

            Console.WriteLine("--- array to string ---");

            for(int i = 0; i < stringToArray.Count; i++)
                Console.WriteLine("{0}      {1}", i, arrayToString[i]);
        }
    }
}
