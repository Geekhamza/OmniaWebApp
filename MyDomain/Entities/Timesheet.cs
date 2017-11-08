using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomain.Entities
{
    public class Timesheet
    {
        public Timesheet()
        {
            date = DateTime.Today;
        }
        [Key]
        public long  Id { get; set; }
        public long  idUser { get; set; }
        public long idtache { get; set; }
        public DateTime date { get; set; }
        public double time { get; set; }

    }
}
