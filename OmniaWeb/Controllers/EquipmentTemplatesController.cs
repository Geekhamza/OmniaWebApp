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
    [Authorize(Roles="admin,DG")]
    public class EquipmentTemplatesController : Controller
    {
        // initiate role manager & users manager 
        TemplateEquipementService templatesEquipmentS = new TemplateEquipementService();
        TemplateServiceService templateServiceS = new TemplateServiceService();
        public ProjectService Pservice=new ProjectService();
        TemplateTacheService templatesTaskS = new TemplateTacheService();
        public EquipmentTemplatesController()   
        {  
         }
        //
        // GET: /EquipmentTemplates/Manhole
        public ActionResult Manhole()
        {
            
            var ManholeType = TypeTemplate.Manhole;
            var templates = templatesEquipmentS.GetByType(ManholeType);
            List<EquipementTemplateModels> list=new List<EquipementTemplateModels>();
                foreach (var tmp in templates){
                    EquipementTemplateModels model = new EquipementTemplateModels(){
                        Id=tmp.Id,
                        Name=tmp.Name
                        
                    };
                    var allservice = templateServiceS.GetAll().ToList();
                    int cmp=0;
                    foreach (var s in allservice)
                    {
                        if (s.EquipementTemplaREfId == tmp.Id)
                        {
                            cmp++;
                        }
                    }
                    model.nb_services = cmp;
                    list.Add(model);
                }
            return View(list);
        }
        //
        // GET: /EquipmentTemplates/Tube
        public ActionResult Tube()
        {
            var type = TypeTemplate.Tube;
            var templates = templatesEquipmentS.GetByType(type);
            List<EquipementTemplateModels> list = new List<EquipementTemplateModels>();
            foreach (var tmp in templates)
            {
                EquipementTemplateModels model = new EquipementTemplateModels()
                {
                    Id = tmp.Id,
                    Name = tmp.Name
                };
                var allservice = templateServiceS.GetAll().ToList();
                int cmp = 0;
                foreach (var s in allservice)
                {
                    if (s.EquipementTemplaREfId == tmp.Id)
                    {
                        cmp++;
                    }
                }
                model.nb_services = cmp;
                list.Add(model);
            }
            return View(list);
        }
        //
        // GET: /EquipmentTemplates/Cable
        public ActionResult Cable()
        {
            var type = TypeTemplate.Cable;
            var templates = templatesEquipmentS.GetByType(type);
            List<EquipementTemplateModels> list = new List<EquipementTemplateModels>();
            foreach (var tmp in templates)
            {
                EquipementTemplateModels model = new EquipementTemplateModels()
                {
                    Id = tmp.Id,
                    Name = tmp.Name

                };
                var allservice = templateServiceS.GetAll().ToList();
                int cmp = 0;
                foreach (var s in allservice)
                {
                    if (s.EquipementTemplaREfId == tmp.Id)
                    {
                        cmp++;
                    }
                }
                model.nb_services = cmp;
                list.Add(model);
            }
            return View(list);
        }
        //
        // GET: /EquipmentTemplates/Joint
        public ActionResult Joint()
        {
            var type = TypeTemplate.Joint;
            var templates = templatesEquipmentS.GetByType(type);
            List<EquipementTemplateModels> list = new List<EquipementTemplateModels>();
            foreach (var tmp in templates)
            {
                EquipementTemplateModels model = new EquipementTemplateModels()
                {
                    Id = tmp.Id,
                    Name = tmp.Name

                };
                var allservice = templateServiceS.GetAll().ToList();
                int cmp = 0;
                foreach (var s in allservice)
                {
                    if (s.EquipementTemplaREfId == tmp.Id)
                    {
                        cmp++;
                    }
                }
                model.nb_services = cmp;
                list.Add(model);
            }
            return View(list);
        }
        //
        // GET: /EquipmentTemplates/PEHD
        public ActionResult PEHD()
        {
            var type = TypeTemplate.PEHD;
            var templates = templatesEquipmentS.GetByType(type);
            List<EquipementTemplateModels> list = new List<EquipementTemplateModels>();
            foreach (var tmp in templates)
            {
                EquipementTemplateModels model = new EquipementTemplateModels()
                {
                    Id = tmp.Id,
                    Name = tmp.Name

                };
                var allservice = templateServiceS.GetAll().ToList();
                int cmp = 0;
                foreach (var s in allservice)
                {
                    if (s.EquipementTemplaREfId == tmp.Id)
                    {
                        cmp++;
                    }
                }
                model.nb_services = cmp;
                list.Add(model);
            }
            return View(list);
        }



        //
        // GET: /EquipmentTemplates/Details/5
        public ActionResult Details(long id)
        {
            var allServices = templateServiceS.GetAll();
            List<ServiceTemplateModel> services = new List<ServiceTemplateModel>();
            foreach (var s in allServices)
            {
                if (s.EquipementTemplaREfId == id)
                {

                    ServiceTemplateModel tmp = new ServiceTemplateModel()
                    {
                        Id = s.Id,
                        Name = s.name,
                        price = s.price,
                        priceHT = s.priceHT,
                        EquipementRefId = id
                    };
                    var allTask = templatesTaskS.GetAll().ToList();
                    int cmp = 0;
                    foreach (var t in allTask)
                    {
                        if (t.TemplateServiceREfId == s.Id)
                        {
                            cmp++;

                        }
                    }
                    tmp.nb_Tasks = cmp;
                    services.Add(tmp);

                }
            }
            
            ViewBag.id = id;
            return View(services);
        }

        //
        // GET: /EquipmentTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /EquipmentTemplates/Create
        [HttpPost]
        public ActionResult Create(AddEquipementTemplateModels model)
        {
            try
            {
                // TODO: Add insert logic here
                TemplateEquipement template = new TemplateEquipement()
                {
                    Name = model.Name
                    
                };
                 switch (model.Type)
                {
                     case("0"):{template.Type=TypeTemplate.Manhole;};break;
                     case("1"):{template.Type=TypeTemplate.Tube;};break;
                    case("2"):{template.Type=TypeTemplate.Cable;};break;
                    case("3"):{template.Type=TypeTemplate.Joint;};break;
                    case("4"):{template.Type=TypeTemplate.PEHD;};break;
                    default: { template.Type = TypeTemplate.Manhole; }; break;    
                };

                templatesEquipmentS.Create(template);
                templatesEquipmentS.Save();

                if (template.Type == TypeTemplate.Manhole)
                {
                    return RedirectToAction("Manhole");
                }
                if (template.Type == TypeTemplate.Cable)
                {
                    return RedirectToAction("Cable");
                }
                if (template.Type == TypeTemplate.Joint)
                {
                    return RedirectToAction("Joint");
                }
                if (template.Type == TypeTemplate.PEHD)
                {
                    return RedirectToAction("PEHD");
                }
                if (template.Type == TypeTemplate.Tube)
                {
                    return RedirectToAction("Tube");
                }
                else
                {
                    return RedirectToAction("Manhole");
                }
            }
            catch
            {
                ViewBag.error = "An Error HAs Occured While inserting New Template";
                return View(model);
            }
        }

        //
        // GET: /EquipmentTemplates/Edit/5
        public ActionResult Edit(long id)
        {
            try{
                ViewBag.id = id;
                var template = templatesEquipmentS.Get(id);
                AddEquipementTemplateModels model = new AddEquipementTemplateModels()
                {
                    Name = template.Name
                };
                if (template.Type == TypeTemplate.Manhole)
                {
                    model.Type = "Manhole";
                }
                if (template.Type == TypeTemplate.Cable)
                {
                    model.Type = "Cable";
                }
                if (template.Type == TypeTemplate.Joint)
                {
                    model.Type = "Joint";
                }
                if (template.Type == TypeTemplate.PEHD)
                {
                    model.Type = "PEHD";
                }
                if (template.Type == TypeTemplate.Tube)
                {
                    model.Type = "Tube";
                }
                return View(model);
            }
            catch{
                ViewBag.error="An Error Just Occured getting the Template Details ";
                return View();

            }
        }

        //
        // POST: /EquipmentTemplates/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AddEquipementTemplateModels model)
        {
            try
            {
                // TODO: Add update logic here
                var template = templatesEquipmentS.Get(id);
                template.Name = model.Name;
                switch (model.Type)
                {
                    case ("0"): { template.Type = TypeTemplate.Manhole; }; break;
                    case ("1"): { template.Type = TypeTemplate.Tube; }; break;
                    case ("2"): { template.Type = TypeTemplate.Cable; }; break;
                    case ("3"): { template.Type = TypeTemplate.Joint; }; break;
                    case ("4"): { template.Type = TypeTemplate.PEHD; }; break;
                    default: { template.Type = TypeTemplate.Manhole; }; break;
                };
                templatesEquipmentS.Update(template);
                templatesEquipmentS.Save();

                if (template.Type == TypeTemplate.Manhole)
                {
                    return RedirectToAction("Manhole");
                }
                if (template.Type == TypeTemplate.Cable)
                {
                    return RedirectToAction("Cable");
                }
                if (template.Type == TypeTemplate.Joint)
                {
                    return RedirectToAction("Joint");
                }
                if (template.Type == TypeTemplate.PEHD)
                {
                    return RedirectToAction("PEHD");
                }
                if (template.Type == TypeTemplate.Tube)
                {
                    return RedirectToAction("Tube");
                }
                else
                {
                    return RedirectToAction("Manhole");
                }


                
            }
            catch
            {
                ViewBag.error = " An error Has occured while updating ..";
                return View(model);
            }
        }

        //
        

        //
        // GET /EquipmentTemplates/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                templatesEquipmentS.Delete(id);
                templatesEquipmentS.Save();
                return RedirectToAction("Manhole");
            }
            catch
            {
                return RedirectToAction("Manhole");
            }
        }
    }
}
