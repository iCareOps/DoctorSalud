using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Configuration;

namespace DoctorSalud.Controllers.Modulos
{
    public class ModulosController : Controller
    {
        DoctorSalud_Entities db = new DoctorSalud_Entities();

        // GET: Modulos
        public ActionResult Index(int id)
        {
            ViewBag.ID = id;

            return View();
        }

        public ActionResult SignosVitales(int id)
        {
            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Inicio_SignosVitales == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Inicio_SignosVitales = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }


            ViewBag.ID = id;

            return View();
        }

        public ActionResult MedicinaInterna(int id)
        {
            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Inicio_MedicinaInterna == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Inicio_MedicinaInterna = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }


            ViewBag.ID = id;

            return View();
        }


        public ActionResult Oftalmologia(int id)
        {
            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Inicio_Oftalmologia == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Inicio_Oftalmologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }


            ViewBag.ID = id;

            return View();
        }

        public ActionResult Cardiologia(int id)
        {
            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Inicio_Cardiologia == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Inicio_Cardiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }


            ViewBag.ID = id;

            return View();
        }

        public ActionResult Nutricion(int id)
        {
            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Inicio_Nutriciom == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Inicio_Nutriciom = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }


            ViewBag.ID = id;

            return View();
        }

        /*----------------------------------GUARDAR DATOS POR MÓDULO---------------------------------*/

        [HttpPost]
        public ActionResult Guardar_SignosVitales(int id, double sistolica, double diastolica, double cardiaca, double respiratoria, double temperatura, double peso, double estatura, double cintura, double cuello, double grasa)
        {
            SignosVitalesDS sv = new SignosVitalesDS();
            sv.idPacienteDS = id;
            sv.IMC = Math.Round(peso / (Math.Pow((estatura / 100), 2)), 2).ToString();
            sv.Sistolica = sistolica.ToString();
            sv.Diastolica = diastolica.ToString();
            sv.Cardiaca = cardiaca.ToString();
            sv.Respiratoria = respiratoria.ToString();
            sv.Temperatura = temperatura.ToString();
            sv.Peso = peso.ToString();
            sv.Estatura = estatura.ToString();
            sv.Cintura = cintura.ToString();
            sv.Cuello = cuello.ToString();
            sv.Grasa = grasa.ToString();

            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Fin_SignosVitales == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Fin_SignosVitales = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.SignosVitalesDS.Add(sv);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "Modulos", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_MedicinaInterna(int id, string certificado, string tratamiento)
        {
            MedicinaInterna medicina = new MedicinaInterna();
            medicina.idPacienteDS = id;
            medicina.CertificadoMedico = certificado;
            medicina.PlanTratamiento = tratamiento;

            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Fin_MedicinaInterna == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Fin_MedicinaInterna = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.MedicinaInterna.Add(medicina);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            TempData["ID_MG"] = id;
            return RedirectToAction("Index", "Modulos", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Oftalmologia(int id, string certificado, string tratamiento)
        {
            Oftalmologo oftalmologia = new Oftalmologo();
            oftalmologia.idPacienteDS = id;
            oftalmologia.CertificadoMedico = certificado;
            oftalmologia.PlanTratamiento = tratamiento;

            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Fin_Oftalmologia == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Fin_Oftalmologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Oftalmologo.Add(oftalmologia);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            TempData["ID_OFTALMOLOGIA"] = id;
            return RedirectToAction("Index", "Modulos", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Cardiologia(int id, string certificado, string tratamiento)
        {
            Cardiologo cardio = new Cardiologo();
            cardio.idPacienteDS = id;
            cardio.CertificadoMedico = certificado;
            cardio.PlanTratamiento = tratamiento;

            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Fin_Cardiologia == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Fin_Cardiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Cardiologo.Add(cardio);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            TempData["ID_CARDIOLOGIA"] = id;
            return RedirectToAction("Index", "Modulos", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Nutricion(int id, string certificado, string tratamiento)
        {
            Nutriologo nutri = new Nutriologo();
            nutri.idPacienteDS = id;
            nutri.CertificadoMedico = certificado;
            nutri.PlanTratamiento = tratamiento;

            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (revision.Fin_Nutricion == null)
            {
                var cm = db.CitaDS.Find(revision.idCitaDS);
                cm.Fin_Nutricion = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Nutriologo.Add(nutri);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            TempData["ID_NUTRICION"] = id;
            return RedirectToAction("Index", "Modulos", new { id = id });
        }

        public ActionResult Guardar_Farmacia(int id, string paracetamol, string metformina, string enalapril, string captopril, string hidroclorotiazida,
            string pioglitazona)
        {
            Farmacia farmacia = new Farmacia();
            farmacia.idPacienteDS = id;

            string receta = "";

            if(paracetamol == "on")
            {
                receta += "Paracetamol, ";
            }
            if (metformina == "on")
            {
                receta += "Metformina, ";
            }
            if (enalapril == "on")
            {
                receta += "Enalapril, ";
            }
            if (captopril == "on")
            {
                receta += "Captopril, ";
            }
            if (hidroclorotiazida == "on")
            {
                receta += "Hidroclorotiazida, ";
            }
            if (pioglitazona == "on")
            {
                receta += "Pioglitazona, ";
            }

            farmacia.MedicamentoGeneral = receta;

            if (ModelState.IsValid)
            {
                db.Farmacia.Add(farmacia);
                db.SaveChanges();
            }

            ViewBag.idPaciente = id;
            TempData["ID_FARMACIA"] = id;
            return RedirectToAction("Farmacia", "Recepcion");
        }

        public ActionResult DescargarMG(int? id)
        {

            System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
            System.Console.WriteLine("Se entra al metodo descargar");


            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            requiredPath = requiredPath.Replace("file:\\", "");

            System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
            System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

            var headerMG = requiredPath + ConfigurationManager.AppSettings["MEDICINAGENERAL"];

            var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
            var cita = (from p in db.MedicinaInterna where p.idPacienteDS == id orderby p.idMedicinaInterna descending select p).FirstOrDefault();
            var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

            //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
            //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


            /*----------------------------------PDFs Certificados----------------------------------*/

            if (true)
            {
                System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
                System.Console.WriteLine("Entra al llenado del pdf");

                string noEstudio = paciente.idPacienteDS.ToString();
                string nombre = paciente.Nombre;
                string email = paciente.Email;
                string telefono = paciente.Telefono;

                string certificado = cita.CertificadoMedico;
                string tratamiento = cita.PlanTratamiento;

                //Comienza el armado del archivo
                Document doc = new Document(PageSize.A4.Rotate());
                var mem = new MemoryStream();
                iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
                //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
                //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                doc.Open();
                System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
                System.Console.WriteLine("Se prepara para obtener las imagenes");

                iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

                System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                PNG1.Alignment = Image.ALIGN_CENTER;

                PNG1.ScaleAbsolute(650, 120);

                var color = new iTextSharp.text.BaseColor(128, 128, 128);
                var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
                var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
                string coloro = "";
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                Chunk c01 = new Chunk("\n", font);
                Chunk c02 = new Chunk("\n", font);
                Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
                Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
                Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
                Chunk c3 = new Chunk("\n", font);


                Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
                Chunk c5 = new Chunk("Presión Arterial Sistólica: " +SV.Sistolica+ " \n", font);
                Chunk c6 = new Chunk("Presión Arterial Diastólica: " +SV.Diastolica+ " \n", font);
                Chunk c7 = new Chunk("Frecuencia Cardiaca: " +SV.Cardiaca+" \n", font);
                Chunk c8 = new Chunk("Frecuencia Respiratoria: " +SV.Respiratoria+" \n", font);
                Chunk c9 = new Chunk("Peso: " +SV.Peso+" kgs\n", font);
                Chunk c10 = new Chunk("Estatura: "+SV.Estatura+" cms\n", font);
                Chunk c11 = new Chunk("IMC: "+SV.IMC+"\n", font);
                Chunk c12 = new Chunk("Cuello: "+SV.Cuello+" cms\n", font);
                Chunk c13 = new Chunk("Cintura: "+SV.Cintura+" cms\n", font);
                Chunk c14 = new Chunk("Porcentaje de Grasa: "+SV.Grasa+"%\n", font);
                Chunk c15 = new Chunk("\n", font);
                //Chunk c16= new Chunk("\n", font);
                Chunk c17 = new Chunk("CERTIFICADO MÉDICO: \n"+cita.CertificadoMedico+"\n", font);
                Chunk c18 = new Chunk("PLAN DE TRATAMIENTO:\n "+cita.PlanTratamiento+"\n", font);

 
                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);
                //p.Add(c01);
                //p.Add(c02);
                p.Add(c0);
                p.Add(c1);
                p.Add(c2);
                p.Add(c3);
                p.Add(c4);
                p.Add(c5);
                p.Add(c6);
                p.Add(c7);
                p.Add(c8);
                p.Add(c9);
                p.Add(c10);
                p.Add(c11);
                p.Add(c12);
                p.Add(c13);
                p.Add(c14);
                p.Add(c15);
                //p.Add(c16);
                p.Add(c17);
                p.Add(c18);

                doc.Add(p);
                doc.Add(pr);
                PdfPTable table = new PdfPTable(3);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.WidthPercentage = 75f;
                table.AddCell(codeQRImage);
                table.AddCell("");

                System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
                System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

                doc.Add(table);
                doc.Close();
                wri.Close();


                var pdf = mem.ToArray();
                string file = Convert.ToBase64String(pdf);

                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

                mem.Close();

                byte[] bytes2 = mem.ToArray();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
                Response.BinaryWrite(bytes2);
                Response.End();

                return File(bytes2, "application/pdf");
            }

            return Redirect("Index");
        }

        public ActionResult DescargarNUTRICION(int? id)
        {

            System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
            System.Console.WriteLine("Se entra al metodo descargar");


            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            requiredPath = requiredPath.Replace("file:\\", "");

            System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
            System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

            var headerMG = requiredPath + ConfigurationManager.AppSettings["NUTRICION"];

            var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
            var cita = (from p in db.Nutriologo where p.idPacienteDS == id orderby p.idNutriologo descending select p).FirstOrDefault();
            var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

            //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
            //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


            /*----------------------------------PDFs Certificados----------------------------------*/

            if (true)
            {
                System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
                System.Console.WriteLine("Entra al llenado del pdf");

                string noEstudio = paciente.idPacienteDS.ToString();
                string nombre = paciente.Nombre;
                string email = paciente.Email;
                string telefono = paciente.Telefono;

                string certificado = cita.CertificadoMedico;
                string tratamiento = cita.PlanTratamiento;

                //Comienza el armado del archivo
                Document doc = new Document(PageSize.A4.Rotate());
                var mem = new MemoryStream();
                iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
                //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
                //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                doc.Open();
                System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
                System.Console.WriteLine("Se prepara para obtener las imagenes");

                iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

                System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                PNG1.Alignment = Image.ALIGN_CENTER;

                PNG1.ScaleAbsolute(650, 120);

                var color = new iTextSharp.text.BaseColor(128, 128, 128);
                var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
                var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
                string coloro = "";
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                Chunk c01 = new Chunk("\n", font);
                Chunk c02 = new Chunk("\n", font);
                Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
                Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
                Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
                Chunk c3 = new Chunk("\n", font);


                Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
                Chunk c5 = new Chunk("Presión Arterial Sistólica: " + SV.Sistolica + " \n", font);
                Chunk c6 = new Chunk("Presión Arterial Diastólica: " + SV.Diastolica + " \n", font);
                Chunk c7 = new Chunk("Frecuencia Cardiaca: " + SV.Cardiaca + " \n", font);
                Chunk c8 = new Chunk("Frecuencia Respiratoria: " + SV.Respiratoria + " \n", font);
                Chunk c9 = new Chunk("Peso: " + SV.Peso + " kgs\n", font);
                Chunk c10 = new Chunk("Estatura: " + SV.Estatura + " cms\n", font);
                Chunk c11 = new Chunk("IMC: " + SV.IMC + "\n", font);
                Chunk c12 = new Chunk("Cuello: " + SV.Cuello + " cms\n", font);
                Chunk c13 = new Chunk("Cintura: " + SV.Cintura + " cms\n", font);
                Chunk c14 = new Chunk("Porcentaje de Grasa: " + SV.Grasa + "%\n", font);
                Chunk c15 = new Chunk("\n", font);
                //Chunk c16 = new Chunk("\n", font);
                Chunk c17 = new Chunk("CERTIFICADO MÉDICO: \n" + cita.CertificadoMedico + "\n", font);
                Chunk c18 = new Chunk("PLAN DE TRATAMIENTO:\n " + cita.PlanTratamiento + "\n", font);


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);
                //p.Add(c01);
                //p.Add(c02);
                p.Add(c0);
                p.Add(c1);
                p.Add(c2);
                p.Add(c3);
                p.Add(c4);
                p.Add(c5);
                p.Add(c6);
                p.Add(c7);
                p.Add(c8);
                p.Add(c9);
                p.Add(c10);
                p.Add(c11);
                p.Add(c12);
                p.Add(c13);
                p.Add(c14);
                p.Add(c15);
                //p.Add(c16);
                p.Add(c17);
                p.Add(c18);

                doc.Add(p);
                doc.Add(pr);
                PdfPTable table = new PdfPTable(3);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.WidthPercentage = 75f;
                table.AddCell(codeQRImage);
                table.AddCell("");

                System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
                System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

                doc.Add(table);
                doc.Close();
                wri.Close();


                var pdf = mem.ToArray();
                string file = Convert.ToBase64String(pdf);

                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

                mem.Close();

                byte[] bytes2 = mem.ToArray();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
                Response.BinaryWrite(bytes2);
                Response.End();

                return File(bytes2, "application/pdf");
            }

            return Redirect("Index");
        }

        public ActionResult DescargarCARDIOLOGIA(int? id)
        {

            System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
            System.Console.WriteLine("Se entra al metodo descargar");


            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            requiredPath = requiredPath.Replace("file:\\", "");

            System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
            System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

            var headerMG = requiredPath + ConfigurationManager.AppSettings["CARDIOLOGIA"];

            var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
            var cita = (from p in db.Cardiologo where p.idPacienteDS == id orderby p.idCardiologo descending select p).FirstOrDefault();
            var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

            //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
            //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


            /*----------------------------------PDFs Certificados----------------------------------*/

            if (true)
            {
                System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
                System.Console.WriteLine("Entra al llenado del pdf");

                string noEstudio = paciente.idPacienteDS.ToString();
                string nombre = paciente.Nombre;
                string email = paciente.Email;
                string telefono = paciente.Telefono;

                string certificado = cita.CertificadoMedico;
                string tratamiento = cita.PlanTratamiento;

                //Comienza el armado del archivo
                Document doc = new Document(PageSize.A4.Rotate());
                var mem = new MemoryStream();
                iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
                //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
                //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                doc.Open();
                System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
                System.Console.WriteLine("Se prepara para obtener las imagenes");

                iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

                System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                PNG1.Alignment = Image.ALIGN_CENTER;

                PNG1.ScaleAbsolute(650, 120);

                var color = new iTextSharp.text.BaseColor(128, 128, 128);
                var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
                var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
                string coloro = "";
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                Chunk c01 = new Chunk("\n", font);
                Chunk c02 = new Chunk("\n", font);
                Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
                Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
                Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
                Chunk c3 = new Chunk("\n", font);


                Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
                Chunk c5 = new Chunk("Presión Arterial Sistólica: " + SV.Sistolica + " \n", font);
                Chunk c6 = new Chunk("Presión Arterial Diastólica: " + SV.Diastolica + " \n", font);
                Chunk c7 = new Chunk("Frecuencia Cardiaca: " + SV.Cardiaca + " \n", font);
                Chunk c8 = new Chunk("Frecuencia Respiratoria: " + SV.Respiratoria + " \n", font);
                Chunk c9 = new Chunk("Peso: " + SV.Peso + " kgs\n", font);
                Chunk c10 = new Chunk("Estatura: " + SV.Estatura + " cms\n", font);
                Chunk c11 = new Chunk("IMC: " + SV.IMC + "\n", font);
                Chunk c12 = new Chunk("Cuello: " + SV.Cuello + " cms\n", font);
                Chunk c13 = new Chunk("Cintura: " + SV.Cintura + " cms\n", font);
                Chunk c14 = new Chunk("Porcentaje de Grasa: " + SV.Grasa + "%\n", font);
                Chunk c15 = new Chunk("\n", font);
                //Chunk c16 = new Chunk("\n", font);
                Chunk c17 = new Chunk("CERTIFICADO MÉDICO: \n" + cita.CertificadoMedico + "\n", font);
                Chunk c18 = new Chunk("PLAN DE TRATAMIENTO:\n " + cita.PlanTratamiento + "\n", font);


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);
                //p.Add(c01);
                //p.Add(c02);
                p.Add(c0);
                p.Add(c1);
                p.Add(c2);
                p.Add(c3);
                p.Add(c4);
                p.Add(c5);
                p.Add(c6);
                p.Add(c7);
                p.Add(c8);
                p.Add(c9);
                p.Add(c10);
                p.Add(c11);
                p.Add(c12);
                p.Add(c13);
                p.Add(c14);
                p.Add(c15);
                //p.Add(c16);
                p.Add(c17);
                p.Add(c18);

                doc.Add(p);
                doc.Add(pr);
                PdfPTable table = new PdfPTable(3);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.WidthPercentage = 75f;
                table.AddCell(codeQRImage);
                table.AddCell("");

                System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
                System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

                doc.Add(table);
                doc.Close();
                wri.Close();


                var pdf = mem.ToArray();
                string file = Convert.ToBase64String(pdf);

                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

                mem.Close();

                byte[] bytes2 = mem.ToArray();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
                Response.BinaryWrite(bytes2);
                Response.End();

                return File(bytes2, "application/pdf");
            }

            return Redirect("Index");
        }

        public ActionResult DescargarOFTALMOLOGIA(int? id)
        {

            System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
            System.Console.WriteLine("Se entra al metodo descargar");


            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            requiredPath = requiredPath.Replace("file:\\", "");

            System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
            System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

            var headerMG = requiredPath + ConfigurationManager.AppSettings["OFTALMOLOGIA"];

            var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
            var cita = (from p in db.Oftalmologo where p.idPacienteDS == id orderby p.idOftalmologo descending select p).FirstOrDefault();
            var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

            //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
            //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


            /*----------------------------------PDFs Certificados----------------------------------*/

            if (true)
            {
                System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
                System.Console.WriteLine("Entra al llenado del pdf");

                string noEstudio = paciente.idPacienteDS.ToString();
                string nombre = paciente.Nombre;
                string email = paciente.Email;
                string telefono = paciente.Telefono;

                string certificado = cita.CertificadoMedico;
                string tratamiento = cita.PlanTratamiento;

                //Comienza el armado del archivo
                Document doc = new Document(PageSize.A4.Rotate());
                var mem = new MemoryStream();
                iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
                //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
                //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                doc.Open();
                System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
                System.Console.WriteLine("Se prepara para obtener las imagenes");

                iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

                System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                PNG1.Alignment = Image.ALIGN_CENTER;

                PNG1.ScaleAbsolute(650, 120);

                var color = new iTextSharp.text.BaseColor(128, 128, 128);
                var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
                var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
                string coloro = "";
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                Chunk c01 = new Chunk("\n", font);
                Chunk c02 = new Chunk("\n", font);
                Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
                Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
                Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
                Chunk c3 = new Chunk("\n", font);


                Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
                Chunk c5 = new Chunk("Presión Arterial Sistólica: " + SV.Sistolica + " \n", font);
                Chunk c6 = new Chunk("Presión Arterial Diastólica: " + SV.Diastolica + " \n", font);
                Chunk c7 = new Chunk("Frecuencia Cardiaca: " + SV.Cardiaca + " \n", font);
                Chunk c8 = new Chunk("Frecuencia Respiratoria: " + SV.Respiratoria + " \n", font);
                Chunk c9 = new Chunk("Peso: " + SV.Peso + " kgs\n", font);
                Chunk c10 = new Chunk("Estatura: " + SV.Estatura + " cms\n", font);
                Chunk c11 = new Chunk("IMC: " + SV.IMC + "\n", font);
                Chunk c12 = new Chunk("Cuello: " + SV.Cuello + " cms\n", font);
                Chunk c13 = new Chunk("Cintura: " + SV.Cintura + " cms\n", font);
                Chunk c14 = new Chunk("Porcentaje de Grasa: " + SV.Grasa + "%\n", font);
                Chunk c15 = new Chunk("\n", font);
                //Chunk c16 = new Chunk("\n", font);
                Chunk c17 = new Chunk("CERTIFICADO MÉDICO: \n" + cita.CertificadoMedico + "\n", font);
                Chunk c18 = new Chunk("PLAN DE TRATAMIENTO:\n " + cita.PlanTratamiento + "\n", font);


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);
                //p.Add(c01);
                //p.Add(c02);
                p.Add(c0);
                p.Add(c1);
                p.Add(c2);
                p.Add(c3);
                p.Add(c4);
                p.Add(c5);
                p.Add(c6);
                p.Add(c7);
                p.Add(c8);
                p.Add(c9);
                p.Add(c10);
                p.Add(c11);
                p.Add(c12);
                p.Add(c13);
                p.Add(c14);
                p.Add(c15);
                //p.Add(c16);
                p.Add(c17);
                p.Add(c18);

                doc.Add(p);
                doc.Add(pr);
                PdfPTable table = new PdfPTable(3);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.WidthPercentage = 75f;
                table.AddCell(codeQRImage);
                table.AddCell("");

                System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
                System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

                doc.Add(table);
                doc.Close();
                wri.Close();


                var pdf = mem.ToArray();
                string file = Convert.ToBase64String(pdf);

                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

                mem.Close();

                byte[] bytes2 = mem.ToArray();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
                Response.BinaryWrite(bytes2);
                Response.End();

                return File(bytes2, "application/pdf");
            }

            return Redirect("Index");
        }

        public ActionResult DescargarFARMACIA(int? id)
        {
            var recetas = (from r in db.Farmacia where r.idPacienteDS == id orderby r.idFarmacia descending select r.MedicamentoGeneral).FirstOrDefault();

            string[] recetaIndividual = recetas.Split(',');
            int longitud = recetaIndividual.Length;

            string medicamento = "";
            for(int i = 1; i < longitud; i++)
            {
                medicamento += "-" + recetaIndividual[i - 1].Replace(",", "") + "\n";
            }

            System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
            System.Console.WriteLine("Se entra al metodo descargar");


            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            requiredPath = requiredPath.Replace("file:\\", "");

            System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
            System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

            var headerMG = requiredPath + ConfigurationManager.AppSettings["FARMACIA"];

            var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
            var cita = (from p in db.Oftalmologo where p.idPacienteDS == id orderby p.idOftalmologo descending select p).FirstOrDefault();
            var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

            //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
            //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


            /*----------------------------------PDFs Certificados----------------------------------*/

            if (true)
            {
                System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
                System.Console.WriteLine("Entra al llenado del pdf");

                string noEstudio = paciente.idPacienteDS.ToString();
                string nombre = paciente.Nombre;
                string email = paciente.Email;
                string telefono = paciente.Telefono;

                string certificado = cita.CertificadoMedico;
                string tratamiento = cita.PlanTratamiento;

                //Comienza el armado del archivo
                Document doc = new Document(PageSize.A4.Rotate());
                var mem = new MemoryStream();
                iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
                //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
                //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                doc.Open();
                System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
                System.Console.WriteLine("Se prepara para obtener las imagenes");

                iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

                System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                PNG1.Alignment = Image.ALIGN_CENTER;

                PNG1.ScaleAbsolute(650, 120);

                var color = new iTextSharp.text.BaseColor(128, 128, 128);
                var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
                var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
                string coloro = "";
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                Chunk c01 = new Chunk("\n", font);
                Chunk c02 = new Chunk("\n", font);
                Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
                Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
                Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
                Chunk c3 = new Chunk("\n", font);


                Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
                Chunk c5 = new Chunk("Presión Arterial Sistólica: " + SV.Sistolica + " \n", font);
                Chunk c6 = new Chunk("Presión Arterial Diastólica: " + SV.Diastolica + " \n", font);
                Chunk c7 = new Chunk("Frecuencia Cardiaca: " + SV.Cardiaca + " \n", font);
                Chunk c8 = new Chunk("Frecuencia Respiratoria: " + SV.Respiratoria + " \n", font);
                Chunk c9 = new Chunk("Peso: " + SV.Peso + " kgs\n", font);
                Chunk c10 = new Chunk("Estatura: " + SV.Estatura + " cms\n", font);
                Chunk c11 = new Chunk("IMC: " + SV.IMC + "\n", font);
                Chunk c12 = new Chunk("Cuello: " + SV.Cuello + " cms\n", font);
                Chunk c13 = new Chunk("Cintura: " + SV.Cintura + " cms\n", font);
                Chunk c14 = new Chunk("Porcentaje de Grasa: " + SV.Grasa + "%\n", font);
                Chunk c15 = new Chunk("\n", font);
                //Chunk c16 = new Chunk("\n", font);
                Chunk c17 = new Chunk("PLAN DE TRATAMIENTO: \n" + medicamento + "\n", font);


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);
                //p.Add(c01);
                //p.Add(c02);
                p.Add(c0);
                p.Add(c1);
                p.Add(c2);
                p.Add(c3);
                p.Add(c4);
                p.Add(c5);
                p.Add(c6);
                p.Add(c7);
                p.Add(c8);
                p.Add(c9);
                p.Add(c10);
                p.Add(c11);
                p.Add(c12);
                p.Add(c13);
                p.Add(c14);
                p.Add(c15);
                //p.Add(c16);
                p.Add(c17);

                doc.Add(p);
                doc.Add(pr);
                PdfPTable table = new PdfPTable(3);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.WidthPercentage = 75f;
                table.AddCell(codeQRImage);
                table.AddCell("");

                System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
                System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

                doc.Add(table);
                doc.Close();
                wri.Close();


                var pdf = mem.ToArray();
                string file = Convert.ToBase64String(pdf);

                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

                mem.Close();

                byte[] bytes2 = mem.ToArray();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
                Response.BinaryWrite(bytes2);
                Response.End();

                return File(bytes2, "application/pdf");
            }

            return Redirect("Index");
        }

    }
}