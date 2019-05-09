using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Customer
    {
        public string CustomerName { get; private set; }
        public string Email { get; private set; }
        public string Telephone { get; private set; }
        public string Vat { get; private set; }
        public Vehicle Vehicle { get; private set; }

        public Customer(string customerName, string email, string telephone, Vehicle vehicle, string vat = "")
        {
            CustomerName = customerName;
            Email = email;
            Telephone = telephone;
            Vat = vat;
            Vehicle = vehicle;
        }

        public Customer(string customerName, string email, string telephone, string vat = "")
        {
            CustomerName = customerName;
            Email = email;
            Telephone = telephone;
            Vat = vat;
        }

        public Customer(string customerName)
        {
            CustomerName = customerName;
        }
    }
}
