using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Repositories
{
    public class TemplateTacheRepository : RepositoryBase<TemplateTache>, ITemplateTacheRepository
    {
        public TemplateTacheRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface ITemplateTacheRepository : IRepository<TemplateTache>
    {

    }
}
