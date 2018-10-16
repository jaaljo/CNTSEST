using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CNTS.Controllers
{
    public class Informe002Controller : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: Informe002
        public ActionResult Index()
        {
            var k_cnts_01_002 = db.k_cnts_01_002.Include(k => k.c_establecimiento).Include(k => k.c_periodo);
            return View(k_cnts_01_002.ToList());
        }

        // GET: Informe002/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_002 k_cnts_01_002 = db.k_cnts_01_002.Find(id);
            if (k_cnts_01_002 == null)
            {
                return HttpNotFound();
            }
            return View(k_cnts_01_002);
        }

        // GET: Informe002/Create
        public ActionResult Create()
        {
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid");
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo");
            return View();
        }

        // POST: Informe002/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cnts_01_002,fe_registro,fe_envio,uuid,nb_archivo_informe,esta_activo,id_establecimiento,id_periodo")] k_cnts_01_002 k_cnts_01_002)
        {
            if (ModelState.IsValid)
            {
                db.k_cnts_01_002.Add(k_cnts_01_002);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_002.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_002.id_periodo);
            return View(k_cnts_01_002);
        }

        // GET: Informe002/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_002 k_cnts_01_002 = db.k_cnts_01_002.Find(id);
            if (k_cnts_01_002 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_002.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_002.id_periodo);
            return View(k_cnts_01_002);
        }

        // POST: Informe002/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cnts_01_002,fe_registro,fe_envio,uuid,nb_archivo_informe,esta_activo,id_establecimiento,id_periodo")] k_cnts_01_002 k_cnts_01_002)
        {
            if (ModelState.IsValid)
            {
                db.Entry(k_cnts_01_002).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_002.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_002.id_periodo);
            return View(k_cnts_01_002);
        }

        // GET: Informe002/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_002 k_cnts_01_002 = db.k_cnts_01_002.Find(id);
            if (k_cnts_01_002 == null)
            {
                return HttpNotFound();
            }
            return View(k_cnts_01_002);
        }

        // POST: Informe002/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            k_cnts_01_002 k_cnts_01_002 = db.k_cnts_01_002.Find(id);
            db.k_cnts_01_002.Remove(k_cnts_01_002);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileResult CNTS_01_002(int? id)
        {
            MemoryStream os = new MemoryStream();

            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, os);
            doc.Open();
            Rectangle rec2 = new Rectangle(PageSize.LETTER);
            doc.SetPageSize(rec2);

            Font fontH1 = new Font(null, 12, Font.BOLD);
            Font fontH1W = new Font(null, 12, Font.BOLD, BaseColor.WHITE);

            Font fontH2 = new Font(null, 10, Font.BOLD);
            Font fontH2W = new Font(null, 10, Font.BOLD, BaseColor.WHITE);

            Font fontC1 = new Font(null, 7, Font.NORMAL);
            Font fontC1Bold = new Font(null, 7, Font.BOLD);
            Font fontC1W = new Font(null, 3, Font.NORMAL, BaseColor.WHITE);

            Font fontC2 = new Font(null, 6, Font.NORMAL);
            Font fontC2Bold = new Font(null, 6, Font.BOLD);
            Font fontC2W = new Font(null, 6, Font.NORMAL, BaseColor.WHITE);

            Font fontC3 = new Font(null, 5, Font.NORMAL);
            Font fontC3Bold = new Font(null, 5, Font.BOLD);
            Font fontC3W = new Font(null, 5, Font.NORMAL, BaseColor.WHITE);

            Font fontC4 = new Font(null, 5, Font.NORMAL);
            Font fontC4Bold = new Font(null, 5, Font.BOLD);
            Font fontC4W = new Font(null, 5, Font.NORMAL, BaseColor.WHITE);

            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = Server.MapPath("~/Content/logoPDF.jpeg");

            var Titulo1 = new Paragraph("Hola mundo", fontH1);
            Titulo1.Alignment = Element.ALIGN_CENTER;
            doc.Add(Titulo1);
            doc.Close();
            return File(os.GetBuffer(), "application/pdf", "x.pdf");

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
