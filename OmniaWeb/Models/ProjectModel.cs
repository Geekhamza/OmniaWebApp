using MyDomain.Entities;
using OmniaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmniaWeb.MyModels
{
    
    public class AddProjectViewModel
    {
        public AddProjectViewModel ()
        {
            datDebut = DateTime.Today;
            datFin = DateTime.Today;
            datFinEstime = DateTime.Today;
        }
        public string  Name { get; set; }
        public string Zone { get; set; }
        public string Country { get; set; }
        public DateTime datDebut { get; set; }
        public DateTime datFin { get; set; }
        public DateTime datFinEstime { get; set; }
        public string OwnerID { get; set; }
        public string client { get; set; }
    }
    public class ProjecDetailstModel
    {
        public ProjecDetailstModel()
        {
            PEHDsdetails = new List<PEHDProjectModel>();
            manholesdetails = new List<ManholeProjectModel>();
            datDebut = DateTime.Today;
            datFin = DateTime.Today;
            datFinEstime = DateTime.Today;
            

        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Country { get; set; }
        public DateTime datDebut { get; set; }
        public DateTime datFin { get; set; }
        public DateTime datFinEstime { get; set; }
        public List<PEHDProjectModel> PEHDsdetails { get; set; }
        public List<ManholeProjectModel> manholesdetails { get; set; }
        public string client { get; set; }
        public string Owner { get; set; }
        public string OwnerID { get; set; }
        public long nb_equipment { get; set; }
        public long nb_services { get; set; }


    }
    public class ManholeProjectModel
    {
        public long Id { get; set; }
        public double progress { get; set; }
        public string  reference { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }

    }
    public class PEHDProjectModel
    {
        public long Id { get; set; }
        public double progress { get; set; }
        public double latSrc { get; set; }
        public double lngSrc { get; set; }
        public double latDist { get; set; }
        public double lngDist { get; set; }

    }
    public class EditProjectViewModel
    {
        public EditProjectViewModel()
        {
            datDebut = DateTime.Today;
            datFin = DateTime.Today;
            datFinEstime = DateTime.Today;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Country { get; set; }
        public DateTime datDebut { get; set; }
        public DateTime datFin { get; set; }
        public DateTime datFinEstime { get; set; }
        public string client { get; set; }
        public string Owner { get; set; }
        public string OwnerID { get; set; }
    }
    
}
