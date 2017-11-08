using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyDomain.Entities;
using MyService.Services;
using OmniaWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmniaWeb.Controllers
{
    public class RapportPhotoController : Controller
    {
        // initiate role manager & users manager 

        ProjectService projectS = new ProjectService();
        EquipementService equipementS = new EquipementService();
        ServiceService serviceS = new ServiceService();
        TacheService taskS = new TacheService();
        RapportPhotoService photoS = new RapportPhotoService();
        public ApplicationDbContext context;
        public RoleStore<IdentityRole> roleStore;
        public RoleManager<IdentityRole> roleManager;
        public UserStore<ApplicationUser> userStore;
        public UserManager<ApplicationUser> userManager { get; private set; }
        public ProjectService Pservice = new ProjectService();

        public RapportPhotoController()

        {

            context = new ApplicationDbContext();
            roleStore = new RoleStore<IdentityRole>(context);
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            roleManager = new RoleManager<IdentityRole>(roleStore);
        }

        // GET: Photo
        public ActionResult GetByService (long id)
        {
           // ViewBag.id = id;
            ViewBag.RefID = id;
            List<RapportPhoto> all = photoS.GetAll().ToList();
            List<RapportPhoto> photos = new List<RapportPhoto>();
            foreach(var p in all){
                if (p.ServiceRefId == id)
                    photos.Add(p);
            }

            return View(photos);
        }

        // GET: Photo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Photo/Create
        public ActionResult Create(long id)
        {
            ViewBag.RefID = id;
            return View();
        }

        // POST: Photo/Create
        [HttpPost]
        public ActionResult Create(long RefID, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                 //   byte[] imageSize = new byte[file.ContentLength];
                    byte[] imageSize;

                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        imageSize = reader.ReadBytes(file.ContentLength);
                    }

                    RapportPhoto r = new RapportPhoto()
                    {
                        photo = Convert.ToBase64String(imageSize),
                        ServiceRefId = RefID,
                        lat = 10,
                        lng = 10,
                        descrition = " abdc"
                    };
                    photoS.Create(r);
                    photoS.Save();
                    ViewBag.success = " Imae Uploaded Succesfully ";
                    return RedirectToAction("GetByService", "RapportPhoto", new {id=RefID });
                }
                throw new DivideByZeroException();

                
            }
            catch
            {
                ViewBag.error = " an error occured ";
                return RedirectToAction("create");
                //return View();
            }
        }

        // GET: Photo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Photo/Edit/5
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

        // GET: Photo/Delete/5
        public ActionResult Delete(int id)
        {
            long parent = photoS.Get(id).ServiceRefId;
            photoS.Delete(id);
            photoS.Save();
            return RedirectToAction("GetByService",new { id = parent });
        }

       
    }
}
