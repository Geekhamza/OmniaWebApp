using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MyDomain.Entities
{
    public class RapportPhoto
    {
        [Key]
        public long  id { get; set; }
        public double  lat { get; set; }
        public double lng { get; set; }
        public string photo { get; set; }
        public double distance { get;set; }
        public string type { get; set; }
        public string descrition { get; set; }
        public long ServiceRefId { get; set; }

        [ForeignKey("ServiceRefId")]
        public virtual Service service { get; set; }
    }
}
