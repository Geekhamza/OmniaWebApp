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
    public class EquipmentController : Controller
    {
        // initiate role manager & users manager 
        TemplateEquipementService templatesEquipmentS = new TemplateEquipementService();
        TemplateServiceService templateServiceS = new TemplateServiceService();
        TemplateTacheService templateTacheS = new TemplateTacheService();
        EquipementService equipementS = new EquipementService();
        ProjectService projectS = new ProjectService();
        ServiceService serviceS = new ServiceService();
        public ProjectService Pservice = new ProjectService();

        public EquipmentController()

        {


        }

        //
        // GET: /Equipment/
        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /ProjectEquipements/1
        public ActionResult ProjectEquipements(long id)
        {
            var allEquipment = equipementS.GetAll().ToList();
            List<Equipement> equipments = new List<Equipement>();
            foreach (var e in allEquipment)
            {
                if (e is Manhole && ((Manhole)e).ProjectRefId == id)
                {
                    equipments.Add(e);
                    foreach (var j in ((Manhole)e).joints)
                    {
                        equipments.Add(j);
                    }
                }

                if (e is PEHD && ((PEHD)e).ProjectRefId == id)
                {

                    equipments.Add(e);
                    foreach (var t in ((PEHD)e).Tubes)
                    {
                        equipments.Add(t);
                    }
                    foreach (var c in ((PEHD)e).Cable)
                    {
                        equipments.Add(c);
                    }

                }
            }
            ViewBag.id = id;
            return View(equipments);
        }

        //
        // GET: /Equipment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Equipment/Create
        [Authorize(Roles = "admin,DG")]
        public ActionResult Create(int type, long id)
        {
            ViewBag.id = id;
            if (type == 0) {
                List<TemplateEquipement> templates = templatesEquipmentS.GetByType(MyDomain.Entities.TypeTemplate.Manhole).ToList();
                ViewBag.list = templates;
                return View("CreateManhole");
            }
            if (type == 1) {
                List<TemplateEquipement> templates = templatesEquipmentS.GetByType(MyDomain.Entities.TypeTemplate.PEHD).ToList();
                ViewBag.list = templates;
                List<Manhole> list = equipementS.GetAllManhole().ToList();
                List<Manhole> manholes = new List<Manhole>();
                foreach (var m in list)
                {
                    if (m.ProjectRefId == id)
                        manholes.Add(m);
                }
                ViewBag.manholes = manholes;
                return View("CreatePEHD");
            }
            if (type == 2) {
                List<TemplateEquipement> templates = templatesEquipmentS.GetByType(MyDomain.Entities.TypeTemplate.Joint).ToList();
                ViewBag.list = templates;
                List<Manhole> list = equipementS.GetAllManhole().ToList();
                List<Manhole> manholes = new List<Manhole>();
                foreach (var m in list)
                {
                    if (m.ProjectRefId == id)
                        manholes.Add(m);
                }
                ViewBag.manholes = manholes;

                return View("CreateJoint");
            }
            if (type == 3) {
                List<TemplateEquipement> templates = templatesEquipmentS.GetByType(MyDomain.Entities.TypeTemplate.Cable).ToList();
                ViewBag.list = templates;
                List<PEHD> list = equipementS.GetAllPEHD().ToList();
                List<PEHD> Pehd = new List<PEHD>();
                foreach (var p in list)
                {
                    if (p.ProjectRefId == id)
                        Pehd.Add(p);
                }
                ViewBag.pehds = Pehd;
                return View("CreateCable");
            }
            else {
                List<TemplateEquipement> templates = templatesEquipmentS.GetByType(MyDomain.Entities.TypeTemplate.Tube).ToList();
                ViewBag.list = templates;
                List<PEHD> list = equipementS.GetAllPEHD().ToList();
                List<PEHD> Pehd = new List<PEHD>();
                foreach (var p in list)
                {
                    if (p.ProjectRefId == id)
                        Pehd.Add(p);
                }
                ViewBag.pehds = Pehd;
                return View("CreateTube");
            }


        }

        //
        // POST: /Equipment/CreateManhole
        [HttpPost]
        public ActionResult CreateManhole(AddManholeModel model, long RefID)
        {
            try
            {
                // TODO: Add insert logic here
                Manhole manhole = new Manhole()
                {
                    latManhole = model.latManhole,
                    lngManhole = model.lngManhole,
                    TypeCh = model.TypeCh,
                    reference = model.reference,
                    ProjectRefId = RefID

                };


                //if a template was used 
                if (model.EquipementTemplateModel != "0") {
                    // get it 
                    TemplateEquipement template = templatesEquipmentS.Get(Int64.Parse(model.EquipementTemplateModel));
                    // and for each service declared , add it to the current manhole
                    foreach (TemplateService tmpS in template.services) {
                        Service s = new Service()
                        {
                            Name = tmpS.name,
                            price = tmpS.price,
                            priceHT = tmpS.priceHT,
                            //EquipementREfId = Int64.Parse(model.EquipementTemplateModel)
                        };
                        //serviceS.Create(s);
                        // get evey task inside service template 
                        var all = templateTacheS.GetAll().ToList();
                        foreach (var tmpT in all)
                        {
                            if (tmpT.TemplateServiceREfId == tmpS.Id)
                            {
                                Tache t = new Tache()
                                {
                                    Description = tmpT.Description,
                                    dureEstime = tmpT.dureEstime,
                                    Nom = tmpT.Nom
                                };
                                s.Taches.Add(t);
                            }
                        }
                        // serviceS.Save();
                        manhole.services.Add(s);
                    };
                }
                projectS.Get(RefID).manholes.Add(manhole);
                projectS.Save();
                ViewBag.success = " Equipement Added Succesfully";
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.InnerException);
                ViewBag.error = "An Error Has occured ";
                ViewBag.id = RefID;
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
        }



        //
        // POST: /Equipment/CreatePEHD/1?RefID=1
        [HttpPost]
        public ActionResult CreatePEHD(AddPEHDModel model, long RefID)
        {
            try
            {
                // TODO: Add insert logic here
                PEHD pehd = new PEHD()
                {
                    reference = model.reference,
                    DiametrePEHD = model.DiametrePEHD,
                    IdManholeSrc = Int64.Parse(model.ManholeSrc),
                    IdManholeDist = Int64.Parse(model.ManholeDist),
                    PressionPEHD = model.PressionPEHD,
                    longeurPEHD = model.longeurPEHD,
                    ProjectRefId = RefID,
                    Tubes = new List<Tube>(),
                    Cable = new List<Cable>()

                };


                //if a template was used 
                if (model.EquipementTemplateModel != "0")
                {
                    // get it 
                    TemplateEquipement template = templatesEquipmentS.Get(Int64.Parse(model.EquipementTemplateModel));
                    // and for each service declared , add it to the current manhole
                    foreach (TemplateService tmpS in template.services)
                    {
                        Service s = new Service()
                        {
                            Name = tmpS.name,
                            price = tmpS.price,
                            priceHT = tmpS.priceHT,
                            //EquipementREfId = Int64.Parse(model.EquipementTemplateModel)
                        };
                        //serviceS.Create(s);
                        // get evey task inside service template  
                        foreach (var tmpT in tmpS.Taches)
                        {
                            Tache t = new Tache()
                            {
                                Description = tmpT.Description,
                                dureEstime = tmpT.dureEstime,
                                Nom = tmpT.Nom
                            };
                            s.Taches.Add(t);
                        }
                        // serviceS.Save();
                        pehd.services.Add(s);
                    };
                }
                projectS.Get(RefID).PEHDs.Add(pehd);
                projectS.Save();
                ViewBag.succcess = " Equipement Added Succesfully";
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.InnerException);
                ViewBag.error = "An Error Has occured ";
                ViewBag.id = RefID;
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
        }


        //
        // POST: /Equipment/CreateManhole
        [HttpPost]
        public ActionResult CreateJoint(AddJointModel model, long RefID)
        {
            try
            {
                // TODO: Add insert logic here
                Joint joint = new Joint()
                {
                    reference = model.reference,
                    ManholeId = Int64.Parse(model.Manhole),
                    typeJoint = model.typeJoint
                };

                //if a template was used 
                if (model.EquipementTemplateModel != "0")
                {
                    // get it 
                    TemplateEquipement template = templatesEquipmentS.Get(Int64.Parse(model.EquipementTemplateModel));
                    // and for each service declared , add it to the current manhole
                    foreach (TemplateService tmpS in template.services)
                    {
                        Service s = new Service()
                        {
                            Name = tmpS.name,
                            price = tmpS.price,
                            priceHT = tmpS.priceHT,
                            //EquipementREfId = Int64.Parse(model.EquipementTemplateModel)
                        };
                        //serviceS.Create(s);
                        // get evey task inside service template  
                        foreach (var tmpT in tmpS.Taches)
                        {
                            Tache t = new Tache()
                            {
                                Description = tmpT.Description,
                                dureEstime = tmpT.dureEstime,
                                Nom = tmpT.Nom
                            };
                            s.Taches.Add(t);
                        }
                        // serviceS.Save();
                        joint.services.Add(s);
                    };
                }
                Manhole parent = (Manhole)equipementS.GetEquipementById(joint.ManholeId);
                parent.joints.Add(joint);
                equipementS.Save();
                ViewBag.success = " Equipement Added Succesfully";
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.InnerException);
                ViewBag.error = "An Error Has occured ";
                ViewBag.id = RefID;
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
        }


        //
        // POST: /Equipment/CreateManhole
        [HttpPost]
        public ActionResult CreateCable(AddCableModel model, long RefID)
        {
            try
            {
                // TODO: Add insert logic here
                Cable cable = new Cable()
                {
                    reference = model.reference,
                    PEHDRefId = Int64.Parse(model.PEHDRefId),
                    LongeurCable = model.LongeurCable,
                    nbrFibre = model.nbrFibre,
                    classFibre = model.classFibre,
                    StructureCable = model.StructureCable,
                    Typefibre = model.Typefibre
                };

                //if a template was used 
                if (model.EquipementTemplateModel != "0")
                {
                    // get it 
                    TemplateEquipement template = templatesEquipmentS.Get(Int64.Parse(model.EquipementTemplateModel));
                    // and for each service declared , add it to the current manhole
                    foreach (TemplateService tmpS in template.services)
                    {
                        Service s = new Service()
                        {
                            Name = tmpS.name,
                            price = tmpS.price,
                            priceHT = tmpS.priceHT,
                            //EquipementREfId = Int64.Parse(model.EquipementTemplateModel)
                        };
                        //serviceS.Create(s);
                        // get evey task inside service template  
                        foreach (var tmpT in tmpS.Taches)
                        {
                            Tache t = new Tache()
                            {
                                Description = tmpT.Description,
                                dureEstime = tmpT.dureEstime,
                                Nom = tmpT.Nom
                            };
                            s.Taches.Add(t);
                        }
                        // serviceS.Save();
                        cable.services.Add(s);
                    };
                }
                PEHD parent = (PEHD)equipementS.GetEquipementById(cable.PEHDRefId);
                parent.Cable.Add(cable);
                equipementS.Save();
                ViewBag.success = " Equipement Added Succesfully";
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.InnerException);
                ViewBag.error = "An Error Has occured ";
                ViewBag.id = RefID;
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
        }

        //
        // POST: /Equipment/CreateManhole
        [HttpPost]
        public ActionResult CreateTube(AddTubeModel model, long RefID)
        {
            try
            {
                // TODO: Add insert logic here
                Tube tube = new Tube()
                {
                    reference = model.reference,
                    PEHDRefId = Int64.Parse(model.PEHDRefId),
                    DiametreTube = model.DiametreTube,
                    longeurTube = model.longeurTube,


                };
                tube.typeTube = TypeTube.Glavanise;
                if (model.typeTube == "PVC")
                    tube.typeTube = TypeTube.PVC;

                //if a template was used 
                if (model.EquipementTemplateModel != "0")
                {
                    // get it 
                    TemplateEquipement template = templatesEquipmentS.Get(Int64.Parse(model.EquipementTemplateModel));
                    // and for each service declared , add it to the current manhole
                    foreach (TemplateService tmpS in template.services)
                    {
                        Service s = new Service()
                        {
                            Name = tmpS.name,
                            price = tmpS.price,
                            priceHT = tmpS.priceHT,
                            //EquipementREfId = Int64.Parse(model.EquipementTemplateModel)
                        };
                        //serviceS.Create(s);
                        // get evey task inside service template  
                        foreach (var tmpT in tmpS.Taches)
                        {
                            Tache t = new Tache()
                            {
                                Description = tmpT.Description,
                                dureEstime = tmpT.dureEstime,
                                Nom = tmpT.Nom
                            };
                            s.Taches.Add(t);
                        }
                        // serviceS.Save();
                        tube.services.Add(s);
                    };
                }
                PEHD parent = (PEHD)equipementS.GetEquipementById(tube.PEHDRefId);
                parent.Tubes.Add(tube);
                equipementS.Save();
                ViewBag.success = " Equipement Added Succesfully";
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.InnerException);
                ViewBag.error = "An Error Has occured ";
                ViewBag.id = RefID;
                return RedirectToAction("Details", "Project", new { id = RefID });
            }
        }

        //
        // GET: /Equipment/Edit/5
        public ActionResult Edit(long id, string type, long ProjectID)
        {
            ViewBag.RefID = ProjectID;
            // project  pehd
            List<PEHD> listPe = equipementS.GetAllPEHD().ToList();
            List<PEHD> Pehd = new List<PEHD>();
            foreach (var p in listPe)
            {
                if (p.ProjectRefId == ProjectID)
                    Pehd.Add(p);
            }
            //prooject manhole

            List<Manhole> listMh = equipementS.GetAllManhole().ToList();
            List<Manhole> manholes = new List<Manhole>();

            foreach (var m in listMh)
            {
                if (m.ProjectRefId == ProjectID)
                    manholes.Add(m);
            }

            ViewBag.id = id;
            if (type == "Manhole")
            {
                Manhole m = (Manhole)equipementS.GetEquipementById(id);
                AddManholeModel model = new AddManholeModel()
                {
                    latManhole = m.latManhole,
                    lngManhole = m.lngManhole,
                    reference = m.reference,
                    TypeCh = m.TypeCh
                };
                return View("EditManhole", model);
            }
            if (type == "PEHD")
            {
                ViewBag.manholes = manholes;
                PEHD p = (PEHD)equipementS.GetEquipementById(id);
                AddPEHDModel model = new AddPEHDModel()
                {
                    DiametrePEHD = p.DiametrePEHD,
                    longeurPEHD = p.longeurPEHD,
                    PressionPEHD = p.PressionPEHD,
                    reference = p.reference,
                    ManholeDist = p.IdManholeDist.ToString(),
                    ManholeSrc = p.IdManholeSrc.ToString()
                };
                return View("EditPEHD", model);
            }
            if (type == "Joint")
            {
                ViewBag.manholes = manholes;
                Joint j = (Joint)equipementS.GetEquipementById(id);
                AddJointModel model = new AddJointModel()
                {
                    reference = j.reference,
                    Manhole = j.ManholeId.ToString(),
                    typeJoint = j.typeJoint.ToString()
                };
                return View("EditJoint", model);
            }
            if (type == "Cable")
            {


                ViewBag.pehds = Pehd;
                Cable c = (Cable)equipementS.GetEquipementById(id);
                AddCableModel model = new AddCableModel()
                {
                    classFibre = c.classFibre,
                    LongeurCable = c.LongeurCable,
                    nbrFibre = c.nbrFibre,
                    reference = c.reference,
                    PEHDRefId = c.PEHDRefId.ToString(),
                    StructureCable = c.StructureCable,
                    Typefibre = c.Typefibre

                };

                return View("EditCable", model);
            }
            else
            {
                ViewBag.pehds = Pehd;
                Tube t = (Tube)equipementS.GetEquipementById(id);
                AddTubeModel model = new AddTubeModel()
                {
                    DiametreTube = t.DiametreTube,
                    longeurTube = t.longeurTube,
                    PEHDRefId = t.PEHDRefId.ToString(),
                    reference = t.reference,
                    typeTube = t.typeTube.ToString()
                };
                return View("EditTube", model);
            }

        }


        [HttpPost]
        public ActionResult EditManhole(int id, long RefID, AddManholeModel model)
        {
            try
            {
                Manhole m = (Manhole)equipementS.GetEquipementById(id);
                m.latManhole = model.latManhole;
                m.lngManhole = model.lngManhole;
                m.reference = model.reference;
                m.TypeCh = m.TypeCh;
                equipementS.Update(m);
                equipementS.Save();
                return RedirectToAction("ProjectEquipements", new { id = RefID });

            }
            catch
            {
                ViewBag.error = " An error Has occured";
                return View("EditManhole", model);
            }
        }



        [HttpPost]
        public ActionResult EditPEHD(int id, long RefID, AddPEHDModel model)
        {
            try
            {
                PEHD p = (PEHD)equipementS.GetEquipementById(id);
                p.DiametrePEHD = model.DiametrePEHD;
                p.IdManholeDist = Int64.Parse(model.ManholeDist);
                p.IdManholeSrc = Int64.Parse(model.ManholeSrc);
                p.longeurPEHD = model.longeurPEHD;
                p.PressionPEHD = model.PressionPEHD;
                p.reference = model.reference;
                equipementS.Update(p);
                equipementS.Save();
                return RedirectToAction("ProjectEquipements", new { id = RefID });

            }
            catch
            {
                ViewBag.error = " An error Has occured";
                     //   public ActionResult Edit(long id, string type, long ProjectID)

                return RedirectToAction("Edit", new { id = id, type ="1",ProjectID=RefID });
    
            }
        }




        [HttpPost]
        public ActionResult EditJoint(int id, long RefID, AddJointModel model)
        {
            try
            {
                Joint j = (Joint)equipementS.GetEquipementById(id);
                j.typeJoint = model.typeJoint;
                j.reference = model.reference;
                equipementS.Update(j);
                equipementS.Save();
                return RedirectToAction("ProjectEquipements", new { id = RefID });

            }
            catch
            {
                ViewBag.error = " An error Has occured";
                return View("EditJoint", model);
            }
        }

        [HttpPost]
        public ActionResult EditTube(int id, long RefID, AddTubeModel model)
        {
            try
            {
                Tube t = (Tube)equipementS.GetEquipementById(id);
                t.longeurTube = model.longeurTube;
                t.DiametreTube = model.DiametreTube;
                t.reference = model.reference;
                equipementS.Update(t);
                equipementS.Save();
                return RedirectToAction("ProjectEquipements", new { id = RefID });

            }
            catch
            {
                ViewBag.error = " An error Has occured";
                return View("EditTube", model);
            }
        }

        [HttpPost]
        public ActionResult EditCable(int id, long RefID, AddCableModel model)
        {
            try
            {
                Cable c = (Cable)equipementS.GetEquipementById(id);
                c.classFibre = model.classFibre;
                c.LongeurCable = model.LongeurCable;
                c.nbrFibre = model.nbrFibre;
                c.StructureCable = model.StructureCable;
                c.Typefibre = model.Typefibre;
                c.reference = model.reference;
                equipementS.Update(c);
                equipementS.Save();
                return RedirectToAction("ProjectEquipements", new { id = RefID });

            }
            catch
            {
                ViewBag.error = " An error Has occured";
                return View("EditCable", model);
            }
        }

        //
        // GET: /Equipment/Delete/5
        public ActionResult Delete(long id,long projectID)
        {
            
            equipementS.Delete(id);
            equipementS.Save();
            return RedirectToAction("ProjectEquipements", "Equipment", new { id = projectID });

        }


    }
}
