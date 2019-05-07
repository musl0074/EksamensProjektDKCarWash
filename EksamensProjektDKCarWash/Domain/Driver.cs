using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Driver
    {
        public string Name { get; private set; }

        public Driver(string name)
        {
            Name = name;
        }
    }
}
