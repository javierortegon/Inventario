using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Inventario.Models;

namespace Inventario.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string mensaje = "")
        {
            ViewBag.Message = mensaje;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            using (var db = new inventarioEntities1())
            {
                var user = db.usuario.FirstOrDefault(e => e.email == email && e.password == password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.email, true);
                    return RedirectToAction("Index","Proveedor");
                }
                else
                {
                    return Login("Revise los datos");
                }
            }
        }

        [Authorize]
        public ActionResult CloseSession()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}