using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MyDomain.Entities
{
    public class Tache
    {
        public Tache()
        {
            progresses = new Collection<Progress>();
            dateDepart = DateTime.Today;
            dateFin = DateTime.Today;

        }
        [Key]
        public long  Id { get; set; }
        public string Nom { get; set; }
        public string AffectedUSers { get; set; }
        public int dureEstime { get; set; }
        public int? dureeReel { get; set; }
        public DateTime dateDepart { get; set; }
        public DateTime dateFin { get; set; }
        public string  Description { get; set; }
        public long ServiceREfId { get; set; }
        

        [ForeignKey("ServiceREfId")]
        public virtual Service service { get;set;}
        public virtual ICollection<Progress> progresses { get; set; }
        
        

    }
}
