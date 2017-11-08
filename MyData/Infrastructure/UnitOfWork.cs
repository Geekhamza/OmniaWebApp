using MyData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyContext dataContext;
        DatabaseFactory dbFactory;
        public UnitOfWork(DatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }
        private IProjectRepository projectRepository;
        IProjectRepository IUnitOfWork.ProjectRepository
        {
            get
            {
                return projectRepository ?? (projectRepository = new ProjectRepository(dbFactory));
            }
        }
        private IClientRepository clientRepository;
        IClientRepository IUnitOfWork.ClientRepository
        {
            get
            {
                return clientRepository ?? (clientRepository = new ClientRepository(dbFactory));
            }
        }
        private IEquipementRepository equipementRepository;
        IEquipementRepository IUnitOfWork.EquipementRepository
        {
            get
            {
                return equipementRepository ?? (equipementRepository = new EquipementRepository(dbFactory));
            }
        }
        private IServiceRepository serviceRepository;
        IServiceRepository IUnitOfWork.ServiceRepository
        {
            get
            {
                return serviceRepository ?? (serviceRepository = new ServiceRepository(dbFactory));
            }
        }
        private ITacheRepository tacheRepository;
        ITacheRepository IUnitOfWork.TacheRepository
        {
            get
            {
                return tacheRepository ?? (tacheRepository = new TacheRepository(dbFactory));
            }
        }
        private IProgressRepository progressRepository;
        IProgressRepository IUnitOfWork.ProgressRepository
        {
            get
            {
                return progressRepository ?? (progressRepository = new ProgressRepository(dbFactory));
            }
        }
        private ISubcoRepositoy subcoRepositoy;
        ISubcoRepositoy IUnitOfWork.SubcoRepositoy
        {
            get
            {
                return subcoRepositoy ?? (subcoRepositoy = new SubcoRepositoy(dbFactory));
            }
        }
        private IRapportPhotoRepository rapportPhotoRepository;
        IRapportPhotoRepository IUnitOfWork.RapportPhotoRepository
        {
            get
            {
                return rapportPhotoRepository ?? (rapportPhotoRepository = new RapportPhotoRepository(dbFactory));
            }
        }
        private ITimesheetRepository timesheetRepository;
        ITimesheetRepository IUnitOfWork.TimesheetRepository
        {
            get
            {
                return timesheetRepository ?? (timesheetRepository = new TimesheetRepository(dbFactory));
            }
        }
        private ITemplateEquipementRepository templateEquipementRepository;
        ITemplateEquipementRepository IUnitOfWork.TemplateEquipementRepository
        {
            get
            {
                return templateEquipementRepository ?? (templateEquipementRepository = new TemplateEquipementRepository(dbFactory));
            }
        }

        private ITemplateServiceRepository templateServiceRepository;
        ITemplateServiceRepository IUnitOfWork.TemplateServiceRepository
        {
            get
            {
                return templateServiceRepository ?? (templateServiceRepository = new TemplateServiceRepository(dbFactory));
            }
        }
        private ITemplateTacheRepository templateTacheRepository;
        ITemplateTacheRepository IUnitOfWork.TemplateTacheRepository
        {
            get
            {
                return templateTacheRepository ?? (templateTacheRepository = new TemplateTacheRepository(dbFactory));
            }
        }
        

        protected MyContext DataContext
        {
            get
            {
                return dataContext ?? (dataContext = dbFactory.Get());
            }
        }        
        public void Commit()
        {
            DataContext.Commit();
        }
           


    }
}
