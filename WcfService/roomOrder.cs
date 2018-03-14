using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfService
{
    class roomOrder
    {
        private string name; 
        private int count;
        private double price;

        public string Name { get => name; set => name = value; }
        public int Count { get => count; set => count = value; }
        public double Price { get => price; set => price = value; }
    }
}
