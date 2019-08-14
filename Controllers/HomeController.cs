using Oficial2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oficial2.Controllers
{
    public class HomeController : Controller
    {
        ///GET: Home
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario objUser)
        {
            if (ModelState.IsValid)
            {
                using (catalogoOficialEntities db = new catalogoOficialEntities())
                {
                    var obj = db.Usuario.Where(a => a.nome_Usuario.Equals(objUser.nome_Usuario) && a.senha_Usuario.Equals(objUser.senha_Usuario)).FirstOrDefault();
                    if (obj == null)
                    {
                        objUser.LoginErrorMessage = "Usuario ou senha incorretos";
                        return View("Login", objUser);
                    }
                    else
                    {
                        Session["id_Usuario"] = obj.id_Usuario;
                        return RedirectToAction("Index", "Index");

                    }
                }
            }
            return View(objUser);
        }

        public ActionResult Index()
        {
            if (Session["id_Usuario"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}