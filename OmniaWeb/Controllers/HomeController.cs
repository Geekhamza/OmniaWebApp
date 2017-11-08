using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyService.Services;
using OmniaWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmniaWeb.Controllers
{
    public class HomeController : Controller
    {
       // initiate role manager & users manager 
        public ApplicationDbContext context;
        public RoleStore<IdentityRole> roleStore;
        public RoleManager<IdentityRole> roleManager;
        public UserStore<ApplicationUser> userStore;
        public UserManager<ApplicationUser> userManager { get; private set; }
        public ProjectService Pservice=new ProjectService();
        public TacheService taskS = new TacheService();

        public HomeController()
            
        {
            
            context = new ApplicationDbContext();
            roleStore = new RoleStore<IdentityRole>(context);
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            roleManager = new RoleManager<IdentityRole>(roleStore);
         }


        public ActionResult Index()
        {
            

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var context = new ApplicationDbContext();
            var allUsers = context.Users.ToList();
            var projects = Pservice.GetAll().ToList();
            var nb_finished = 0;
            foreach (var p in projects)
            {
                bool ok = true;
                foreach (var m in p.manholes)
                {
                    foreach (var s in m.services)
                    {

                        foreach (var t in s.Taches)
                        {
                            if (t.progresses.Count() > 0)
                                ok = (t.progresses.Last().progress != 100);
                        }
                    }
                    if (ok == true)
                    {
                        foreach (var j in m.joints)
                        {
                            foreach (var s in j.services)
                            {

                                foreach (var t in s.Taches)
                                {
                                    if (t.progresses.Count() > 0)
                                        ok = (t.progresses.Last().progress != 100);
                                }
                            }
                        }
                    }
                }
                foreach (var pehd in p.PEHDs)
                {
                    foreach (var s in pehd.services)
                    {

                        foreach (var t in s.Taches)
                        {
                            if (t.progresses.Count() > 0)
                                ok = (t.progresses.Last().progress != 100);
                        }
                    }
                    if (ok == true)
                    {
                        foreach (var tube in pehd.Tubes)
                        {
                            foreach (var s in tube.services)
                            {

                                foreach (var t in s.Taches)
                                {
                                    if (t.progresses.Count() > 0)
                                        ok = (t.progresses.Last().progress != 100);
                                }
                            }
                        }
                        foreach (var c in pehd.Cable)
                        {
                            foreach (var s in c.services)
                            {

                                foreach (var t in s.Taches)
                                {
                                    if (t.progresses.Count() > 0)
                                        ok = (t.progresses.Last().progress != 100);
                                }
                            }

                        }
                    }
                }
                if (!ok)
                    nb_finished++;
            }
            var model = new DashboardViewModel()
            {
                nb_user = allUsers.Count(),
                nb_project=projects.Count(),
                nb_not_finished_project=(projects.Count()-nb_finished),
                nb_finidhed_project=nb_finished
            };
            var tasks = taskS.GetAll().ToList();
            int cmp = 6,i=0;
            while (cmp > 0 && i < tasks.Count())
            {
                model.tasks.Add(tasks[tasks.Count()-i-1]);
                i++;
                cmp--;
            }
            return View(model);
        }
        [Authorize]
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [Authorize]
        public ActionResult Help()
        {
            return View();
        }


        [Authorize(Roles="admin")]
        public ActionResult Projects()
        {
            return View();
        }
        

    }
}