using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventario.Models;

namespace Inventario.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.producto.Add(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        public ActionResult listarProveedores()
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    return View(db.proveedor.ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }

        }

        public static string NombreProveedor(int? idProveedor)
        {
            using (var db = new inventarioEntities1())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }


        public ActionResult Edit(int id)
        {
            using (var db = new inventarioEntities1())
            {
                producto producto = db.producto.Where(a => a.id == id).FirstOrDefault();
                return View(producto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto producto)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto productoEdit = db.producto.Find(producto.id);
                    productoEdit.nombre = producto.nombre;
                    productoEdit.percio_unitario = producto.percio_unitario;
                    productoEdit.cantidad = producto.cantidad;
                    productoEdit.descripcion = producto.descripcion;
                    productoEdit.id_proveedor = producto.id_proveedor;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}