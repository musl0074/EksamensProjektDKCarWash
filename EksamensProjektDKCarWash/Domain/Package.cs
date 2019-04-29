using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Package
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }


        public Package(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }


    }
}
