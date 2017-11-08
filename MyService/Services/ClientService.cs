using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class ClientService : IClientService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public ClientService()
        {
        }

        public IEnumerable<Client> GetAll()
        {
            var client = utOfWork.ClientRepository.GetAll();
            return client;
        }

        public Client Get(long id)
        {
            var client = utOfWork.ClientRepository.GetById(id);
            return client;
        }

        public void Create(Client client)
        {
            utOfWork.ClientRepository.Add(client);
        }

        public void Delete(long id)
        {
            var client = utOfWork.ClientRepository.GetById(id);
            utOfWork.ClientRepository.Delete(client);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(Client client)
        {
            utOfWork.ClientRepository.Update(client);
        }


        public Client GetByName(string name)
        {
            return utOfWork.ClientRepository.Get(x => x.name == name);
        }


    }

    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client Get(long id);
        Client GetByName(string nom);
        void Create(Client Project);
        void Delete(long id);
        void Save();
        void Update(Client Project);
    }
}
