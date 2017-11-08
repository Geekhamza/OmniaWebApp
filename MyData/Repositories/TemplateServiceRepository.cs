using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Repositories
{


    public class TemplateServiceRepository : RepositoryBase<TemplateService>, ITemplateServiceRepository
    {
        public TemplateServiceRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface ITemplateServiceRepository : IRepository<TemplateService>
    {

    }
}
