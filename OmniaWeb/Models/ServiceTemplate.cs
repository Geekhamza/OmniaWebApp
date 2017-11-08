using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace OmniaWeb.Models
{
    public class ServiceTemplateModel
    {
            public long Id { get; set; }
            public long EquipementRefId { get; set; }
            public string Name { get; set; }
            public double price { get; set; }
            public double priceHT { get; set; }
            public int nb_Tasks { get; set; }
    }
    public class AddServiceTemplateModel
    {

      
        public long Id { get; set; }
        public long EquipementRefId { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public double priceHT { get; set; }
       

    }
}