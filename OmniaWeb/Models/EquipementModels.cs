
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OmniaWeb.Models
{
    public class AddManholeModel
    {
        
        public string reference { get; set; }
        
        public double latManhole { get; set; }
        
        public double lngManhole { get; set; }
        public string TypeCh { get; set; }
        public string EquipementTemplateModel { get; set; }
        
    }
    public class AddPEHDModel
    {
        public string reference { get; set; }
        public double PressionPEHD { get; set; }
        public double DiametrePEHD { get; set; }
        public double longeurPEHD { get; set; }
        public string ManholeSrc { get; set; }
        public string ManholeDist { get; set; }
        public string EquipementTemplateModel { get; set; }
    }
    public class AddJointModel
    {
        public string reference { get; set; }
        public string typeJoint { get; set; }
        public string  Manhole { get; set; }
        public string EquipementTemplateModel { get; set; }


    }
    public class AddCableModel
    {
        public string reference { get; set; }
        public string classFibre { get; set; }
        public string Typefibre { get; set; }
        public int nbrFibre { get; set; }
        public string StructureCable { get; set; }
        public double LongeurCable { get; set; }
        public string PEHDRefId { get; set; }
        public string EquipementTemplateModel { get; set; }


    }
    public class AddTubeModel
    {
        public string reference { get; set; }
        public string typeTube { get; set; }
        public double DiametreTube { get; set; }
        public double longeurTube { get; set; }
        public string PEHDRefId { get; set; }
        public string EquipementTemplateModel { get; set; }


    }
}