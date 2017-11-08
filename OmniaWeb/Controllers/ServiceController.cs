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
    public class ServiceController : Controller
    {
         // initiate role manager & users manager 
        
        ProjectService projectS = new ProjectService();
        EquipementService equipementS = new EquipementService();
        ServiceService serviceS = new ServiceService();
        TacheService taskS = new TacheService();
        public ProjectService Pservice=new ProjectService();

        public ServiceController()
            
        {
       
         }

        //
        // GET: /GetByEquipement/1
        public ActionResult GetByEquipement(long id)
        {
            
            var all = serviceS.GetAll();
            List<ServiceDetailsModel> services = new List<ServiceDetailsModel>();
            foreach (var s in all)
            {
                if (s.EquipementREfId == id)
                {
                    ServiceDetailsModel model = new ServiceDetailsModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        price = s.price,
                        priceHT = s.priceHT,
                    };
                    int closed = 0;
                    int open = 0;
                    int nbTaches = 0;
                    var allTaches = taskS.GetAll().ToList();
                    foreach(var t in allTaches)
                    {
                        
                        if (t.ServiceREfId==s.Id && t.progresses.Count()>0 &&t.progresses.Last().progress>= 100)
                        {
                            closed++;
                        }
                        if (t.ServiceREfId == s.Id && t.progresses.Count() > 0 && t.progresses.Last().progress==0)
                        {
                            open++;
                        }
                        if (t.ServiceREfId == s.Id)
                            nbTaches++;
                    }
                    model.nbTask = nbTaches;
                    model.finished=1;
                    
                    if ((closed == nbTaches))
                        model.finished = 2;
                    if ((open == nbTaches))
                        model.finished = 0;

                    services.Add(model);
                }
            }
            ViewBag.id = id;
            var equip=(equipementS.GetEquipementById(id));
            if(equip is Manhole){
                ViewBag.RefID = ((Manhole)equip).ProjectRefId;
            }
            if (equip is PEHD)
            {
                ViewBag.RefID = ((PEHD)equip).ProjectRefId;
            }
            if (equip is Tube)
            {
                ViewBag.RefID = ((Tube)equip).pehd.ProjectRefId;
            }
            if (equip is Cable)
            {
                ViewBag.RefID = ((Cable)equip).pehd.ProjectRefId;
            }
            if (equip is Joint)
            {
                ViewBag.RefID = ((Joint)equip).manhole.ProjectRefId;
            }
            return View(services);
        }




        //
        // GET: /Service/Details/5
        public ActionResult Details(int id)
        {
         
            return View();
        }

        //
        // GET: /Service/Create
        public ActionResult Create(long id)
        {
            ViewBag.RefID = id;
            return View();
        }

        //
        // POST: /Service/Create
        [HttpPost]
        public ActionResult Create(long RefID, AddServiceModel model)
        {
            try
            {
                // TODO: Add insert logic here
                Service s = new Service()
                {
                    Name = model.Name,
                    price = model.price,
                    priceHT = model.priceHT,
                    EquipementREfId= RefID
                };
                serviceS.Create(s);
                serviceS.Save();
                return RedirectToAction("GetByEquipement",new { id=RefID});
            }
            catch
            {
                ViewBag.error = "An error has occured";
                ViewBag.RefID = RefID;
                return RedirectToAction("GetByEquipement", new { id = RefID });
            }
        }

        //
        // GET: /Service/Edit/5
        public ActionResult Edit(long id,long RefID)
        {
            ViewBag.RefID = RefID;
            Service s = serviceS.Get(id);
            AddServiceModel model = new AddServiceModel
            {
                Name = s.Name,
                price=s.price,
                priceHT=s.priceHT
            };
            return View(model);
        }

        //
        // POST: /Service/Edit/5
        [HttpPost]
        public ActionResult Edit(long  id,long RefId, AddServiceModel model)
        {
            try
            {
                // TODO: Add update logic here
                Service s = serviceS.Get(id);
                s.Name = model.Name;
                s.priceHT = model.priceHT;
                s.price = model.price;
                return RedirectToAction("GetByEquipement",new { id=RefId});
            }
            catch
            {
                ViewBag.error = "An error has occured ";
                ViewBag.RefID = RefId;
                return View(model);
            }
        }

        //
        // GET: /Service/Delete/5
        public ActionResult Delete(long id)
        {
            var  EquipmentId=serviceS.Get(id).EquipementREfId;
            try
            {
                serviceS.Delete(id);
                serviceS.Save();
                return RedirectToAction("GetByEquipement", "Service", new { id = EquipmentId, type = equipementS.GetEquipementById(EquipmentId).GetType().ToString() });
            }
            catch
            {
                ViewBag.error = " An Error Has Occured";
                return RedirectToAction("GetByEquipement", "Service",new {id=EquipmentId,type=equipementS.GetEquipementById(EquipmentId).GetType().ToString()});
            }
            
        }

       
    }
}
