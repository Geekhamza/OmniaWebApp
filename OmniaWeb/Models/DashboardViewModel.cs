using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmniaWeb.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            tasks = new List<Tache>();
        }
        public int nb_user { get; set; }
        public int nb_project { get; set; }
        public int nb_finidhed_project { get; set; }
        public int nb_not_finished_project { get; set; }
        public List<Tache> tasks { get; set; }
    }
}