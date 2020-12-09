using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EstadiasUTTN.Models;
using EstadiasUTTN.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace EstadiasUTTN.Controllers
{
    public class OrganigramaController : Controller
    {
        // GET: Organigrama
        public ActionResult Index()
        {
            return View();
        }

        public FileResult Plantilla()
        {
            var ruta = Server.MapPath("/Archivos/Plantilla/ORGANIGRAMA UTTN.xlsx");
            return File(ruta, "application/xlsx", "ORGANIGRAMA UTTN.xlsx");
        }

        // GET: Organigrama/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Organigrama/Create
        public ActionResult Create()
        {
            var items = GetFiles();
            //return View(items);

            List<ListOrganigramaViewModel> lst;
            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                var iduser = User.Identity.GetUserId();
                lst = (from d in db.Organigrama
                       where d.idusuario == iduser
                           select new ListOrganigramaViewModel
                           {
                               Id = d.id,
                               Title = d.title,
                               Name = d.name,
                               Datetime = d.datetime,
                           }).ToList();
            }
            return View(lst);
        }

        // POST: Organigrama/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, OrganigramaViewModel model)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string adjunto = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Archivos/Organigrama"),
                                              adjunto);
                        file.SaveAs(path);
                        ViewBag.Message = "Archivo subido correctamente";
                    

                    using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
                    {
                        var oOrganigrama = new Organigrama();
                        oOrganigrama.title = adjunto;
                        oOrganigrama.name = adjunto;
                        oOrganigrama.datetime = DateTime.Now;
                        oOrganigrama.idusuario = User.Identity.GetUserId();

                        db.Organigrama.Add(oOrganigrama);
                        db.SaveChanges();
                    }

                    
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "No tienes un archivo especificado.";
            }

            var items = GetFiles();
            return Redirect("~/Organigrama/Create/");
        }

        public FileResult Download(string ImageName)
        {
            var FileVirtualPath = "~/Archivos/Organigrama/" + ImageName;
            return File(FileVirtualPath, "application/force- download", Path.GetFileName(FileVirtualPath));
        }

        public ActionResult DeleteFile (string ImageDelete)
        {
            var FileVirtualPath = Path.Combine(HttpContext.Server.MapPath("/Archivos/Organigrama/"), ImageDelete);
            //var FileVirtualPath = "/Archivos/Organigrama/" + ImageNameDelete;
            System.IO.File.Delete(FileVirtualPath);

            
            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                var x = (from d in db.Organigrama
                       where d.title == ImageDelete
                       select d).FirstOrDefault();

                db.Organigrama.Remove(x);
                db.SaveChanges();
            }

            ViewBag.Message = "Archivo eliminado";
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        private List<string> GetFiles()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Archivos/Organigrama"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");

            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }

            return items;
        }

        public ActionResult Usuarios()
        {
            List<ListUsersViewModel> lst;
            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                lst = (from d in db.Organigrama
                       join s in db.AspNetUsers on d.idusuario equals s.Id
                       orderby s.UserName
                       select new ListUsersViewModel
                       {
                           Nombre = s.Nombre,
                           Apellido = s.Apellido,
                           Idusuario = s.Id
                       }).Distinct().ToList();
            }
            return View(lst);
        }
        //[HttpPost]
        public ActionResult UsuariosOrg(ListUsersViewModel user, string iduser)
        {
            List<ListOrganigramaViewModel> lst;
            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                lst = (from d in db.Organigrama
                       join s in db.AspNetUsers on d.idusuario equals s.Id
                       where d.idusuario == iduser
                       select new ListOrganigramaViewModel
                       {
                           Id = d.id,
                           Title = d.title,
                           Name = d.name,
                           Datetime = d.datetime,
                       }).ToList();
            }
            return View(lst);
        }

        public ActionResult UsuariosOrgAll()
        {
            List<ListOrganigramaViewModel> lst;
            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                lst = (from d in db.Organigrama
                       where d.idusuario != null
                       select new ListOrganigramaViewModel
                       {
                           Id = d.id,
                           Title = d.title,
                           Name = d.name,
                           Datetime = d.datetime,
                       }).ToList();
            }
            return View(lst);
        }

        // GET: Organigrama/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Organigrama/Edit/5
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

        // GET: Organigrama/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Organigrama/Delete/5
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
