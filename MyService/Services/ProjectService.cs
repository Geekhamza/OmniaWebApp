using MyData.Infrastructure;
using MyDomain.Entities;
using OmniaWeb.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Services
{
    public class ProjectService : IProjectService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);

        public ProjectService()
        {
        }

        public IEnumerable<Project> GetAll()
        {
            var Projects = utOfWork.ProjectRepository.GetAll();
            return Projects;
        }

        public Project Get(long id)
        {
            var Project = utOfWork.ProjectRepository.GetById(id);
            return Project;
        }

        public void Create(Project Project)
        {
            utOfWork.ProjectRepository.Add(Project);
        }

        public void Delete(long id)
        {
            var Project = utOfWork.ProjectRepository.GetById(id);
            utOfWork.ProjectRepository.Delete(Project);
        }

        public void Save()
        {
            utOfWork.Commit();
        }

        public void Update(Project Project)
        {
            utOfWork.ProjectRepository.Update(Project);
        }


        public Project GetByName(string name)
        {
            return utOfWork.ProjectRepository.Get(x => x.Name == name);
        }
        public IEnumerable<Project> GetMyProject(string id)
        {
            
            var allProject= utOfWork.ProjectRepository.GetAll();
            List<Project> res = new List<Project>();
            foreach (var p in allProject)
            {
                if(ProjectUsers.isAuthorized(p.Id,id)){

                    res.Add(p);
                }
            }
            return res;
        }

       
    }

    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        Project Get(long id);
        Project GetByName(string name);
        IEnumerable<Project> GetMyProject(string idUser);
        void Create(Project Project);
        void Delete(long id);
        void Save();
        void Update(Project Project);
    }
}
