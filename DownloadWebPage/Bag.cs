using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadWebPage
{
    class Bag<T> 
    {
        private Node<T> first;
        public int size = 0;

        public Bag()
        {
            size = 0;
        }

        public void add(T item)
        {
            Node<T> oldFIrst = first;
            first = new Node<T>();
            first.item = item;
            first.next = oldFIrst;

            size++;
        }







    }

   
}
