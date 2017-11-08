using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmniaWeb.Models
{
    public class ClientDetailsModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public string ContactPM { get; set; }
        public string ContactFinancier { get; set; }

        public virtual List<Project> projects { get; set; }

    }
    public class AddClientModel
    {

        public string name { get; set; }
        public string ContactPM { get; set; }
        public string ContactFinancier { get; set; }

        public virtual List<Project> projects { get; set; }
    }

}