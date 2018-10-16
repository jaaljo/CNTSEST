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
    public class Informe003CController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: Informe003C
        public ActionResult Index()
        {
            var k_cnts_01_003_c = db.k_cnts_01_003_c.Include(k => k.c_establecimiento).Include(k => k.c_periodo);
            return View(k_cnts_01_003_c.ToList());
        }

        // GET: Informe003C/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_003_c k_cnts_01_003_c = db.k_cnts_01_003_c.Find(id);
            if (k_cnts_01_003_c == null)
            {
                return HttpNotFound();
            }
            return View(k_cnts_01_003_c);
        }

        // GET: Informe003C/Create
        public ActionResult Create()
        {
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid");
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo");
            return View();
        }

        // POST: Informe003C/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cnts_01_003_c,fe_registro,fe_envio,uuid,nb_archivo_informe,esta_activo,id_establecimiento,id_periodo")] k_cnts_01_003_c k_cnts_01_003_c)
        {
            if (ModelState.IsValid)
            {
                db.k_cnts_01_003_c.Add(k_cnts_01_003_c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_003_c.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_003_c.id_periodo);
            return View(k_cnts_01_003_c);
        }

        // GET: Informe003C/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_003_c k_cnts_01_003_c = db.k_cnts_01_003_c.Find(id);
            if (k_cnts_01_003_c == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_003_c.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_003_c.id_periodo);
            return View(k_cnts_01_003_c);
        }

        // POST: Informe003C/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cnts_01_003_c,fe_registro,fe_envio,uuid,nb_archivo_informe,esta_activo,id_establecimiento,id_periodo")] k_cnts_01_003_c k_cnts_01_003_c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(k_cnts_01_003_c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_003_c.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_003_c.id_periodo);
            return View(k_cnts_01_003_c);
        }

        // GET: Informe003C/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_003_c k_cnts_01_003_c = db.k_cnts_01_003_c.Find(id);
            if (k_cnts_01_003_c == null)
            {
                return HttpNotFound();
            }
            return View(k_cnts_01_003_c);
        }

        // POST: Informe003C/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            k_cnts_01_003_c k_cnts_01_003_c = db.k_cnts_01_003_c.Find(id);
            db.k_cnts_01_003_c.Remove(k_cnts_01_003_c);
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
