using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyDomain.Entities;
using MyService.Extra;
using MyService.Services;
using OmniaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmniaWeb.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
         // initiate role manager & users manager 
        
        ProjectService projectS = new ProjectService();
        EquipementService equipementS = new EquipementService();
        ServiceService serviceS = new ServiceService();
        TacheService taskS = new TacheService();
        ProgressService progressS = new ProgressService();
        public ApplicationDbContext context;
        public RoleStore<IdentityRole> roleStore;
        public RoleManager<IdentityRole> roleManager;
        public UserStore<ApplicationUser> userStore;
        public UserManager<ApplicationUser> userManager { get; private set; }
        public List<ApplicationUser> users = new List<ApplicationUser>();


        
        public TaskController()
            
        {
            context = new ApplicationDbContext();
            roleStore = new RoleStore<IdentityRole>(context);
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            roleManager = new RoleManager<IdentityRole>(roleStore);
            

            List<ApplicationUser> list;
            try
            {
                list = context.Users.ToList();
            }
            catch (Exception e)
            {
                list = new List<ApplicationUser>().ToList();

            }
            foreach (var u in list)
            {
                if (userManager.IsInRole(u.Id, "User"))
                {
                    users.Add(u);
                }
            }


        }

        //
        // GET: /Task/GetbyService/1
        public ActionResult GetByService(long id)
        {
            var all = taskS.GetAll();
            List<Tache> tasks = new List<Tache>();
            foreach (var t in all)
            {
                if (t.ServiceREfId == id)
                    tasks.Add(t);
            }
            ViewBag.id = id;
            ViewBag.user = userManager.FindByName(User.Identity.Name).Id;
            ViewBag.RefID = serviceS.Get(id).EquipementREfId;
            return View(tasks);
        }
        //
        // GET: /Task/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Task/Create
        public ActionResult Create(long RefID)
        {
            ViewBag.RefID = RefID;
            return View();
        }


        public ActionResult Affectation(long id)
        {
            
            ViewBag.users = users; 
            ViewBag.id = id;
            ViewBag.name = taskS.Get(id).Nom;
            return View();

        }
        [HttpPost]
        public ActionResult Affectation(AuthoriseUserModel model)
        {
                var context = new ApplicationDbContext();
                ViewBag.users = context.Users.ToList();


                ViewBag.users = users;
                ViewBag.id = model.ProjectId;
                ViewBag.name = taskS.Get(Convert.ToInt64(model.ProjectId)).Nom;
                

            try
                {
                    var list = new List<string>();
                    list.Add(model.UserId);
                    TaskUSers.AddUsers(list, Convert.ToInt64(model.ProjectId));
                    ViewBag.success = "User Affected Succesfully , Want to Affect Another ? ";
                    return View();
                }
                catch
                {
                    ViewBag.error = "Error While Affecting User";
                    return View(model);
                }
            }

        //
        // POST: /Task/Create
        [HttpPost]
        public ActionResult Create(long RefID,AddTaskModel model)
        {
            try
            {
                // TODO: Add insert logic here
                Tache t = new Tache()
                {
                    dateDepart = model.dateDepart,
                    dateFin = model.dateFin,
                    dureEstime = model.dureEstime,
                    Nom = model.Nom,
                    dureeReel=model.dureeReel,
                    Description = model.Description,
                    ServiceREfId = RefID
                };
                if (t.dateFin < t.dateDepart)
                    throw (new DivideByZeroException());

                Progress p = new Progress()
                {
                    date = DateTime.Today,
                    progress = 0
                };
                t.progresses.Add(p);
                taskS.Create(t);
                taskS.Save();
                return RedirectToAction("GetByService",new { id = RefID });
            }
            catch
            {
                ViewBag.RefID = RefID;
                ViewBag.error = "An Error Has Occured";
                return View();
            }
        }

        //
        // GET: /Task/Edit/5
        public ActionResult Edit(long id,long  RefID)
        {
            ViewBag.id = id;
            ViewBag.RefID = RefID;
            Tache t = taskS.Get(id);
            AddTaskModel model = new AddTaskModel()
            {
                dateDepart=t.dateDepart,
                dateFin=t.dateFin,
                Description=t.Description,
                dureeReel=t.dureeReel,
                dureEstime=t.dureEstime,
                Nom=t.Nom
            };
            List<Progress> list = new List<Progress>();
            list = progressS.GetAll().ToList();
            
            model.progress = 0.0;
            
            if (t.progresses.Count() > 0)
            {
                model.progress = t.progresses.Last().progress;
            }
            return View(model);
        }

        //
        // POST: /Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,long RefID, AddTaskModel model)
        {
            try
            {
               
                Tache t = taskS.Get(id);
                // TODO: Add update logic here
                t.Nom = model.Nom;
                t.dateDepart = model.dateDepart;
                t.dateFin = model.dateFin;
                t.Description = model.Description;
                t.dureeReel = model.dureeReel;
                t.dureEstime = model.dureEstime;
                if (t.dateFin < t.dateDepart||model.progress<0||model.progress>100)
                    throw (new DivideByZeroException());
                double last_progress = 0.0;
                if (t.progresses.Count() > 0)
                {
                     last_progress = t.progresses.Last().progress;
                }
                if (model.progress != last_progress)
                {
                    Progress p = new Progress()
                    {
                        date = DateTime.Today,
                        progress = model.progress
                    };

                    t.progresses.Add(p);
                }
                taskS.Update(t);
                taskS.Save();
                return RedirectToAction("GetByService", new { id = RefID });
            }
            catch
            {
                ViewBag.id = id;
                ViewBag.RefID = RefID;
                ViewBag.error = "An error has occured";
                return View(model);
            }
        }

        //
        // GET: /Task/Delete/5
        public ActionResult Delete(long  id,long RefID)
        {
            taskS.Delete(id);
            taskS.Save();
            return RedirectToAction("GetByService", new { id = RefID });
        }

       
    }
}
