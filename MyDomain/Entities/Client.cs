using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MyDomain.Entities
{
    public class Client
    {
        public Client()
        {
            projects = new Collection<Project>(); //bich matjinich liste vide (erreur)
        }
        [Key]
        public long  id { get; set; }
        public string name { get; set; }
        public string ContactPM { get; set; }
        public string  ContactFinancier { get; set; }

        public virtual ICollection<Project> projects { get; set; } //relation 1..*


    }
}
