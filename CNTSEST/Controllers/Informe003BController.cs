using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;

namespace CNTS.Controllers
{
    public class Informe003BController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: Informe003B
        public ActionResult Index()
        {
            var k_cnts_01_003_b = db.k_cnts_01_003_b.Include(k => k.c_establecimiento).Include(k => k.c_periodo);
            return View(k_cnts_01_003_b.ToList());
        }

        // GET: Informe003B/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_003_b k_cnts_01_003_b = db.k_cnts_01_003_b.Find(id);
            if (k_cnts_01_003_b == null)
            {
                return HttpNotFound();
            }
            return View(k_cnts_01_003_b);
        }

        // GET: Informe003B/Create
        public ActionResult Create()
        {
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid");
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo");
            return View();
        }

        // POST: Informe003B/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cnts_01_003_b,fe_registro,fe_envio,uuid,nb_archivo_informe,esta_activo,id_establecimiento,id_periodo")] k_cnts_01_003_b k_cnts_01_003_b)
        {
            if (ModelState.IsValid)
            {
                db.k_cnts_01_003_b.Add(k_cnts_01_003_b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_003_b.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_003_b.id_periodo);
            return View(k_cnts_01_003_b);
        }

        // GET: Informe003B/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_003_b k_cnts_01_003_b = db.k_cnts_01_003_b.Find(id);
            if (k_cnts_01_003_b == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_003_b.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_003_b.id_periodo);
            return View(k_cnts_01_003_b);
        }

        // POST: Informe003B/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cnts_01_003_b,fe_registro,fe_envio,uuid,nb_archivo_informe,esta_activo,id_establecimiento,id_periodo")] k_cnts_01_003_b k_cnts_01_003_b)
        {
            if (ModelState.IsValid)
            {
                db.Entry(k_cnts_01_003_b).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_003_b.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_003_b.id_periodo);
            return View(k_cnts_01_003_b);
        }

        // GET: Informe003B/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_003_b k_cnts_01_003_b = db.k_cnts_01_003_b.Find(id);
            if (k_cnts_01_003_b == null)
            {
                return HttpNotFound();
            }
            return View(k_cnts_01_003_b);
        }

        // POST: Informe003B/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            k_cnts_01_003_b k_cnts_01_003_b = db.k_cnts_01_003_b.Find(id);
            db.k_cnts_01_003_b.Remove(k_cnts_01_003_b);
            db.SaveChanges();
            return RedirectToAction("Index");
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
