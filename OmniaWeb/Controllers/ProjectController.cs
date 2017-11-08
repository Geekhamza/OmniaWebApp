using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyData.Infrastructure;
using MyDomain.Entities;
using MyService.Services;
using OmniaWeb.Extra;
using OmniaWeb.ExtraTools;
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
    [Authorize]
    public class ProjectController : Controller
    {
        // initiate role manager & users manager 
        public ApplicationDbContext context;
        public RoleStore<IdentityRole> roleStore;
        public RoleManager<IdentityRole> roleManager;
        public UserStore<ApplicationUser> userStore;
        public UserManager<ApplicationUser> userManager { get; private set; }
        public ProjectService Pservice = new ProjectService();
        public ClientService cService = new ClientService();
        public EquipementService eService = new EquipementService();
        public ServiceService serviceS = new ServiceService();
        public TacheService taskS = new TacheService();
        public ProgressService progressS = new ProgressService();
        public List<ApplicationUser> projectmanagers = new List<ApplicationUser>(); 
        public List<string> Countries=new Country().countires;
        

         public ProjectController()
            
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
                if (userManager.IsInRole(u.Id, "ProjectManger") || userManager.IsInRole(u.Id, "admin"))
                {
                    projectmanagers.Add(u);
                }
            }


           
        }



         #region Project methods


         //
        // GET: /Project/
        public ActionResult Index()
        {
            List<ProjecDetailstModel> projects = new List<ProjecDetailstModel>();
            if (userManager.IsInRole(User.Identity.GetUserId().ToString(), "admin") || userManager.IsInRole(User.Identity.GetUserId().ToString(), "DG"))
                {
                    var tmp = Pservice.GetAll().ToList();
                    foreach (var p in tmp)
                    {
                        var projectmodel = new ProjecDetailstModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Country = p.Country,
                            datDebut = p.datDebut,
                            datFin = p.datFin,
                            datFinEstime = p.datFinEstime,
                            Zone=p.Zone,
                            OwnerID=p.OwnerId,

                            
                            

                        };

                    
                        try{
                            projectmodel.Owner = userManager.FindById(p.OwnerId).UserName;
                        }
                        catch
                        {
                            projectmodel.Owner = "Not Found";
                        }
                        
                        
                         try{
                            projectmodel.client = cService.Get(p.ClientRefId).name;
                            }
                            catch{
                                projectmodel.client="Not Defined";
                            }


                        projects.Add(projectmodel);
                    }
                }
            else
            {
                var tmp = Pservice.GetMyProject(User.Identity.GetUserId()).ToList();
                foreach (var p in tmp)
                {
                    var projectmodel = new ProjecDetailstModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Country = p.Country,
                        Zone=p.Zone,
                        datDebut = p.datDebut,
                        datFin = p.datFin,
                        datFinEstime = p.datFinEstime,
                        OwnerID = p.OwnerId,
                        Owner = userManager.FindById(p.OwnerId).UserName
                    };
                    try
                    {
                        projectmodel.client = cService.Get(p.ClientRefId).name;
                    }
                    catch
                    {
                        projectmodel.client = "Not Defined";
                    }
                    projects.Add(projectmodel);
                }
            }

            return View(projects);
        }

        //
        // GET: /Project/Details/5
        public ActionResult Details(long id)
        {
            var p=Pservice.Get(id);
            ProjecDetailstModel projectmodel = new ProjecDetailstModel()
            {
                Id = p.Id,
                Name = p.Name,
                Country = p.Country,
                datDebut = p.datDebut,
                Zone = p.Zone,
                datFin = p.datFin,
                datFinEstime = p.datFinEstime,
                OwnerID = p.OwnerId,
                Owner = userManager.FindById(p.OwnerId).UserName,
                nb_equipment = 0,
                nb_services=0
            };
            try
            {
                projectmodel.client = cService.Get(p.ClientRefId).name;
                var all = eService.GetAll().ToList();
                foreach (var e in all)
                {
                    if(e is Manhole&& ((Manhole)e).ProjectRefId==id) {
                    Manhole mh=(Manhole)e;
                    ManholeProjectModel model = new ManholeProjectModel()
                    {
                        Id = mh.Id,
                        lat = mh.latManhole,
                        lng = mh.lngManhole,
                        reference=mh.reference ,
                        progress = getManholeprogress(mh.Id)
                    };
                    projectmodel.manholesdetails.Add(model);
                    projectmodel.nb_equipment++;
                    projectmodel.nb_services+= (((Manhole)e).services.Count());
                    projectmodel.nb_equipment += (((Manhole)e).joints.Count());
                        foreach(var j in ((Manhole)e).joints)
                        {
                            projectmodel.nb_services += j.services.Count();
                        }

                    }

                    if (e is PEHD && ((PEHD)e).ProjectRefId == id) 
                {
                    PEHD pehd =(PEHD)e;
                    PEHDProjectModel model = new PEHDProjectModel()
                    {
                        Id = pehd.Id,
                        latSrc = ((Manhole)eService.GetEquipementById(pehd.IdManholeSrc)).latManhole,
                        lngSrc = ((Manhole)eService.GetEquipementById(pehd.IdManholeSrc)).lngManhole,
                        latDist = ((Manhole)eService.GetEquipementById(pehd.IdManholeDist)).latManhole,
                        lngDist= ((Manhole)eService.GetEquipementById(pehd.IdManholeDist)).lngManhole,
                        progress = getPEHDprogress(pehd.Id)
                    };
                    projectmodel.PEHDsdetails.Add(model);
                    projectmodel.nb_equipment++;
                    projectmodel.nb_equipment += (((PEHD)e).Tubes.Count());
                    projectmodel.nb_equipment += (((PEHD)e).Cable.Count());
                    projectmodel.nb_services += (((PEHD)e).services.Count());
                        foreach (var t in ((PEHD)e).Tubes)
                        {
                            projectmodel.nb_services += t.services.Count();
                        }
                        foreach (var c in ((PEHD)e).Cable)
                        {
                            projectmodel.nb_services += c.services.Count();
                        }
                    }
               }

            }
            catch
            {
                projectmodel.client = "Not Defined";
            }

            ViewBag.id = id;
            return View(projectmodel);
        }

        private double getPEHDprogress(long id)
        {
            var list = eService.GetAll().ToList();
            var services = serviceS.GetAll().ToList();
            var taches = taskS.GetAll().ToList();
            var progress = progressS.GetAll().ToList();
            var all = taskS.GetAll().ToList();
            long nb_task = 0;
            double totalProg = 0.0;
            foreach (var t in all)
            {
                var equip = eService.GetEquipementById((serviceS.Get(t.ServiceREfId)).EquipementREfId);
                // if its a pehd
                if (equip is PEHD && (((PEHD)equip).Id == id))
                {
                    nb_task++;
                    if(t.progresses.Count()>0)
                    totalProg += t.progresses.Last().progress;

                }
                // if its sub equipement of PEHD
                if (equip is Tube && (((Tube)equip).PEHDRefId == id))
                {
                    nb_task++;
                    if (t.progresses.Count() > 0)
                    totalProg += t.progresses.Last().progress;

                }
                if (equip is Cable && (((Cable)equip).PEHDRefId == id))
                {
                    nb_task++;
                    if (t.progresses.Count() > 0)
                        totalProg += t.progresses.Last().progress;
                }

            }
            if (nb_task == 0)
                return 100.0;
            return (double)(totalProg/nb_task);
        }

        private double getManholeprogress(long id)
        {
            var list = eService.GetAll().ToList();
            var services = serviceS.GetAll().ToList();
            var taches = taskS.GetAll().ToList();
            var progress = progressS.GetAll().ToList();
             var all = taskS.GetAll().ToList();
            long nb_task = 0;
            double totalProg = 0.0;
            foreach (var t in all)
            {
                var equip = eService.GetEquipementById((serviceS.Get(t.ServiceREfId)).EquipementREfId);
                // if its a pehd
                if (equip is Manhole && (((Manhole)equip).Id == id))
                {
                    nb_task++;
                    if (t.progresses.Count() > 0)
                        totalProg += t.progresses.Last().progress;

                }
                // if its sub equipement of PEHD
                if (equip is Joint && (((Joint)equip).ManholeId == id))
                {
                    nb_task++;
                    if (t.progresses.Count() > 0)
                        totalProg += t.progresses.Last().progress;

                }
            }
            if (nb_task == 0)
                return 100.0;
            return (double)(totalProg/nb_task);
          
        }

        //
        // GET: /Project/Create
        public ActionResult Create()
        {
            ViewBag.Users = projectmanagers;
            ViewBag.countries = Countries;
            ViewBag.clients = cService.GetAll().ToList();
            return View();
        }

        //
        // POST: /Project/Create
        [HttpPost]
        public ActionResult Create(AddProjectViewModel p)
        {
            ViewBag.error = "Erreur Adding Project(Please Verify the Project Dates , Client ... and resubmit )";
            
            try
            {
                // TODO: Add insert logic here
                Project prj = new Project()
                {
                    Name = p.Name,
                    Zone = p.Zone,
                    Country = p.Country,
                    datDebut = p.datDebut,
                    datFin = p.datFin,
                    datFinEstime = p.datFinEstime,
                    OwnerId=p.OwnerID
                    
                };
              
                if (prj.datDebut > prj.datFinEstime)
                    throw (new DivideByZeroException());
                Client c = cService.GetByName(p.client);
               // Pservice.Create(prj);
                c.projects.Add(prj);
                cService.Save();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                ViewBag.Users = projectmanagers;
                ViewBag.countries = Countries;
                ViewBag.clients = cService.GetAll().ToList();
                return View(p);
            }
        }

        //
        // GET: /Project/Edit/5
        public ActionResult Edit(long id)
        {
            
            EditProjectViewModel p = new EditProjectViewModel();
            try
            {
                Project prj = Pservice.Get(id);
                p.client = cService.Get(prj.ClientRefId).name;
                p.Id = prj.Id;
                p.Country = prj.Country;
                p.datDebut = prj.datDebut;
                p.datFin = prj.datFin;
                p.datFinEstime = prj.datFinEstime;
                p.Name = prj.Name;
                p.Zone = prj.Zone;
                p.OwnerID = prj.OwnerId;
                p.Owner=userManager.FindById(prj.OwnerId).UserName;
            }
            catch(Exception e)
            {
                // if an error occured disable editing
                ViewBag.disable = 1;
                ViewBag.error = " an error occured while getting project , Try refreching the page ?";
                
            }
            
            ViewBag.Users = projectmanagers;
            ViewBag.countries = Countries;
            ViewBag.clients = cService.GetAll().ToList();
            return View(p);
        }

        //
        // POST: /Project/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, EditProjectViewModel p)
        {

            ViewBag.error = "Erreur Editing Project(Please Verify the Project Dates , Client ... and resubmit )";
            try
            {
                // TODO: Add update logic here
                // TODO: Add insert logic here
                Project prj = Pservice.Get(id);
                    prj.Name = p.Name;
                    prj.Zone = p.Zone;
                    prj.Country = p.Country;
                    prj.datDebut = p.datDebut;
                    prj.datFin = p.datFin;
                    prj.datFinEstime = p.datFinEstime;
                    prj.OwnerId = p.OwnerID;
                Client c = cService.GetByName(p.client);
                prj.ClientRefId = c.id;
                Pservice.Update(prj);
                Pservice.Save();
                cService.Save();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Users = projectmanagers;
                //Console.WriteLine(e.InnerException);
                ViewBag.countries = Countries;
                ViewBag.clients = cService.GetAll().ToList();
                return View(p);
            }
        }

       
        public ActionResult Delete(long id)
        {

            try
            {
                // TODO: Add delete logic here
                Pservice.Delete(id);
                Pservice.Save();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return RedirectToAction("Index");
            }
        }
    #endregion

    
    
    }

}
