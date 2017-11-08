using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class TimesheetService : ITimesheetService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public TimesheetService()
        {
        }

        public IEnumerable<Timesheet> GetAll()
        {
            var p = utOfWork.TimesheetRepository.GetAll();
            return p;
        }

        public Timesheet Get(long id)
        {
            var p = utOfWork.TimesheetRepository.GetById(id);
            return p;
        }

        public void Create(Timesheet p)
        {
            utOfWork.TimesheetRepository.Add(p);
        }

        public void Delete(long id)
        {
            var p = utOfWork.TimesheetRepository.GetById(id);
            utOfWork.TimesheetRepository.Delete(p);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(Timesheet p)
        {
            utOfWork.TimesheetRepository.Update(p);
        }





    }

    public interface ITimesheetService
    {
        IEnumerable<Timesheet> GetAll();
        Timesheet Get(long id);
        void Create(Timesheet p);
        void Delete(long id);
        void Save();
        void Update(Timesheet p);
    }
}
