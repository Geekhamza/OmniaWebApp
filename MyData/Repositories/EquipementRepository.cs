using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Repositories
{
    public class EquipementRepository : RepositoryBase<Equipement>, IEquipementRepository
    {
        public EquipementRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IEquipementRepository : IRepository<Equipement>
    {

    }
}
