using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Repositories
{
    public class RapportPhotoRepository : RepositoryBase<RapportPhoto>, IRapportPhotoRepository
    {
        public RapportPhotoRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IRapportPhotoRepository : IRepository<RapportPhoto>
    {

    }
}
