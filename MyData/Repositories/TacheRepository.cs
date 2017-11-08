using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Repositories
{
     public class TacheRepository : RepositoryBase<Tache>, ITacheRepository
    {
         public TacheRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface ITacheRepository : IRepository<Tache>
    {

    }
}
