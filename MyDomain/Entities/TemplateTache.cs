using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MyDomain.Entities
{
    public class TemplateTache
    {
        public long Id { get; set; }
        public string Nom { get; set; }
        public int dureEstime { get; set; }
        public string Description { get; set; }
        public long TemplateServiceREfId { get; set; }


        [ForeignKey("TemplateServiceREfId")]
        public virtual TemplateService Templateservice { get; set; }

    }
}
