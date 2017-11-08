using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MyDomain.Entities
{
    public class Service
    {
         public Service() 
        {
            Taches = new Collection<Tache>();
            subcos = new Collection<Subco>();
            photos = new Collection<RapportPhoto>();
        }
        [Key]
        public long  Id { get; set; }
        public string Name { get; set; }
        public double  price  { get; set; }
        public double priceHT { get; set; }
        
        public long EquipementREfId { get; set; }

        [ForeignKey("EquipementREfId")]
        public virtual Equipement equipement { get; set; }
        public virtual ICollection<Tache> Taches { get; set; }
        public virtual ICollection<Subco> subcos { get; set; }
        public virtual ICollection<RapportPhoto> photos { get; set; }

        

    }
}
