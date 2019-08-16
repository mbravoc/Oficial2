using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Oficial2.Models;

namespace Oficial2.Controllers
{
    public class CatalogoController : Controller
    {
        private catalogoOficialEntities db = new catalogoOficialEntities();

        // GET: Catalogo
        public ActionResult IndexCatalogo()
        {
            var catalogo = db.Catalogo.Include(c => c.Carro1);
            return View(catalogo.ToList());
        }

        // GET: Catalogo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = db.Catalogo.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // GET: Catalogo/Create
        public ActionResult Create()
        {
            ViewBag.Carro = new SelectList(db.Carro, "id_carro", "nome_Carro");
            return View();
        }

        // POST: Catalogo/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Catalogo,Carro")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                db.Catalogo.Add(catalogo);
                db.SaveChanges();
                return RedirectToAction("IndexCatalogo");
            }

            ViewBag.Carro = new SelectList(db.Carro, "id_carro", "nome_Carro", catalogo.Carro);
            return View(catalogo);
        }

        // GET: Catalogo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = db.Catalogo.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Carro = new SelectList(db.Carro, "id_carro", "nome_Carro", catalogo.Carro);
            return View(catalogo);
        }

        // POST: Catalogo/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Catalogo,Carro")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexCatalogo");
            }
            ViewBag.Carro = new SelectList(db.Carro, "id_carro", "nome_Carro", catalogo.Carro);
            return View(catalogo);
        }

        // GET: Catalogo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = db.Catalogo.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // POST: Catalogo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catalogo catalogo = db.Catalogo.Find(id);
            db.Catalogo.Remove(catalogo);
            db.SaveChanges();
            return RedirectToAction("IndexCatalogo");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
