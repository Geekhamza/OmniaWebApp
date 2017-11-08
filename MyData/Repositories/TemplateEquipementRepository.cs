using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Repositories
{
    
    
    public class TemplateEquipementRepository : RepositoryBase<TemplateEquipement>, ITemplateEquipementRepository
    {
        public TemplateEquipementRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface ITemplateEquipementRepository : IRepository<TemplateEquipement>
    {

    }
}
