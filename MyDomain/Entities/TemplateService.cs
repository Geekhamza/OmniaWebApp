using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MyDomain.Entities
{
    public class TemplateService
    {
        public TemplateService()
        {
             Taches = new Collection <TemplateTache> ();
        }
        [Key]
        public long Id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double priceHT { get; set; }

        public long EquipementTemplaREfId { get; set; }

        [ForeignKey("EquipementTemplaREfId")]
        public virtual TemplateEquipement Templatequipement { get; set; }
        public virtual ICollection<TemplateTache> Taches { get; set; }
    }
}
