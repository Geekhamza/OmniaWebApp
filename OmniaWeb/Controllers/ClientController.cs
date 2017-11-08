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
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OmniaWeb.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
       // initiate role manager & users manager 
        public ApplicationDbContext context;
        public RoleStore<IdentityRole> roleStore;
        public RoleManager<IdentityRole> roleManager;
        public UserStore<ApplicationUser> userStore;
        public UserManager<ApplicationUser> userManager { get; private set; }
        public ProjectService Pservice=new ProjectService();
        public ClientService Cservice = new ClientService();


        public ClientController()
            
        {
            
            context = new ApplicationDbContext();
            roleStore = new RoleStore<IdentityRole>(context);
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            roleManager = new RoleManager<IdentityRole>(roleStore);
         }



 

        //
        // GET: /Client/
        [Authorize(Roles="admin,DG")]
        public ActionResult Index()
        {
            var clients = Cservice.GetAll();
            var list = new List<ClientDetailsModel>();
            foreach (var c in clients)
            {
                var tmp = new ClientDetailsModel()
                {
                    ContactFinancier = c.ContactFinancier,
                    ContactPM = c.ContactPM,
                    id = c.id,
                    name = c.name,
                    projects = c.projects.ToList()
                };
                list.Add(tmp);
            }
            return View(list);
        }

        //
        // GET: /Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
       
        //
        // GET: /Client/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Client/Create
        [HttpPost]
        public ActionResult Create(AddClientModel client)
        {
            try
            {
                // TODO: Add insert logic here
                var c = new Client()
                {
                    ContactFinancier = client.ContactFinancier,
                    ContactPM = client.ContactPM,
                    name = client.name
                };
                Cservice.Create(c);
                Cservice.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.error = " An error Occured While inserting the new client";
                return View(client);
            }
        }

        //
        // GET: /Client/Edit/5
        public ActionResult Edit(int id)
        {
            var c = Cservice.Get(id);
            var client = new ClientDetailsModel()
            {
                id = c.id,
                name = c.name,
                projects = c.projects.ToList(),
                ContactFinancier = c.ContactFinancier,
                ContactPM = c.ContactPM

            };
            return View(client);
        }

        //
        // POST: /Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClientDetailsModel client)
        {
            try
            {
                // TODO: Add update logic here
                var c = Cservice.Get(client.id);
                c.name = client.name;
                c.ContactPM = client.ContactPM;
                c.ContactFinancier = client.ContactFinancier;
                Cservice.Update(c);
                Cservice.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.error = "An Error Has Occured While Updating the Client";
                return View(client);
            }
        }

        //
        // GET: /Client/Delete/5
        public ActionResult Delete(int id)
        {

            try
            {
                Cservice.Delete(id);
                Cservice.Save();
                return RedirectToAction("Index");


            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Client/Delete/5
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
