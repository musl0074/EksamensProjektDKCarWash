﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Booking
    {
        public string CustomerName { get; set; }
        public DateTime StartTime { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int Package { get; set; }
        public string Vat { get; set; }
        public int Id { get; set; }


 
        public Booking(string customerName, DateTime startTime, string email, string telephone, int package, int id, string vat = "")
        {
            CustomerName = customerName;
            StartTime = startTime;
            Email = email;
            Telephone = telephone;
            Package = package;
            Id = id;
            Vat = vat;
        }
    }
}
