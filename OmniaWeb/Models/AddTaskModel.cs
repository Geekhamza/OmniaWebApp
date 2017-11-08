using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmniaWeb.Models
{
    public class AddTaskModel
    {
        public AddTaskModel()
        {
            dateDepart = DateTime.Today;
            dateFin = DateTime.Today;
            
        }
        public string Nom { get; set; }
        public int dureEstime { get; set; }
        public int? dureeReel { get; set; }
        public DateTime dateDepart { get; set; }
        public DateTime dateFin { get; set; }
        public string Description { get; set; }
        public double progress { get; set; }

    }
}