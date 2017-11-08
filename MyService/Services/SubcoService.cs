using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class SubcoService : ISubcoService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public SubcoService()
        {
        }

        public IEnumerable<Subco> GetAll()
        {
            var p = utOfWork.SubcoRepositoy.GetAll();
            return p;
        }

        public Subco Get(long id)
        {
            var p = utOfWork.SubcoRepositoy.GetById(id);
            return p;
        }

        public void Create(Subco p)
        {
            utOfWork.SubcoRepositoy.Add(p);
        }

        public void Delete(long id)
        {
            var p = utOfWork.SubcoRepositoy.GetById(id);
            utOfWork.SubcoRepositoy.Delete(p);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(Subco p)
        {
            utOfWork.SubcoRepositoy.Update(p);
        }





    }

    public interface ISubcoService
    {
        IEnumerable<Subco> GetAll();
        Subco Get(long id);
        void Create(Subco p);
        void Delete(long id);
        void Save();
        void Update(Subco p);
    }
}
