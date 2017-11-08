using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmniaWeb.Models
{
    public class AddServiceModel
    {
        public string Name { get; set; }
        public double price { get; set; }
        public double priceHT { get; set; }

    }
    public class ServiceDetailsModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public double priceHT { get; set; }
        public int nbTask { get; set; }
        public int finished { get; set; }

    }
}