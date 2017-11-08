using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{

    public class TemplateTacheService : ITemplateTacheService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public IEnumerable<TemplateTache> GetAll()
        {
            var templates = utOfWork.TemplateTacheRepository.GetAll();
            return templates;
        }


        public TemplateTache Get(long id)
        {
            var template = utOfWork.TemplateTacheRepository.GetById(id);
            return template;
        }

        public void Create(TemplateTache templateTache)
        {
            utOfWork.TemplateTacheRepository.Add(templateTache);
        }

        public void Delete(long id)
        {
            var template = utOfWork.TemplateTacheRepository.GetById(id);
            utOfWork.TemplateTacheRepository.Delete(template);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(TemplateTache templateTache)
        {
            utOfWork.TemplateTacheRepository.Update(templateTache);
        }






     


    }
    public interface ITemplateTacheService
        {
            IEnumerable<TemplateTache> GetAll();
            TemplateTache Get(long id);
            void Create(TemplateTache template);
            void Delete(long id);
            void Save();
            void Update(TemplateTache template);
        }
}
