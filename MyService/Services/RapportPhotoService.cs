using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class RapportPhotoService : IRapportPhotoService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public RapportPhotoService()
        {
        }

        public IEnumerable<RapportPhoto> GetAll()
        {
            var p = utOfWork.RapportPhotoRepository.GetAll();
            return p;
        }

        public RapportPhoto Get(long id)
        {
            var p = utOfWork.RapportPhotoRepository.GetById(id);
            return p;
        }

        public void Create(RapportPhoto p)
        {
            utOfWork.RapportPhotoRepository.Add(p);
        }

        public void Delete(long id)
        {
            var p = utOfWork.RapportPhotoRepository.GetById(id);
            utOfWork.RapportPhotoRepository.Delete(p);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(RapportPhoto p)
        {
            utOfWork.RapportPhotoRepository.Update(p);
        }





    }

    public interface IRapportPhotoService
    {
        IEnumerable<RapportPhoto> GetAll();
        RapportPhoto Get(long id);
        void Create(RapportPhoto p);
        void Delete(long id);
        void Save();
        void Update(RapportPhoto p);
    }
}
