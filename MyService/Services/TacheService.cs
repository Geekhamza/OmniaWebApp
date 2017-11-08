using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class TacheService : ITacheService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public TacheService()
        {
        }

        public IEnumerable<Tache> GetAll()
        {
            var p = utOfWork.TacheRepository.GetAll();
            return p;
        }

        public Tache Get(long id)
        {
            var p = utOfWork.TacheRepository.GetById(id);
            return p;
        }

        public void Create(Tache p)
        {
            utOfWork.TacheRepository.Add(p);
        }

        public void Delete(long id)
        {
            var p = utOfWork.TacheRepository.GetById(id);
            utOfWork.TacheRepository.Delete(p);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(Tache p)
        {
            utOfWork.TacheRepository.Update(p);
        }





    }

    public interface ITacheService
    {
        IEnumerable<Tache> GetAll();
        Tache Get(long id);
        void Create(Tache p);
        void Delete(long id);
        void Save();
        void Update(Tache p);
    }
}
