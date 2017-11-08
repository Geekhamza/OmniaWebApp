using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Repositories
{
    public class ProgressRepository : RepositoryBase<Progress>, IProgressRepository
    {
        public ProgressRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IProgressRepository : IRepository<Progress>
    {

    }
}
