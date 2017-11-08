using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Repositories
{
    public class SubcoRepositoy : RepositoryBase<Subco>, ISubcoRepositoy
    {
        public SubcoRepositoy(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface ISubcoRepositoy : IRepository<Subco>
    {

    }
}
