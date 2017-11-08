using MyDomain.Entities;
using MyService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmniaWeb.Controllers
{
    public class ProgressController : Controller
    {
        ProgressService progressS = new ProgressService();
        TacheService taskS = new TacheService();
        //
        // GET: /Progress/log/1
        public ActionResult log(long id)
        {
            var all = progressS.GetAll().ToList();
            List<Progress> res = new List<Progress>();
            foreach (var p in all)
            {
                if (p.TacheRefId == id)
                    res.Add(p);
            }
            ViewBag.RefId = taskS.Get(id).ServiceREfId;
            return View(res);
        }

        //
        // GET: /Progress/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Progress/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Progress/Create
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

        //
        // GET: /Progress/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Progress/Edit/5
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

        //
        // GET: /Progress/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Progress/Delete/5
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
