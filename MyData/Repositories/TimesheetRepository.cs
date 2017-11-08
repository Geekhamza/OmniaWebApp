using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyData.Repositories
{
    public class TimesheetRepository : RepositoryBase<Timesheet>, ITimesheetRepository
    {
         public TimesheetRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface ITimesheetRepository : IRepository<Timesheet>
    {

    }
}
