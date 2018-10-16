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
    public class Informe003AController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult A01 (int? id)
        {
            k_secc_a_tabla_01 model = new k_secc_a_tabla_01();

            return View(model);
        }

        public ActionResult A02(int? id)
        {
            k_secc_a_tabla_02 model = new k_secc_a_tabla_02();

            return View(model);
        }

        public ActionResult A03(int? id)
        {
            k_secc_a_tabla_03 model = new k_secc_a_tabla_03();

            return View(model);
        }

        public ActionResult A04(int? id)
        {
            k_secc_a_tabla_04 model = new k_secc_a_tabla_04();

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_003_a k_cnts_01_003_a = db.k_cnts_01_003_a.Find(id);
            if (k_cnts_01_003_a == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_003_a.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_003_a.id_periodo);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_01, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_02, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_03, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_04, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_05, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_06, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_07, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_08, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            return View(k_cnts_01_003_a);
        }

        // POST: Informe003A/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cnts_01_003_a,fe_registro,fe_envio,uuid,nb_archivo_informe,esta_activo,id_establecimiento,id_periodo")] k_cnts_01_003_a k_cnts_01_003_a)
        {
            if (ModelState.IsValid)
            {
                db.Entry(k_cnts_01_003_a).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_establecimiento = new SelectList(db.c_establecimiento, "id_establecimiento", "uuid", k_cnts_01_003_a.id_establecimiento);
            ViewBag.id_periodo = new SelectList(db.c_periodo, "id_periodo", "cl_periodo", k_cnts_01_003_a.id_periodo);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_01, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_02, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_03, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_04, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_05, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_06, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_07, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            ViewBag.id_cnts_01_003_a = new SelectList(db.k_secc_a_tabla_08, "id_cnts_01_003_a", "id_cnts_01_003_a", k_cnts_01_003_a.id_cnts_01_003_a);
            return View(k_cnts_01_003_a);
        }

        // GET: Informe003A/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_cnts_01_003_a k_cnts_01_003_a = db.k_cnts_01_003_a.Find(id);
            if (k_cnts_01_003_a == null)
            {
                return HttpNotFound();
            }
            return View(k_cnts_01_003_a);
        }

        // POST: Informe003A/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            k_cnts_01_003_a k_cnts_01_003_a = db.k_cnts_01_003_a.Find(id);
            db.k_cnts_01_003_a.Remove(k_cnts_01_003_a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileResult GenerarPDF(int? id)
        {
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

            //var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            
            Document doc = new Document();
            Rectangle tamaño = new Rectangle(PageSize.LETTER);
            doc.SetPageSize(tamaño);
            doc.SetMargins(20f, 20f, 30f, 30f);
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();

                Image logo1 = Image.GetInstance(Server.MapPath("~/Content/Images/logo1.png"));
                logo1.ScaleToFit(50f, 50f);
                Image logo2 = Image.GetInstance(Server.MapPath("~/Content/Images/logo2.png"));
                logo2.ScaleToFit(50f, 50f);

                // ************************************* TABLA: TITULO DEL INFORME *************************************
                //
                PdfPTable tablaTitulo1 = new PdfPTable(3);
                tablaTitulo1.TotalWidth = 550f;
                tablaTitulo1.LockedWidth = true;
                float[] anchos = new float[] { 50f, 450f, 50f };
                tablaTitulo1.SetWidths(anchos);
                //tablaTitulo1.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell = new PdfPCell();
                cell.BorderColor = new BaseColor(System.Drawing.Color.White);

                // F1C1
                tablaTitulo1.AddCell(logo1);
                // F1C2
                PdfPTable tablaTitulo2 = new PdfPTable(1);
                
                cell = new PdfPCell(new Phrase("COMISIÓN COORDINADORA DE INSTITUTOS NACIONALES DE SALUD Y HOSPITALES DE ALTA ESPECIALIDAD", fontC1));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaTitulo2.AddCell(cell);
                cell = new PdfPCell(new Phrase("CENTRO NACIONAL DE LA TRANSFUSIÓN SANGUÍNEA", fontC1Bold));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaTitulo2.AddCell(cell);
                cell = new PdfPCell(new Phrase("INFORME MENSUAL DE LA DISPOSICIÓN DE SANGRE Y COMPONENTES SANGUÍNEOS", fontC1));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaTitulo2.AddCell(cell);
                cell = new PdfPCell(new Phrase("TRÁMITE: CNTS-01-003-A, MODALIDAD A) BANCOS DE SANGRE", fontC1));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaTitulo2.AddCell(cell);
                cell = new PdfPCell(new Phrase("Para ser llenado y firmado por el responsable sanitario del banco de sangre", fontC1Bold));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaTitulo2.AddCell(cell);
                cell = new PdfPCell(tablaTitulo2);
                cell.Padding = 0f;
                tablaTitulo1.AddCell(cell);
                // F1C3
                tablaTitulo1.AddCell(logo2);
                tablaTitulo1.AddCell(" ");
                tablaTitulo1.AddCell(" ");
                tablaTitulo1.AddCell(" ");

                doc.Add(tablaTitulo1);

                // ************************************* TABLA: DATOS GENERALES *************************************
                //
                PdfPTable tablaDG = new PdfPTable(1);
                tablaDG.TotalWidth = 550f;
                tablaDG.LockedWidth = true;
                // F1
                cell = new PdfPCell(new Phrase("Datos Generales del Establecimiento", fontC1Bold));
                tablaDG.AddCell(cell);
                // F2
                cell = new PdfPCell(new Phrase("Fecha en que se elabora el informe (DD/MM/AAAA):", fontC1));
                tablaDG.AddCell(cell);
                // F3
                cell = new PdfPCell(new Phrase("Razón social o denominación del establecimiento:", fontC1));
                tablaDG.AddCell(cell);
                // F4
                cell = new PdfPCell(new Phrase("Calle y núm. exterior e interior:", fontC1));
                tablaDG.AddCell(cell);
                // F5
                cell = new PdfPCell(new Phrase("Entre qué calles se encuentra el establecimeinto:", fontC1));
                tablaDG.AddCell(cell);
                // F
                cell = new PdfPCell(new Phrase("Delegación / Municipio:", fontC1));
                tablaDG.AddCell(cell);
                // F
                cell = new PdfPCell(new Phrase("Entidad Federativa:", fontC1));
                tablaDG.AddCell(cell);
                // F
                cell = new PdfPCell(new Phrase("Correo(s) electrónico(s):", fontC1));
                tablaDG.AddCell(cell);
                // F
                cell = new PdfPCell(new Phrase("Número de licencia sanitaria:", fontC1));
                tablaDG.AddCell(cell);
                // F
                cell = new PdfPCell(new Phrase("Pertence a:", fontC1));
                tablaDG.AddCell(cell);

                doc.Add(tablaDG);
            }

            catch (Exception ex)
            {
                //Log error;
            }

            finally
            {
                doc.Close();
            }
            return File(memoryStream.GetBuffer(), "application/pdf", "x.pdf");

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
