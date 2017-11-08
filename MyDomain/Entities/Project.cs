using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomain.Entities
{
    
    public class Project
    {
        public Project()
        {
            datDebut = DateTime.Today;
            datFin = DateTime.Today;
            datFinEstime = DateTime.Today;
            manholes = new Collection<Manhole>();
            PEHDs = new Collection<PEHD>();
            
        }

        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Country { get; set; }
        // list  of authorised user access 
        public string OwnerId { get; set; }
        public string Users { get; set; }
        public long ClientRefId { get; set; }
        public DateTime datDebut { get; set; }
        public DateTime datFin { get; set; }
        public DateTime datFinEstime { get; set; }
        public virtual ICollection<Manhole> manholes { get; set; }
        public virtual ICollection<PEHD> PEHDs { get; set; }
        [ForeignKey("ClientRefId")]
        public virtual Client client { get; set; }
        
        

        
    }
}
