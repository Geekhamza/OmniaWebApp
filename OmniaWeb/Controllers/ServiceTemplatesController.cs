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
    public class ServiceTemplatesController : Controller
    {
            // initiate role manager & users manager 
        TemplateServiceService templatesServiceS = new TemplateServiceService();
        TemplateEquipementService templateEquipementServiceS = new TemplateEquipementService();
        TemplateTacheService templatesTaskS = new TemplateTacheService();

        public ServiceTemplatesController()
            
        {
        }


        //
        // GET: /ServiceTemplates/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ServiceTemplates/Details/5
        public ActionResult Details(long  id)
        {

            List<TaskTemplateModel> list = new List<TaskTemplateModel>();
            var service=templatesServiceS.Get(id);
            var allTask = templatesTaskS.GetAll().ToList();
            foreach (var tmp in allTask)
            {
                if(tmp.TemplateServiceREfId==id)
                {
                    var model = new TaskTemplateModel()
                    {
                        dureeEstime=tmp.dureEstime,
                        Id=tmp.Id,
                         Name=tmp.Nom

                    };
                    list.Add(model);
                }

            }
            ViewBag.id = id;
            return View(list);
        }

        //
        // GET: /ServiceTemplates/Create
        public ActionResult Create(long TemplateRefId)
        {
            // keep the current ref id 
            ViewBag.id = TemplateRefId;
            return View();
        }

        //
        // POST: /ServiceTemplates/Create
        [HttpPost]
        public ActionResult Create(AddServiceTemplateModel model, long RefID)
        {
            try
            {
                // TODO: Add insert logic here
                TemplateService template = new TemplateService()
                {
                    priceHT=model.priceHT,
                    EquipementTemplaREfId=RefID,
                    name=model.Name,
                    price=model.price
                };
                //templatesServiceS.Create(template);
                templateEquipementServiceS.Get(RefID).services.Add(template);
                templateEquipementServiceS.Save();
               // ViewBag.success = "Template Added succesfully , Add More?";
                //ViewBag.id = RefID;
                return RedirectToAction( "Details","EquipmentTemplates" ,new { id = RefID});

                
            }
            catch
            {
                ViewBag.id = RefID;
                ViewBag.error = "An Error Has occured";
                return View(model);
            }
        }

        //
        // GET: /ServiceTemplates/Edit/5
        public ActionResult Edit(long id)
        {
            TemplateService template = templatesServiceS.Get(id);
            var model = new ServiceTemplateModel()
            {
                EquipementRefId = template.EquipementTemplaREfId,
                Id = template.Id,
                Name = template.name,
                nb_Tasks = template.Taches.Count(),
                price = template.price,
                priceHT = template.priceHT
            };
            return View(model);
        }

        //
        // POST: /ServiceTemplates/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, long RefID,ServiceTemplateModel model)
        {
            try
            {
                
                // TODO: Add update logic here
                TemplateService template = templatesServiceS.Get(id);
                template.name = model.Name;
                template.price = model.price;
                template.priceHT = model.priceHT;
                templatesServiceS.Update(template);
                templatesServiceS.Save();
                return RedirectToAction("Details", "EquipmentTemplates", new { id = RefID });
            }
            catch
            {
                ViewBag.error = " an error has occured";
                return View(model);
            }
        }

        //
        // GET: /ServiceTemplates/Delete/5
        public ActionResult Delete(int id)
        {
            var child = templatesServiceS.Get(id);
            long RefID = child.EquipementTemplaREfId;
            templatesServiceS.Delete(id);
            templatesServiceS.Save();
            return RedirectToAction("Details", "EquipmentTemplates", new { id = RefID });


        }

     
     
    }
}
