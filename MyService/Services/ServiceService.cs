using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class ServiceService : IServiceService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public ServiceService()
        {
        }

        public IEnumerable<Service> GetAll()
        {
            var p = utOfWork.ServiceRepository.GetAll();
            return p;
        }

        public Service Get(long id)
        {
            var p = utOfWork.ServiceRepository.GetById(id);
            return p;
        }

        public void Create(Service p)
        {
            utOfWork.ServiceRepository.Add(p);
        }

        public void Delete(long id)
        {
            var p = utOfWork.ServiceRepository.GetById(id);
            utOfWork.ServiceRepository.Delete(p);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(Service p)
        {
            utOfWork.ServiceRepository.Update(p);
        }





    }

    public interface IServiceService
    {
        IEnumerable<Service> GetAll();
        Service Get(long id);
        void Create(Service p);
        void Delete(long id);
        void Save();
        void Update(Service p);
    }
}
