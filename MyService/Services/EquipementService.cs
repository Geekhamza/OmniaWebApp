using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class EquipementService : IEquipementService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public EquipementService()
        {
        }

        public IEnumerable<Equipement> GetAll()
        {
            var equipement = utOfWork.EquipementRepository.GetAll();
            return equipement;
        }

        //public IEnumerable<Equipement> GetByProject(long id) 
        //{
        //    var equipement = utOfWork.EquipementRepository.GetMany(x => x.ProjectRefId == id);
        //    return equipement;
        
        //}
        // Get onllly equipement type manhole
        public IEnumerable<Manhole> GetAllManhole()
        {
            var equipement = utOfWork.EquipementRepository.GetAll();
            List<Manhole> manholes=new List<Manhole>();    
            foreach( var e in equipement)
            {
                if (e is Manhole)
                    manholes.Add((Manhole)e);
            }
                    return manholes.AsEnumerable();
        }

         public IEnumerable<PEHD> GetAllPEHD()
        {
            var equipement = utOfWork.EquipementRepository.GetAll();
            List<PEHD> pehd=new List<PEHD>();    
            foreach( var e in equipement)
            {
                if (e is PEHD)
                    pehd.Add((PEHD)e);
            }
                    return pehd.AsEnumerable();

        }

         public IEnumerable<Tube> GetAllTube()
         {
             var equipement = utOfWork.EquipementRepository.GetAll();
             List<Tube> tubes = new List<Tube>();
             foreach (var e in equipement)
             {
                 if (e is Tube)
                     tubes.Add((Tube)e);
             }
             return tubes.AsEnumerable();
         }


         public IEnumerable<Joint> GetAlljoint()
         {
             var equipements = utOfWork.EquipementRepository.GetAll();
             List<Joint> joints = new List<Joint>();
             foreach (var e in equipements)
             {
                 if (e is Joint)
                     joints.Add((Joint)e);
             }
             return joints.AsEnumerable();
         }



         public IEnumerable<Cable> GetAllCable()
         {
             var equipement = utOfWork.EquipementRepository.GetAll();
             List<Cable> cables = new List<Cable>();
             foreach (var e in equipement)
             {
                 if (e is Cable)
                     cables.Add((Cable)e);
             }
             return cables.AsEnumerable();
         }

       

        public Equipement GetEquipementById(long id)
        {
            var equipement = utOfWork.EquipementRepository.GetById(id);
            return equipement;
        }

         
        public void Create(Equipement equipement)
        {
            utOfWork.EquipementRepository.Add(equipement);
        }

        public void Delete(long id)
        {
            var equipement = utOfWork.EquipementRepository.GetById(id);
            utOfWork.EquipementRepository.Delete(equipement);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(Equipement equipement)
        {
            utOfWork.EquipementRepository.Update(equipement);
        }


        public Equipement GetByRef(string e)
        {
            return utOfWork.EquipementRepository.Get(x => x.reference == e);
        }


    }

    public interface IEquipementService
    {
        IEnumerable<Equipement> GetAll();
        IEnumerable<Manhole> GetAllManhole();
        IEnumerable<PEHD> GetAllPEHD();
        IEnumerable<Tube> GetAllTube();
        IEnumerable<Joint> GetAlljoint();
        IEnumerable<Cable> GetAllCable();
        Equipement GetEquipementById(long id);
        Equipement GetByRef(string reference);
        void Create(Equipement equipement);
        void Delete(long id);
        void Save();
        void Update(Equipement equipement);
    }
}
