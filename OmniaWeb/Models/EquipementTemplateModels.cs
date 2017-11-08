using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmniaWeb.Models
{
    public class EquipementTemplateModels
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int nb_services { get; set; }
    }
    public class AddEquipementTemplateModels
    {
        public string Name { get; set; }
        public string Type { get; set; }
        
    }


}