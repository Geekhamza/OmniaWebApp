using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MyDomain.Entities
{
    public class Subco
    {
        public Subco()
        {
            services = new Collection<Service>();
        }
        [Key]
        public long  id { get; set; }
        public string Name { get; set; }
        public string login { get; set; }
        public string  psw { get; set; }
        public string  Phone { get; set; }
        public string  Contact { get; set; }

        public virtual ICollection<Service> services { get; set; }



    }
}
