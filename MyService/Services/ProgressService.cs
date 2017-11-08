using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class ProgressService : IProgressService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public ProgressService()
        {
        }

        public IEnumerable<Progress> GetAll()
        {
            var p = utOfWork.ProgressRepository.GetAll();
            return p;
        }

        public Progress Get(long id)
        {
            var p = utOfWork.ProgressRepository.GetById(id);
            return p;
        }

        public void Create(Progress p)
        {
            utOfWork.ProgressRepository.Add(p);
        }

        public void Delete(long id)
        {
            var p = utOfWork.ProgressRepository.GetById(id);
            utOfWork.ProgressRepository.Delete(p);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(Progress p)
        {
            utOfWork.ProgressRepository.Update(p);
        }


        


    }

    public interface IProgressService
    {
        IEnumerable<Progress> GetAll();
        Progress Get(long id);
        void Create(Progress p);
        void Delete(long id);
        void Save();
        void Update(Progress p);
    }
}
