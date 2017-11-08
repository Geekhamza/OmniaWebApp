using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IClientRepository : IRepository<Client>
    {

    }
}