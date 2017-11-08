using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmniaWeb.Models
{
    public class StatModel
    {
        public StatModel()
        {
            ownersProjects = new Dictionary<string, int>();
        }
        public double  finishedTask { get; set; }
        public double OpnedTask { get; set; }
        public Dictionary<string, int> ownersProjects { get; set;}
        public double finishedService { get; set; }
        public double OpnedService { get; set; }
        public double InprogressService { get; set; }


    }
}