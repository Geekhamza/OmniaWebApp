using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyData.Infrastructure;
using MyDomain.Entities;
using MyService.Services;
using OmniaWeb.Extra;
using OmniaWeb.Models;
using OmniaWeb.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace OmniaWeb.Controllers
{
    public class StatController : Controller
    {
        ProjectService projectS = new ProjectService();
        EquipementService equipementS = new EquipementService();
        ServiceService serviceS = new ServiceService();
        TacheService taskS = new TacheService();
        ProgressService progressS = new ProgressService();

        // initiate role manager & users manager 
        public ApplicationDbContext context;
        public RoleStore<IdentityRole> roleStore;
        public RoleManager<IdentityRole> roleManager;
        public UserStore<ApplicationUser> userStore;
        public UserManager<ApplicationUser> userManager { get; private set; }
        public StatController()
        {

            context = new ApplicationDbContext();
            roleStore = new RoleStore<IdentityRole>(context);
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            roleManager = new RoleManager<IdentityRole>(roleStore);
        }

        // GET: Stat
        [Authorize(Roles="admin,DG")]
        public ActionResult Index()
        {
            var allTasks = taskS.GetAll().ToList();
            var allService = serviceS.GetAll().ToList();
            var allprojects=projectS.GetAll().ToList();
            long finishedTask = 0, finishedServices = 0, opnedServices = 0, InprogressServices = 0;
            // to make sure each task calculated once 
          //  bool done = false;
          
            foreach( var s in allService)
            {
                long serviceFinishedTask = 0, serviceInprogressTask = 0, total = 0;
                foreach (var t in allTasks)
                {


                    if (t.ServiceREfId == s.Id)
                    {
                        total++;

                        if (t.progresses.Count() > 0 && t.progresses.Last().progress == 100)
                        {
                            finishedTask++;
                        }
                        if (t.progresses.Count() > 0 && t.progresses.Last().progress == 100)
                        {
                            serviceFinishedTask++;
                        }
                        else if ((t.progresses.Count() > 0 && t.progresses.Last().progress < 100 && t.progresses.Last().progress > 0))
                        {
                            serviceInprogressTask++;

                        }

                    }

                }

                if (total == serviceFinishedTask)
                {
                    finishedServices++;
                }
                else if (serviceInprogressTask ==0&&serviceFinishedTask==0)
                {

                    opnedServices++;
                }
                else
                {
                    InprogressServices++;
                }

            }
            StatModel model = new StatModel()
            {
                finishedService=finishedServices,
                InprogressService=InprogressServices,
                finishedTask=finishedTask,
                OpnedService=opnedServices,
                OpnedTask=(allTasks.Count()-finishedTask),
                ownersProjects=new Dictionary<string, int>()
            };

            foreach (var p in allprojects)
            {
                var owner = userManager.FindById(p.OwnerId).UserName;
                if (model.ownersProjects.ContainsKey(owner)==false)
                {
                    model.ownersProjects.Add(owner,0);
                }
                model.ownersProjects[owner.ToString()]++;
            }
           
            return View(model);
        }

        // GET: Stat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
