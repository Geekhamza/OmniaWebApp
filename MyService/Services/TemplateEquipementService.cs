using MyData.Infrastructure;
using MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class TemplateEquipementService : ITemplateEquipementService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public TemplateEquipementService()
        {
        }
        public IEnumerable<TemplateEquipement> GetAll()
        {
            var templates = utOfWork.TemplateEquipementRepository.GetAll();
            return templates;
        }

        public TemplateEquipement Get(long id)
        {
            var template = utOfWork.TemplateEquipementRepository.GetById(id);
            return template;
        }
        public IEnumerable< TemplateEquipement> GetByType(TypeTemplate type)
        {
            var template = utOfWork.TemplateEquipementRepository.GetMany(x=>x.Type==type);
            return template;
        }

        public void Create(TemplateEquipement templateEquipement)
        {
            utOfWork.TemplateEquipementRepository.Add(templateEquipement);
        }

        public void Delete(long id)
        {
            var template = utOfWork.TemplateEquipementRepository.GetById(id);
            utOfWork.TemplateEquipementRepository.Delete(template);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(TemplateEquipement templateEquipement)
        {
            utOfWork.TemplateEquipementRepository.Update(templateEquipement);
        }


        



    }
    public interface ITemplateEquipementService
    {
        IEnumerable<TemplateEquipement> GetAll();
        TemplateEquipement Get(long id);
        IEnumerable<TemplateEquipement> GetByType(TypeTemplate type);
        void Create(TemplateEquipement template);
        void Delete(long id);
        void Save();
        void Update(TemplateEquipement template);
    }
}
