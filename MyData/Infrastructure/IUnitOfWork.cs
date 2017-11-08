using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyData.Repositories;

namespace MyData.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        IProjectRepository ProjectRepository { get; }
        IEquipementRepository EquipementRepository { get; }
        IServiceRepository ServiceRepository { get; }
        ITacheRepository TacheRepository { get; }
        IProgressRepository ProgressRepository { get; }
        ISubcoRepositoy SubcoRepositoy { get; }
        IRapportPhotoRepository  RapportPhotoRepository{ get; }
        IClientRepository ClientRepository  { get; }
        ITimesheetRepository TimesheetRepository { get; }
        ITemplateEquipementRepository TemplateEquipementRepository { get; }
        ITemplateServiceRepository TemplateServiceRepository { get; }
        ITemplateTacheRepository TemplateTacheRepository { get; }


    }
}
