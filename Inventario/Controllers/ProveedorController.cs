using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventario.Models;

namespace Inventario.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.proveedor.ToList());
            }
               
        }

       
        public ActionResult Edit(int id)
        {
            using (var db = new inventarioEntities1())
            {
                proveedor objProveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                return View(objProveedor);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(proveedor proveedor)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    proveedor objProveedor = db.proveedor.Find(proveedor.id);
                    objProveedor.nombre = proveedor.nombre;
                    objProveedor.direccion = proveedor.direccion;
                    objProveedor.telefono = proveedor.telefono;
                    objProveedor.nombre_contacto = proveedor.nombre_contacto;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                throw (ex);
            }
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.proveedor.Add(proveedor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        public ActionResult Reporte()
        {
            try
            {
                var db = new inventarioEntities1();
                    var query = from proveedor in db.proveedor
                                join producto in db.producto on proveedor.id equals producto.id_proveedor
                                select new reporte { producto = producto.nombre, proveedor = proveedor.nombre };

                    return View(query);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }
    }
}