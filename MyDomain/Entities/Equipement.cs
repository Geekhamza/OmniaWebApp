using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomain.Entities
{
    public abstract class Equipement
    {
         public Equipement () 
        {
            services = new Collection<Service>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)] //zayda ema mnfass5ch bich man3awdch tbildi nokssod bih   role id
        public long  Id { get; set; }
        public string reference { get; set; }
       
        public virtual ICollection<Service> services { get; set; } // 3andou ensemble services 1..*

    }
    public class Manhole : Equipement
    {
        public Manhole()
        {
            joints = new Collection<Joint>();
        }
        public double latManhole { get; set; }
        public double lngManhole { get; set; }
        public string  TypeCh { get; set; }
        public long ProjectRefId { get; set; }

        [ForeignKey("ProjectRefId")]
        public virtual Project project { get; set; } 

        
        public virtual ICollection<Joint> joints { get; set; } // 1..*
       
    }

    public class Cable : Equipement
    {
        public string  classFibre { get; set;  }
        public string  Typefibre { get; set; }
        public int nbrFibre { get; set; }
        public string  StructureCable { get; set; }
        public double LongeurCable { get; set; }
        public long PEHDRefId { get; set; }


        [ForeignKey("PEHDRefId")]
        public virtual PEHD pehd { get; set; }
        
    }


    public class Joint :Equipement
    {
        public string typeJoint { get; set; }
        public long  ManholeId { get; set; }
        

        [ForeignKey("ManholeId")]
        public virtual Manhole manhole { get; set; }
    }
    public class Tube:Equipement
    {
        public TypeTube typeTube { get; set; }
        public double  DiametreTube { get; set; }
        public double longeurTube { get; set; }
        public long PEHDRefId { get; set; }


        [ForeignKey("PEHDRefId")]
        public virtual PEHD pehd { get; set; }

    }
    public enum TypeTube { PVC, Glavanise }


    public class PEHD:Equipement
    {
        public PEHD()
        {
            Collection<Tube> Tubes = new Collection<Tube>();
            Collection<Cable> Cable = new Collection<Cable>();
            
        }

        public double PressionPEHD { get; set; }
        public double DiametrePEHD { get; set; }
        public double longeurPEHD { get; set; }
        public long IdManholeSrc { get; set; }
        public long IdManholeDist { get; set; }
        public long? ProjectRefId { get; set; }

        [ForeignKey("ProjectRefId")]
        public virtual Project project { get; set; }
        public virtual ICollection<Tube> Tubes { get; set; }
        public virtual ICollection<Cable> Cable { get; set; }
            


    }
   
}
