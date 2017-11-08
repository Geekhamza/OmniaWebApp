using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomain.Entities
{
    public class TemplateEquipement
    {
        public TemplateEquipement()
        {
            services = new Collection<TemplateService>();
        }
        [Key]
        public long Id { get; set; }
        public string  Name { get; set; }
        public TypeTemplate Type { get; set; }
        public virtual ICollection <TemplateService> services {get; set;}

    }
    public enum TypeTemplate {Manhole,PEHD,Joint,Cable,Tube};
}
