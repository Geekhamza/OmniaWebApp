using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyDomain.Entities;
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
    public class TaskTemplateController : Controller
    {
          // initiate role manager & users manager 
        TemplateTacheService templatesTacheS = new TemplateTacheService();
        TemplateServiceService templatesServiceS = new TemplateServiceService();
        TemplateEquipementService templateEquipementServiceS = new TemplateEquipementService();
        public ApplicationDbContext context;
        public RoleStore<IdentityRole> roleStore;
        public RoleManager<IdentityRole> roleManager;
        public UserStore<ApplicationUser> userStore;
        public UserManager<ApplicationUser> userManager { get; private set; }


        public TaskTemplateController()
            
        {
            
            context = new ApplicationDbContext();
            roleStore = new RoleStore<IdentityRole>(context);
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            roleManager = new RoleManager<IdentityRole>(roleStore);
         }


        //
        // GET: /TaskTemplate/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TaskTemplate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TaskTemplate/Create
        public ActionResult Create(long id)
        {
            // id represent Ref ID to service Template 
            ViewBag.id = id;
            return View();
        }

        //
        // POST: /TaskTemplate/Create
        [HttpPost]
        public ActionResult Create(AddTaskTemplateModel model, long RefID)
        {
            try
            {
                // TODO: Add insert logic here
                var template = new TemplateTache()
                {
                    Nom = model.Name,
                    dureEstime = model.dureeEstime,
                    TemplateServiceREfId=RefID
                };
                templatesServiceS.Get(RefID).Taches.Add(template);
                templatesServiceS.Save();
                return RedirectToAction("Details", "ServiceTemplates", new { id = RefID });
            }
            catch
            {
                ViewBag.id = RefID;
                ViewBag.error = "An error Occured While inserting the new Template";
                return View(model);
            }
        }

        //
        // GET: /TaskTemplate/Edit/5
        public ActionResult Edit(long id)
        {
            TemplateTache template = templatesTacheS.Get(id);
            var model = new TaskTemplateModel()
            {
              dureeEstime=template.dureEstime,
              Id=template.Id,
              Name=template.Nom,
              ServiceRefID=template.TemplateServiceREfId
            };
            return View(model);
        }

        //
        // POST: /TaskTemplate/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, TaskTemplateModel model)
        {
            try
            {
                // TODO: Add update logic here
                var old = templatesTacheS.Get(id);
                old.Nom = model.Name;
                old.dureEstime = model.dureeEstime;
                templatesTacheS.Update(old);
                templatesTacheS.Save();
                return RedirectToAction("Details", "ServiceTemplates", new { id = old.TemplateServiceREfId });
            }
            catch
            {
                ViewBag.error = " An error Occured";
                return View(model);
            }
        }

        //
        // GET: /TaskTemplate/Delete/5
        public ActionResult Delete(int id)
        {
            var child = templatesTacheS.Get(id);
            long RefID = child.TemplateServiceREfId;
            templatesTacheS.Delete(id);
            templatesTacheS.Save();
            return RedirectToAction("Details", "ServiceTemplates", new { id = RefID });
        }

       
    }
}
