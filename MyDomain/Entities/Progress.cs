using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MyDomain.Entities
{
    public class Progress
    {
        public Progress()
        {
            date = DateTime.Today; //na5ou date lyoum par défaut
        }

        [Key]
        public long  id { get; set; }
        public double progress { get; set; }
        public DateTime date { get; set; }
        public long TacheRefId { get; set; }

        [ForeignKey("TacheRefId")]
        public virtual Tache tache { get; set; }

    }
}
