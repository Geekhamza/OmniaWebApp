using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
     

    public class TemplateServiceService : ITemplateServiceService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public TemplateServiceService()
        {
        }
        public IEnumerable<TemplateService> GetAll()
        {
            var templates = utOfWork.TemplateServiceRepository.GetAll();
            return templates;
        }

        public TemplateService Get(long id)
        {
            var template = utOfWork.TemplateServiceRepository.GetById(id);
            return template;
        }

        public void Create(TemplateService templateService)
        {
            utOfWork.TemplateServiceRepository.Add(templateService);
        }

        public void Delete(long id)
        {
            var template = utOfWork.TemplateServiceRepository.GetById(id);
            utOfWork.TemplateServiceRepository.Delete(template);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(TemplateService templateService)
        {
            utOfWork.TemplateServiceRepository.Update(templateService);
        }






    }
    public interface ITemplateServiceService
    {
        IEnumerable<TemplateService> GetAll();
        TemplateService Get(long id);
        void Create(TemplateService template);
        void Delete(long id);
        void Save();
        void Update(TemplateService template);
    }
}
