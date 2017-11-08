using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmniaWeb.Models
{
    public class TaskTemplateModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int dureeEstime { get; set; }
        public long ServiceRefID { get; set; }
        
    }
    public class AddTaskTemplateModel
    {

        
        public string Name { get; set; }
        public int dureeEstime { get; set; }
        

    }
}
