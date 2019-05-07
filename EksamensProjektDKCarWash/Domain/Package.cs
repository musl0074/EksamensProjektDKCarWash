using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Package
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }


        public Package(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public Package (string name)
        {
            Name = name;
        }
           


    }
}
