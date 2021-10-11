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

        [HttpPost]
        public ActionResult SignosVitales_Editar(int id, double? sistolica, double? diastolica, double? cardiaca, double? respiratoria, double? temperatura, double? peso, double? estatura, double? cintura, double? cuello, double? grasa)
        {
            var sv = (from i in db.SignosVitalesDS where i.idPacienteDS == id orderby i.idSignosVitalesDS descending select i).FirstOrDefault();

            sv.IMC = Math.Round((peso == null ? Convert.ToDouble(sv.Peso) : peso ?? default(double)) / (Math.Pow(((estatura == null ? Convert.ToDouble(sv.Estatura) : estatura ?? default(double)) / 100), 2)), 2).ToString();
            sv.Sistolica = sistolica == null ? sv.Sistolica : sistolica.ToString();
            sv.Diastolica = diastolica == null ? sv.Diastolica : diastolica.ToString();
            sv.Cardiaca = cardiaca == null ? sv.Cardiaca : cardiaca.ToString();
            sv.Respiratoria = respiratoria == null ? sv.Respiratoria : respiratoria.ToString();
            sv.Temperatura = temperatura == null ? sv.Temperatura : temperatura.ToString();
            sv.Peso = peso == null ? sv.Peso : peso.ToString();
            sv.Estatura = estatura == null ? sv.Estatura : estatura.ToString();
            sv.Cintura = cintura == null ? sv.Cintura : cintura.ToString();
            sv.Cuello = cuello == null ? sv.Cuello : cuello.ToString();
            sv.Grasa = grasa == null ? sv.Grasa : grasa.ToString();

            var revision = (from r in db.CitaDS where r.idPacienteDS == id select r).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(sv).State = EntityState.Modified;
                db.SaveChanges();
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "Modulos", new { id = id });
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
        public ActionResult Guardar_MedicinaInterna(int id, string certificado, string tratamiento, DateTime? seguimiento)
        {
            MedicinaInterna medicina = new MedicinaInterna();
            medicina.idPacienteDS = id;
            medicina.CertificadoMedico = certificado;
            medicina.PlanTratamiento = tratamiento;
            medicina.Seguimiento = seguimiento == null ? null : seguimiento;

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
            return RedirectToAction("Index", "Modulos", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Oftalmologia(int id, string certificado, string tratamiento, DateTime? seguimiento)
        {
            Oftalmologo oftalmologia = new Oftalmologo();
            oftalmologia.idPacienteDS = id;
            oftalmologia.CertificadoMedico = certificado;
            oftalmologia.PlanTratamiento = tratamiento;
            oftalmologia.Seguimiento = seguimiento == null ? null : seguimiento;

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
            return RedirectToAction("Index", "Modulos", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Cardiologia(int id, string certificado, string tratamiento, DateTime? seguimiento)
        {
            Cardiologo cardio = new Cardiologo();
            cardio.idPacienteDS = id;
            cardio.CertificadoMedico = certificado;
            cardio.PlanTratamiento = tratamiento;
            cardio.Seguimiento = seguimiento == null ? null : seguimiento;

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
            return RedirectToAction("Index", "Modulos", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Nutricion(int id, string certificado, string tratamiento, DateTime? seguimiento)
        {
            Nutriologo nutri = new Nutriologo();
            nutri.idPacienteDS = id;
            nutri.CertificadoMedico = certificado;
            nutri.PlanTratamiento = tratamiento;
            nutri.Seguimiento = seguimiento == null ? null : seguimiento;

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


        public ActionResult EntregaMedicamento(int id, string medicinainterna, string oftalmologia, string cardiologia, string nutricion)
        {
            string MI = medicinainterna == "on" ? "SI" : null;
            string CA = cardiologia == "on" ? "SI" : null;
            string NU = nutricion == "on" ? "SI" : null;
            string OF = oftalmologia == "on" ? "SI" : null;

            var MEDICINA = (from m in db.MedicinaInterna where m.idPacienteDS == id orderby m.idMedicinaInterna descending select m).FirstOrDefault();
            var CARDIOLOGIA = (from m in db.Cardiologo where m.idPacienteDS == id orderby m.idCardiologo descending select m).FirstOrDefault();
            var NUTRICION = (from m in db.Nutriologo where m.idPacienteDS == id orderby m.idNutriologo descending select m).FirstOrDefault();
            var OFTALMOLOGIA = (from m in db.Oftalmologo where m.idPacienteDS == id orderby m.idOftalmologo descending select m).FirstOrDefault();

            if (MEDICINA != null)
            {
                MEDICINA.Medicamento = MI;

                if (ModelState.IsValid)
                {
                    db.Entry(MEDICINA).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (CARDIOLOGIA != null)
            {
                CARDIOLOGIA.Medicamento = CA;

                if (ModelState.IsValid)
                {
                    db.Entry(CARDIOLOGIA).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (NUTRICION != null)
            {
                NUTRICION.Medicamento = NU;

                if (ModelState.IsValid)
                {
                    db.Entry(NUTRICION).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (OFTALMOLOGIA != null)
            {
                OFTALMOLOGIA.Medicamento = OF;

                if (ModelState.IsValid)
                {
                    db.Entry(OFTALMOLOGIA).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Farmacia", "Recepcion");
        }

        /*-------------------------MODULOS DE DESCARGA DE PDFs------------------------*/
        public ActionResult Certificado_MI(int id)
        {
            TempData["Cer_MI"] = id;

            return RedirectToAction("Farmacia", "Recepcion");
        }

        public ActionResult Tratamiento_MI(int id)
        {
            TempData["Tra_MI"] = id;

            return RedirectToAction("Farmacia", "Recepcion");
        }

        public ActionResult Certificado_CA(int id)
        {
            TempData["Cer_CA"] = id;

            return RedirectToAction("Farmacia", "Recepcion", new { id = id });
        }

        public ActionResult Tratamiento_CA(int id)
        {
            TempData["Tra_CA"] = id;

            return RedirectToAction("Farmacia", "Recepcion", new { id = id });
        }

        public ActionResult Certificado_NU(int id)
        {
            TempData["Cer_NU"] = id;

            return RedirectToAction("Farmacia", "Recepcion", new { id = id });
        }

        public ActionResult Tratamiento_NU(int id)
        {
            TempData["Tra_NU"] = id;

            return RedirectToAction("Farmacia", "Recepcion", new { id = id });
        }

        public ActionResult Certificado_OF(int id)
        {
            TempData["Cer_OF"] = id;

            return RedirectToAction("Farmacia", "Recepcion", new { id = id });
        }

        public ActionResult Tratamiento_OF(int id)
        {
            TempData["Tra_OF"] = id;

            return RedirectToAction("Farmacia", "Recepcion", new { id = id });
        }

        public ActionResult Tratamiento_FARMACIA(int id)
        {
            TempData["Tra_FA"] = id;

            return RedirectToAction("Farmacia", "Recepcion", new { id = id });
        }

        //public ActionResult DescargarMG(int? id)
        //{

        //    System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
        //    System.Console.WriteLine("Se entra al metodo descargar");


        //    var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
        //        System.IO.Path.GetDirectoryName(
        //          System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
        //    requiredPath = requiredPath.Replace("file:\\", "");

        //    System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
        //    System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

        //    var headerMG = requiredPath + ConfigurationManager.AppSettings["MEDICINAGENERAL"];

        //    var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
        //    var cita = (from p in db.MedicinaInterna where p.idPacienteDS == id orderby p.idMedicinaInterna descending select p).FirstOrDefault();
        //    var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

        //    //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
        //    //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


        //    /*----------------------------------PDFs Certificados----------------------------------*/

        //    if (true)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
        //        System.Console.WriteLine("Entra al llenado del pdf");

        //        string noEstudio = paciente.idPacienteDS.ToString();
        //        string nombre = paciente.Nombre;
        //        string email = paciente.Email;
        //        string telefono = paciente.Telefono;

        //        string certificado = cita.CertificadoMedico;
        //        string tratamiento = cita.PlanTratamiento;

        //        //Comienza el armado del archivo
        //        Document doc = new Document(PageSize.A4.Rotate());
        //        var mem = new MemoryStream();
        //        iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
        //        //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
        //        //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
        //        doc.Open();
        //        System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
        //        System.Console.WriteLine("Se prepara para obtener las imagenes");

        //        iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

        //        System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
        //        System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

        //        PNG1.Alignment = Image.ALIGN_CENTER;

        //        PNG1.ScaleAbsolute(650, 120);

        //        var color = new iTextSharp.text.BaseColor(128, 128, 128);
        //        var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
        //        var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
        //        string coloro = "";
        //        iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
        //        iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
        //        var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
        //        var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
        //        var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

        //        Chunk c01 = new Chunk("\n", font);
        //        Chunk c02 = new Chunk("\n", font);
        //        Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
        //        Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
        //        Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
        //        Chunk c3 = new Chunk("\n", font);


        //        Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
        //        Chunk c5 = new Chunk("Presión Arterial Sistólica: " +SV.Sistolica+ " \n", font);
        //        Chunk c6 = new Chunk("Presión Arterial Diastólica: " +SV.Diastolica+ " \n", font);
        //        Chunk c7 = new Chunk("Frecuencia Cardiaca: " +SV.Cardiaca+" \n", font);
        //        Chunk c8 = new Chunk("Frecuencia Respiratoria: " +SV.Respiratoria+" \n", font);
        //        Chunk c9 = new Chunk("Peso: " +SV.Peso+" kgs\n", font);
        //        Chunk c10 = new Chunk("Estatura: "+SV.Estatura+" cms\n", font);
        //        Chunk c11 = new Chunk("IMC: "+SV.IMC+"\n", font);
        //        Chunk c12 = new Chunk("Cuello: "+SV.Cuello+" cms\n", font);
        //        Chunk c13 = new Chunk("Cintura: "+SV.Cintura+" cms\n", font);
        //        Chunk c14 = new Chunk("Porcentaje de Grasa: "+SV.Grasa+"%\n", font);
        //        Chunk c15 = new Chunk("\n", font);
        //        //Chunk c16= new Chunk("\n", font);
        //        Chunk c17 = new Chunk("CERTIFICADO MÉDICO: \n"+cita.CertificadoMedico+"\n", font);
        //        Chunk c18 = new Chunk("PLAN DE TRATAMIENTO:\n "+cita.PlanTratamiento+"\n", font);

 
        //        System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
        //        System.Console.WriteLine("Se prepara para generar el QR");

        //        iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
        //        Image codeQRImage = barcodeQRCode.GetImage();
        //        codeQRImage.ScaleAbsolute(150, 150);
        //        codeQRImage.Alignment = Image.ALIGN_LEFT;
        //        doc.Add(PNG1);
        //        //p.Add(c01);
        //        //p.Add(c02);
        //        p.Add(c0);
        //        p.Add(c1);
        //        p.Add(c2);
        //        p.Add(c3);
        //        p.Add(c4);
        //        p.Add(c5);
        //        p.Add(c6);
        //        p.Add(c7);
        //        p.Add(c8);
        //        p.Add(c9);
        //        p.Add(c10);
        //        p.Add(c11);
        //        p.Add(c12);
        //        p.Add(c13);
        //        p.Add(c14);
        //        p.Add(c15);
        //        //p.Add(c16);
        //        p.Add(c17);
        //        p.Add(c18);

        //        doc.Add(p);
        //        doc.Add(pr);
        //        PdfPTable table = new PdfPTable(3);
        //        table.DefaultCell.Border = Rectangle.NO_BORDER;
        //        table.WidthPercentage = 75f;
        //        table.AddCell(codeQRImage);
        //        table.AddCell("");

        //        System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
        //        System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

        //        doc.Add(table);
        //        doc.Close();
        //        wri.Close();


        //        var pdf = mem.ToArray();
        //        string file = Convert.ToBase64String(pdf);

        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

        //        mem.Close();

        //        byte[] bytes2 = mem.ToArray();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
        //        Response.BinaryWrite(bytes2);
        //        Response.End();

        //        return File(bytes2, "application/pdf");
        //    }

        //    return Redirect("Index");
        //}

        //public ActionResult DescargarNUTRICION(int? id)
        //{

        //    System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
        //    System.Console.WriteLine("Se entra al metodo descargar");


        //    var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
        //        System.IO.Path.GetDirectoryName(
        //          System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
        //    requiredPath = requiredPath.Replace("file:\\", "");

        //    System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
        //    System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

        //    var headerMG = requiredPath + ConfigurationManager.AppSettings["NUTRICION"];

        //    var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
        //    var cita = (from p in db.Nutriologo where p.idPacienteDS == id orderby p.idNutriologo descending select p).FirstOrDefault();
        //    var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

        //    //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
        //    //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


        //    /*----------------------------------PDFs Certificados----------------------------------*/

        //    if (true)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
        //        System.Console.WriteLine("Entra al llenado del pdf");

        //        string noEstudio = paciente.idPacienteDS.ToString();
        //        string nombre = paciente.Nombre;
        //        string email = paciente.Email;
        //        string telefono = paciente.Telefono;

        //        string certificado = cita.CertificadoMedico;
        //        string tratamiento = cita.PlanTratamiento;

        //        //Comienza el armado del archivo
        //        Document doc = new Document(PageSize.A4.Rotate());
        //        var mem = new MemoryStream();
        //        iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
        //        //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
        //        //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
        //        doc.Open();
        //        System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
        //        System.Console.WriteLine("Se prepara para obtener las imagenes");

        //        iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

        //        System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
        //        System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

        //        PNG1.Alignment = Image.ALIGN_CENTER;

        //        PNG1.ScaleAbsolute(650, 120);

        //        var color = new iTextSharp.text.BaseColor(128, 128, 128);
        //        var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
        //        var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
        //        string coloro = "";
        //        iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
        //        iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
        //        var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
        //        var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
        //        var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

        //        Chunk c01 = new Chunk("\n", font);
        //        Chunk c02 = new Chunk("\n", font);
        //        Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
        //        Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
        //        Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
        //        Chunk c3 = new Chunk("\n", font);


        //        Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
        //        Chunk c5 = new Chunk("Presión Arterial Sistólica: " + SV.Sistolica + " \n", font);
        //        Chunk c6 = new Chunk("Presión Arterial Diastólica: " + SV.Diastolica + " \n", font);
        //        Chunk c7 = new Chunk("Frecuencia Cardiaca: " + SV.Cardiaca + " \n", font);
        //        Chunk c8 = new Chunk("Frecuencia Respiratoria: " + SV.Respiratoria + " \n", font);
        //        Chunk c9 = new Chunk("Peso: " + SV.Peso + " kgs\n", font);
        //        Chunk c10 = new Chunk("Estatura: " + SV.Estatura + " cms\n", font);
        //        Chunk c11 = new Chunk("IMC: " + SV.IMC + "\n", font);
        //        Chunk c12 = new Chunk("Cuello: " + SV.Cuello + " cms\n", font);
        //        Chunk c13 = new Chunk("Cintura: " + SV.Cintura + " cms\n", font);
        //        Chunk c14 = new Chunk("Porcentaje de Grasa: " + SV.Grasa + "%\n", font);
        //        Chunk c15 = new Chunk("\n", font);
        //        //Chunk c16 = new Chunk("\n", font);
        //        Chunk c17 = new Chunk("CERTIFICADO MÉDICO: \n" + cita.CertificadoMedico + "\n", font);
        //        Chunk c18 = new Chunk("PLAN DE TRATAMIENTO:\n " + cita.PlanTratamiento + "\n", font);


        //        System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
        //        System.Console.WriteLine("Se prepara para generar el QR");

        //        iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
        //        Image codeQRImage = barcodeQRCode.GetImage();
        //        codeQRImage.ScaleAbsolute(150, 150);
        //        codeQRImage.Alignment = Image.ALIGN_LEFT;
        //        doc.Add(PNG1);
        //        //p.Add(c01);
        //        //p.Add(c02);
        //        p.Add(c0);
        //        p.Add(c1);
        //        p.Add(c2);
        //        p.Add(c3);
        //        p.Add(c4);
        //        p.Add(c5);
        //        p.Add(c6);
        //        p.Add(c7);
        //        p.Add(c8);
        //        p.Add(c9);
        //        p.Add(c10);
        //        p.Add(c11);
        //        p.Add(c12);
        //        p.Add(c13);
        //        p.Add(c14);
        //        p.Add(c15);
        //        //p.Add(c16);
        //        p.Add(c17);
        //        p.Add(c18);

        //        doc.Add(p);
        //        doc.Add(pr);
        //        PdfPTable table = new PdfPTable(3);
        //        table.DefaultCell.Border = Rectangle.NO_BORDER;
        //        table.WidthPercentage = 75f;
        //        table.AddCell(codeQRImage);
        //        table.AddCell("");

        //        System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
        //        System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

        //        doc.Add(table);
        //        doc.Close();
        //        wri.Close();


        //        var pdf = mem.ToArray();
        //        string file = Convert.ToBase64String(pdf);

        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

        //        mem.Close();

        //        byte[] bytes2 = mem.ToArray();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
        //        Response.BinaryWrite(bytes2);
        //        Response.End();

        //        return File(bytes2, "application/pdf");
        //    }

        //    return Redirect("Index");
        //}

        //public ActionResult DescargarCARDIOLOGIA(int? id)
        //{

        //    System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
        //    System.Console.WriteLine("Se entra al metodo descargar");


        //    var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
        //        System.IO.Path.GetDirectoryName(
        //          System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
        //    requiredPath = requiredPath.Replace("file:\\", "");

        //    System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
        //    System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

        //    var headerMG = requiredPath + ConfigurationManager.AppSettings["CARDIOLOGIA"];

        //    var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
        //    var cita = (from p in db.Cardiologo where p.idPacienteDS == id orderby p.idCardiologo descending select p).FirstOrDefault();
        //    var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

        //    //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
        //    //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


        //    /*----------------------------------PDFs Certificados----------------------------------*/

        //    if (true)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
        //        System.Console.WriteLine("Entra al llenado del pdf");

        //        string noEstudio = paciente.idPacienteDS.ToString();
        //        string nombre = paciente.Nombre;
        //        string email = paciente.Email;
        //        string telefono = paciente.Telefono;

        //        string certificado = cita.CertificadoMedico;
        //        string tratamiento = cita.PlanTratamiento;

        //        //Comienza el armado del archivo
        //        Document doc = new Document(PageSize.A4.Rotate());
        //        var mem = new MemoryStream();
        //        iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
        //        //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
        //        //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
        //        doc.Open();
        //        System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
        //        System.Console.WriteLine("Se prepara para obtener las imagenes");

        //        iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

        //        System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
        //        System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

        //        PNG1.Alignment = Image.ALIGN_CENTER;

        //        PNG1.ScaleAbsolute(650, 120);

        //        var color = new iTextSharp.text.BaseColor(128, 128, 128);
        //        var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
        //        var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
        //        string coloro = "";
        //        iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
        //        iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
        //        var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
        //        var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
        //        var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

        //        Chunk c01 = new Chunk("\n", font);
        //        Chunk c02 = new Chunk("\n", font);
        //        Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
        //        Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
        //        Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
        //        Chunk c3 = new Chunk("\n", font);


        //        Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
        //        Chunk c5 = new Chunk("Presión Arterial Sistólica: " + SV.Sistolica + " \n", font);
        //        Chunk c6 = new Chunk("Presión Arterial Diastólica: " + SV.Diastolica + " \n", font);
        //        Chunk c7 = new Chunk("Frecuencia Cardiaca: " + SV.Cardiaca + " \n", font);
        //        Chunk c8 = new Chunk("Frecuencia Respiratoria: " + SV.Respiratoria + " \n", font);
        //        Chunk c9 = new Chunk("Peso: " + SV.Peso + " kgs\n", font);
        //        Chunk c10 = new Chunk("Estatura: " + SV.Estatura + " cms\n", font);
        //        Chunk c11 = new Chunk("IMC: " + SV.IMC + "\n", font);
        //        Chunk c12 = new Chunk("Cuello: " + SV.Cuello + " cms\n", font);
        //        Chunk c13 = new Chunk("Cintura: " + SV.Cintura + " cms\n", font);
        //        Chunk c14 = new Chunk("Porcentaje de Grasa: " + SV.Grasa + "%\n", font);
        //        Chunk c15 = new Chunk("\n", font);
        //        //Chunk c16 = new Chunk("\n", font);
        //        Chunk c17 = new Chunk("CERTIFICADO MÉDICO: \n" + cita.CertificadoMedico + "\n", font);
        //        Chunk c18 = new Chunk("PLAN DE TRATAMIENTO:\n " + cita.PlanTratamiento + "\n", font);


        //        System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
        //        System.Console.WriteLine("Se prepara para generar el QR");

        //        iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
        //        Image codeQRImage = barcodeQRCode.GetImage();
        //        codeQRImage.ScaleAbsolute(150, 150);
        //        codeQRImage.Alignment = Image.ALIGN_LEFT;
        //        doc.Add(PNG1);
        //        //p.Add(c01);
        //        //p.Add(c02);
        //        p.Add(c0);
        //        p.Add(c1);
        //        p.Add(c2);
        //        p.Add(c3);
        //        p.Add(c4);
        //        p.Add(c5);
        //        p.Add(c6);
        //        p.Add(c7);
        //        p.Add(c8);
        //        p.Add(c9);
        //        p.Add(c10);
        //        p.Add(c11);
        //        p.Add(c12);
        //        p.Add(c13);
        //        p.Add(c14);
        //        p.Add(c15);
        //        //p.Add(c16);
        //        p.Add(c17);
        //        p.Add(c18);

        //        doc.Add(p);
        //        doc.Add(pr);
        //        PdfPTable table = new PdfPTable(3);
        //        table.DefaultCell.Border = Rectangle.NO_BORDER;
        //        table.WidthPercentage = 75f;
        //        table.AddCell(codeQRImage);
        //        table.AddCell("");

        //        System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
        //        System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

        //        doc.Add(table);
        //        doc.Close();
        //        wri.Close();


        //        var pdf = mem.ToArray();
        //        string file = Convert.ToBase64String(pdf);

        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

        //        mem.Close();

        //        byte[] bytes2 = mem.ToArray();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
        //        Response.BinaryWrite(bytes2);
        //        Response.End();

        //        return File(bytes2, "application/pdf");
        //    }

        //    return Redirect("Index");
        //}

        //public ActionResult DescargarOFTALMOLOGIA(int? id)
        //{

        //    System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
        //    System.Console.WriteLine("Se entra al metodo descargar");


        //    var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
        //        System.IO.Path.GetDirectoryName(
        //          System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
        //    requiredPath = requiredPath.Replace("file:\\", "");

        //    System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
        //    System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

        //    var headerMG = requiredPath + ConfigurationManager.AppSettings["OFTALMOLOGIA"];

        //    var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
        //    var cita = (from p in db.Oftalmologo where p.idPacienteDS == id orderby p.idOftalmologo descending select p).FirstOrDefault();
        //    var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

        //    //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
        //    //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


        //    /*----------------------------------PDFs Certificados----------------------------------*/

        //    if (true)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
        //        System.Console.WriteLine("Entra al llenado del pdf");

        //        string noEstudio = paciente.idPacienteDS.ToString();
        //        string nombre = paciente.Nombre;
        //        string email = paciente.Email;
        //        string telefono = paciente.Telefono;

        //        string certificado = cita.CertificadoMedico;
        //        string tratamiento = cita.PlanTratamiento;

        //        //Comienza el armado del archivo
        //        Document doc = new Document(PageSize.A4.Rotate());
        //        var mem = new MemoryStream();
        //        iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
        //        //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
        //        //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
        //        doc.Open();
        //        System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
        //        System.Console.WriteLine("Se prepara para obtener las imagenes");

        //        iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

        //        System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
        //        System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

        //        PNG1.Alignment = Image.ALIGN_CENTER;

        //        PNG1.ScaleAbsolute(650, 120);

        //        var color = new iTextSharp.text.BaseColor(128, 128, 128);
        //        var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
        //        var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
        //        string coloro = "";
        //        iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
        //        iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
        //        var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
        //        var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
        //        var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

        //        Chunk c01 = new Chunk("\n", font);
        //        Chunk c02 = new Chunk("\n", font);
        //        Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
        //        Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
        //        Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
        //        Chunk c3 = new Chunk("\n", font);


        //        Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
        //        Chunk c5 = new Chunk("Presión Arterial Sistólica: " + SV.Sistolica + " \n", font);
        //        Chunk c6 = new Chunk("Presión Arterial Diastólica: " + SV.Diastolica + " \n", font);
        //        Chunk c7 = new Chunk("Frecuencia Cardiaca: " + SV.Cardiaca + " \n", font);
        //        Chunk c8 = new Chunk("Frecuencia Respiratoria: " + SV.Respiratoria + " \n", font);
        //        Chunk c9 = new Chunk("Peso: " + SV.Peso + " kgs\n", font);
        //        Chunk c10 = new Chunk("Estatura: " + SV.Estatura + " cms\n", font);
        //        Chunk c11 = new Chunk("IMC: " + SV.IMC + "\n", font);
        //        Chunk c12 = new Chunk("Cuello: " + SV.Cuello + " cms\n", font);
        //        Chunk c13 = new Chunk("Cintura: " + SV.Cintura + " cms\n", font);
        //        Chunk c14 = new Chunk("Porcentaje de Grasa: " + SV.Grasa + "%\n", font);
        //        Chunk c15 = new Chunk("\n", font);
        //        //Chunk c16 = new Chunk("\n", font);
        //        Chunk c17 = new Chunk("CERTIFICADO MÉDICO: \n" + cita.CertificadoMedico + "\n", font);
        //        Chunk c18 = new Chunk("PLAN DE TRATAMIENTO:\n " + cita.PlanTratamiento + "\n", font);


        //        System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
        //        System.Console.WriteLine("Se prepara para generar el QR");

        //        iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
        //        Image codeQRImage = barcodeQRCode.GetImage();
        //        codeQRImage.ScaleAbsolute(150, 150);
        //        codeQRImage.Alignment = Image.ALIGN_LEFT;
        //        doc.Add(PNG1);
        //        //p.Add(c01);
        //        //p.Add(c02);
        //        p.Add(c0);
        //        p.Add(c1);
        //        p.Add(c2);
        //        p.Add(c3);
        //        p.Add(c4);
        //        p.Add(c5);
        //        p.Add(c6);
        //        p.Add(c7);
        //        p.Add(c8);
        //        p.Add(c9);
        //        p.Add(c10);
        //        p.Add(c11);
        //        p.Add(c12);
        //        p.Add(c13);
        //        p.Add(c14);
        //        p.Add(c15);
        //        //p.Add(c16);
        //        p.Add(c17);
        //        p.Add(c18);

        //        doc.Add(p);
        //        doc.Add(pr);
        //        PdfPTable table = new PdfPTable(3);
        //        table.DefaultCell.Border = Rectangle.NO_BORDER;
        //        table.WidthPercentage = 75f;
        //        table.AddCell(codeQRImage);
        //        table.AddCell("");

        //        System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
        //        System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

        //        doc.Add(table);
        //        doc.Close();
        //        wri.Close();


        //        var pdf = mem.ToArray();
        //        string file = Convert.ToBase64String(pdf);

        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

        //        mem.Close();

        //        byte[] bytes2 = mem.ToArray();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
        //        Response.BinaryWrite(bytes2);
        //        Response.End();

        //        return File(bytes2, "application/pdf");
        //    }

        //    return Redirect("Index");
        //}

        //public ActionResult DescargarFARMACIA(int? id)
        //{
        //    var recetas = (from r in db.Farmacia where r.idPacienteDS == id orderby r.idFarmacia descending select r.MedicamentoGeneral).FirstOrDefault();

        //    string[] recetaIndividual = recetas.Split(',');
        //    int longitud = recetaIndividual.Length;

        //    string medicamento = "";
        //    for(int i = 1; i < longitud; i++)
        //    {
        //        medicamento += "-" + recetaIndividual[i - 1].Replace(",", "") + "\n";
        //    }

        //    System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
        //    System.Console.WriteLine("Se entra al metodo descargar");


        //    var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
        //        System.IO.Path.GetDirectoryName(
        //          System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
        //    requiredPath = requiredPath.Replace("file:\\", "");

        //    System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
        //    System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

        //    var headerMG = requiredPath + ConfigurationManager.AppSettings["FARMACIA"];

        //    var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
        //    var cita = (from p in db.Oftalmologo where p.idPacienteDS == id orderby p.idOftalmologo descending select p).FirstOrDefault();
        //    var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();

        //    //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
        //    //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


        //    /*----------------------------------PDFs Certificados----------------------------------*/

        //    if (true)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
        //        System.Console.WriteLine("Entra al llenado del pdf");

        //        string noEstudio = paciente.idPacienteDS.ToString();
        //        string nombre = paciente.Nombre;
        //        string email = paciente.Email;
        //        string telefono = paciente.Telefono;

        //        string certificado = cita.CertificadoMedico;
        //        string tratamiento = cita.PlanTratamiento;

        //        //Comienza el armado del archivo
        //        Document doc = new Document(PageSize.A4.Rotate());
        //        var mem = new MemoryStream();
        //        iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
        //        //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
        //        //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
        //        doc.Open();
        //        System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
        //        System.Console.WriteLine("Se prepara para obtener las imagenes");

        //        iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerMG);

        //        System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
        //        System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

        //        PNG1.Alignment = Image.ALIGN_CENTER;

        //        PNG1.ScaleAbsolute(650, 120);

        //        var color = new iTextSharp.text.BaseColor(128, 128, 128);
        //        var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
        //        var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
        //        string coloro = "";
        //        iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
        //        iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
        //        var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
        //        var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
        //        var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

        //        Chunk c01 = new Chunk("\n", font);
        //        Chunk c02 = new Chunk("\n", font);
        //        Chunk c0 = new Chunk("Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n", font);
        //        //Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
        //        Chunk c2 = new Chunk("Nombre: " + nombre + "\n", font);
        //        Chunk c3 = new Chunk("\n", font);


        //        Chunk c4 = new Chunk("SIGNOS VITALES: \n", font);
        //        Chunk c5 = new Chunk("Presión Arterial Sistólica: " + SV.Sistolica + " \n", font);
        //        Chunk c6 = new Chunk("Presión Arterial Diastólica: " + SV.Diastolica + " \n", font);
        //        Chunk c7 = new Chunk("Frecuencia Cardiaca: " + SV.Cardiaca + " \n", font);
        //        Chunk c8 = new Chunk("Frecuencia Respiratoria: " + SV.Respiratoria + " \n", font);
        //        Chunk c9 = new Chunk("Peso: " + SV.Peso + " kgs\n", font);
        //        Chunk c10 = new Chunk("Estatura: " + SV.Estatura + " cms\n", font);
        //        Chunk c11 = new Chunk("IMC: " + SV.IMC + "\n", font);
        //        Chunk c12 = new Chunk("Cuello: " + SV.Cuello + " cms\n", font);
        //        Chunk c13 = new Chunk("Cintura: " + SV.Cintura + " cms\n", font);
        //        Chunk c14 = new Chunk("Porcentaje de Grasa: " + SV.Grasa + "%\n", font);
        //        Chunk c15 = new Chunk("\n", font);
        //        //Chunk c16 = new Chunk("\n", font);
        //        Chunk c17 = new Chunk("PLAN DE TRATAMIENTO: \n" + medicamento + "\n", font);


        //        System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
        //        System.Console.WriteLine("Se prepara para generar el QR");

        //        iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
        //        Image codeQRImage = barcodeQRCode.GetImage();
        //        codeQRImage.ScaleAbsolute(150, 150);
        //        codeQRImage.Alignment = Image.ALIGN_LEFT;
        //        doc.Add(PNG1);
        //        //p.Add(c01);
        //        //p.Add(c02);
        //        p.Add(c0);
        //        //p.Add(c1);
        //        p.Add(c2);
        //        p.Add(c3);
        //        p.Add(c4);
        //        p.Add(c5);
        //        p.Add(c6);
        //        p.Add(c7);
        //        p.Add(c8);
        //        p.Add(c9);
        //        p.Add(c10);
        //        p.Add(c11);
        //        p.Add(c12);
        //        p.Add(c13);
        //        p.Add(c14);
        //        p.Add(c15);
        //        //p.Add(c16);
        //        p.Add(c17);

        //        doc.Add(p);
        //        doc.Add(pr);
        //        PdfPTable table = new PdfPTable(3);
        //        table.DefaultCell.Border = Rectangle.NO_BORDER;
        //        table.WidthPercentage = 75f;
        //        table.AddCell(codeQRImage);
        //        table.AddCell("");

        //        System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
        //        System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

        //        doc.Add(table);
        //        doc.Close();
        //        wri.Close();


        //        var pdf = mem.ToArray();
        //        string file = Convert.ToBase64String(pdf);

        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
        //        System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

        //        mem.Close();

        //        byte[] bytes2 = mem.ToArray();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
        //        Response.BinaryWrite(bytes2);
        //        Response.End();

        //        return File(bytes2, "application/pdf");
        //    }

        //    return Redirect("Index");
        //}


        public ActionResult Cer_MI(int? id)
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
                string direccion = paciente.Direccion + ", " + paciente.Colonia + ", C.P. " + paciente.CP + ", " + paciente.Ciudad + ", " + paciente.Estado;
                DateTime nacimiento = Convert.ToDateTime(paciente.Nacimiento); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
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
                var font2 = FontFactory.GetFont(coloro, 16, Font.NORMAL, color);
                var fontSpace = FontFactory.GetFont(coloro, 4, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                var tbl = new PdfPTable(new float[] { 50f, 25f, 25f}) { WidthPercentage = 100f };
                Chunk c0111 = new Chunk("\n", fontSpace);
                var tbl2 = new PdfPTable(new float[] { 100f}) { WidthPercentage = 100f };

                tbl.AddCell(new PdfPCell(new Phrase("DATOS DEL PACIENTE: \n\n" +
                    "Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n" +
                    "Nombre: " + nombre + "\n" +
                    "Dirección : " + direccion + "\n" +
                    "Edad: " + edad + "\n" +
                    "Fecha de Nacimiento: " + Convert.ToDateTime(paciente.Nacimiento).ToString("dd-MMMM-yyyy") + "\n", font))
                { BorderWidthRight = 0 } );

                tbl.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES: \n\n" +
                    "Presión Arterial Sistólica: " + SV.Sistolica + " \n" +
                    "Presión Arterial Diastólica: " + SV.Diastolica + " \n" +
                    "Frecuencia Cardiaca: " + SV.Cardiaca + " \n" +
                    "Frecuencia Respiratoria: " + SV.Respiratoria + " \n" +
                    "Peso: " + SV.Peso + " kgs\n\n", font))
                { BorderWidthRight = 0, BorderWidthLeft = 0 } );

                tbl.AddCell(new PdfPCell(new Phrase("\n \n" +
                    "Estatura: " + SV.Estatura + " cms\n" +
                    "IMC: " + SV.IMC + "\n" +
                    "Cuello: " + SV.Cuello + " cms\n" +
                    "Cintura: " + SV.Cintura + " cms\n" +
                    "Porcentaje de Grasa: " + SV.Grasa + "%\n\n", font))
                { BorderWidthLeft = 0 } );

                tbl2.AddCell(new PdfPCell(new Phrase("\nCERTIFICADO MÉDICO: \n \n" +
                    cita.CertificadoMedico + "\n\n\n\n\n\n", font2)));


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);

                p.Add(tbl);
                p.Add(c0111);
                p.Add(tbl2);

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

        public ActionResult Tra_MI(int? id)
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
                string direccion = paciente.Direccion + ", " + paciente.Colonia + ", C.P. " + paciente.CP + ", " + paciente.Ciudad + ", " + paciente.Estado;
                DateTime nacimiento = Convert.ToDateTime(paciente.Nacimiento); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
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
                var font2 = FontFactory.GetFont(coloro, 16, Font.NORMAL, color);
                var fontSpace = FontFactory.GetFont(coloro, 4, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                var tbl = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                Chunk c0111 = new Chunk("\n", fontSpace);
                var tbl2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                tbl.AddCell(new PdfPCell(new Phrase("DATOS DEL PACIENTE: \n\n" +
                    "Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n" +
                    "Nombre: " + nombre + "\n" +
                    "Dirección : " + direccion + "\n" +
                    "Edad: " + edad + "\n" +
                    "Fecha de Nacimiento: " + Convert.ToDateTime(paciente.Nacimiento).ToString("dd-MMMM-yyyy") + "\n", font))
                { BorderWidthRight = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES: \n\n" +
                    "Presión Arterial Sistólica: " + SV.Sistolica + " \n" +
                    "Presión Arterial Diastólica: " + SV.Diastolica + " \n" +
                    "Frecuencia Cardiaca: " + SV.Cardiaca + " \n" +
                    "Frecuencia Respiratoria: " + SV.Respiratoria + " \n" +
                    "Peso: " + SV.Peso + " kgs\n\n", font))
                { BorderWidthRight = 0, BorderWidthLeft = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("\n \n" +
                    "Estatura: " + SV.Estatura + " cms\n" +
                    "IMC: " + SV.IMC + "\n" +
                    "Cuello: " + SV.Cuello + " cms\n" +
                    "Cintura: " + SV.Cintura + " cms\n" +
                    "Porcentaje de Grasa: " + SV.Grasa + "%\n\n", font))
                { BorderWidthLeft = 0 });

                tbl2.AddCell(new PdfPCell(new Phrase("\nPLAN DE TRATAMIENTO: \n \n" +
                    cita.PlanTratamiento + "\n\n\n\n\n\n", font2)));


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);

                p.Add(tbl);
                p.Add(c0111);
                p.Add(tbl2);

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

        public ActionResult Cer_CA(int? id)
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
            var footerOscarAlfonso = requiredPath + ConfigurationManager.AppSettings["OscarAlfonso"];

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
                string direccion = paciente.Direccion + ", " + paciente.Colonia + ", C.P. " + paciente.CP + ", " + paciente.Ciudad + ", " + paciente.Estado;
                DateTime nacimiento = Convert.ToDateTime(paciente.Nacimiento); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
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
                iTextSharp.text.Image PNG2 = iTextSharp.text.Image.GetInstance(footerOscarAlfonso);

                System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                PNG1.Alignment = Image.ALIGN_CENTER;
                PNG1.ScaleAbsolute(650, 120);

                PNG2.Alignment = Image.ALIGN_CENTER;
                PNG2.ScaleAbsolute(650, 120);

                var color = new iTextSharp.text.BaseColor(128, 128, 128);
                var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
                var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
                string coloro = "";
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
                var font2 = FontFactory.GetFont(coloro, 13, Font.NORMAL, color);
                var fontSpace = FontFactory.GetFont(coloro, 4, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                var tbl = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                Chunk c0111 = new Chunk("\n", fontSpace);
                var tbl2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                tbl.AddCell(new PdfPCell(new Phrase("DATOS DEL PACIENTE: \n\n" +
                    "Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n" +
                    "Nombre: " + nombre + "\n" +
                    "Dirección : " + direccion + "\n" +
                    "Edad: " + edad + "\n" +
                    "Fecha de Nacimiento: " + Convert.ToDateTime(paciente.Nacimiento).ToString("dd-MMMM-yyyy") + "\n", font))
                { BorderWidthRight = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES: \n\n" +
                    "Presión Arterial Sistólica: " + SV.Sistolica + " \n" +
                    "Presión Arterial Diastólica: " + SV.Diastolica + " \n" +
                    "Frecuencia Cardiaca: " + SV.Cardiaca + " \n" +
                    "Frecuencia Respiratoria: " + SV.Respiratoria + " \n" +
                    "Peso: " + SV.Peso + " kgs\n\n", font))
                { BorderWidthRight = 0, BorderWidthLeft = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("\n \n" +
                    "Estatura: " + SV.Estatura + " cms\n" +
                    "IMC: " + SV.IMC + "\n" +
                    "Cuello: " + SV.Cuello + " cms\n" +
                    "Cintura: " + SV.Cintura + " cms\n" +
                    "Porcentaje de Grasa: " + SV.Grasa + "%\n\n", font))
                { BorderWidthLeft = 0 });

                tbl2.AddCell(new PdfPCell(new Phrase("\nCERTIFICADO MÉDICO: \n \n" +
                    cita.CertificadoMedico + "\n\n", font2)));


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);

                p.Add(tbl);
                p.Add(c0111);
                p.Add(tbl2);
                p.Add(PNG2);

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

        public ActionResult Tra_CA(int? id)
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
            var footerOscarAlfonso = requiredPath + ConfigurationManager.AppSettings["OscarAlfonso"];

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
                string direccion = paciente.Direccion + ", " + paciente.Colonia + ", C.P. " + paciente.CP + ", " + paciente.Ciudad + ", " + paciente.Estado;
                DateTime nacimiento = Convert.ToDateTime(paciente.Nacimiento); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
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
                iTextSharp.text.Image PNG2 = iTextSharp.text.Image.GetInstance(footerOscarAlfonso);

                System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                PNG1.Alignment = Image.ALIGN_CENTER;

                PNG1.ScaleAbsolute(650, 120);

                PNG2.Alignment = Image.ALIGN_CENTER;
                PNG2.ScaleAbsolute(650, 120);

                var color = new iTextSharp.text.BaseColor(128, 128, 128);
                var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
                var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
                string coloro = "";
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
                var font2 = FontFactory.GetFont(coloro, 13, Font.NORMAL, color);
                var fontSpace = FontFactory.GetFont(coloro, 4, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                var tbl = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                Chunk c0111 = new Chunk("\n", fontSpace);
                var tbl2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                tbl.AddCell(new PdfPCell(new Phrase("DATOS DEL PACIENTE: \n\n" +
                    "Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n" +
                    "Nombre: " + nombre + "\n" +
                    "Dirección : " + direccion + "\n" +
                    "Edad: " + edad + "\n" +
                    "Fecha de Nacimiento: " + Convert.ToDateTime(paciente.Nacimiento).ToString("dd-MMMM-yyyy") + "\n", font))
                { BorderWidthRight = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES: \n\n" +
                    "Presión Arterial Sistólica: " + SV.Sistolica + " \n" +
                    "Presión Arterial Diastólica: " + SV.Diastolica + " \n" +
                    "Frecuencia Cardiaca: " + SV.Cardiaca + " \n" +
                    "Frecuencia Respiratoria: " + SV.Respiratoria + " \n" +
                    "Peso: " + SV.Peso + " kgs\n\n", font))
                { BorderWidthRight = 0, BorderWidthLeft = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("\n \n" +
                    "Estatura: " + SV.Estatura + " cms\n" +
                    "IMC: " + SV.IMC + "\n" +
                    "Cuello: " + SV.Cuello + " cms\n" +
                    "Cintura: " + SV.Cintura + " cms\n" +
                    "Porcentaje de Grasa: " + SV.Grasa + "%\n\n", font))
                { BorderWidthLeft = 0 });

                tbl2.AddCell(new PdfPCell(new Phrase("\nPLAN DE TRATAMIENTO: \n \n" +
                    cita.PlanTratamiento + "\n\n", font2)));


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);

                p.Add(tbl);
                p.Add(c0111);
                p.Add(tbl2);
                p.Add(PNG2);

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

        public ActionResult Cer_OF(int? id)
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
                string direccion = paciente.Direccion + ", " + paciente.Colonia + ", C.P. " + paciente.CP + ", " + paciente.Ciudad + ", " + paciente.Estado;
                DateTime nacimiento = Convert.ToDateTime(paciente.Nacimiento); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
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
                var font2 = FontFactory.GetFont(coloro, 13, Font.NORMAL, color);
                var fontSpace = FontFactory.GetFont(coloro, 4, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                var tbl = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                Chunk c0111 = new Chunk("\n", fontSpace);
                var tbl2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                tbl.AddCell(new PdfPCell(new Phrase("DATOS DEL PACIENTE: \n\n" +
                    "Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n" +
                    "Nombre: " + nombre + "\n" +
                    "Dirección : " + direccion + "\n" +
                    "Edad: " + edad + "\n" +
                    "Fecha de Nacimiento: " + Convert.ToDateTime(paciente.Nacimiento).ToString("dd-MMMM-yyyy") + "\n", font))
                { BorderWidthRight = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES: \n\n" +
                    "Presión Arterial Sistólica: " + SV.Sistolica + " \n" +
                    "Presión Arterial Diastólica: " + SV.Diastolica + " \n" +
                    "Frecuencia Cardiaca: " + SV.Cardiaca + " \n" +
                    "Frecuencia Respiratoria: " + SV.Respiratoria + " \n" +
                    "Peso: " + SV.Peso + " kgs\n\n", font))
                { BorderWidthRight = 0, BorderWidthLeft = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("\n \n" +
                    "Estatura: " + SV.Estatura + " cms\n" +
                    "IMC: " + SV.IMC + "\n" +
                    "Cuello: " + SV.Cuello + " cms\n" +
                    "Cintura: " + SV.Cintura + " cms\n" +
                    "Porcentaje de Grasa: " + SV.Grasa + "%\n\n", font))
                { BorderWidthLeft = 0 });

                tbl2.AddCell(new PdfPCell(new Phrase("\nCERTIFICADO MÉDICO: \n \n" +
                    cita.CertificadoMedico + "\n\n", font2)));


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);

                p.Add(tbl);
                p.Add(c0111);
                p.Add(tbl2);

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

        public ActionResult Tra_OF(int? id)
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
                string direccion = paciente.Direccion + ", " + paciente.Colonia + ", C.P. " + paciente.CP + ", " + paciente.Ciudad + ", " + paciente.Estado;
                DateTime nacimiento = Convert.ToDateTime(paciente.Nacimiento); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
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
                var font2 = FontFactory.GetFont(coloro, 13, Font.NORMAL, color);
                var fontSpace = FontFactory.GetFont(coloro, 4, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                var tbl = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                Chunk c0111 = new Chunk("\n", fontSpace);
                var tbl2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                tbl.AddCell(new PdfPCell(new Phrase("DATOS DEL PACIENTE: \n\n" +
                    "Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n" +
                    "Nombre: " + nombre + "\n" +
                    "Dirección : " + direccion + "\n" +
                    "Edad: " + edad + "\n" +
                    "Fecha de Nacimiento: " + Convert.ToDateTime(paciente.Nacimiento).ToString("dd-MMMM-yyyy") + "\n", font))
                { BorderWidthRight = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES: \n\n" +
                    "Presión Arterial Sistólica: " + SV.Sistolica + " \n" +
                    "Presión Arterial Diastólica: " + SV.Diastolica + " \n" +
                    "Frecuencia Cardiaca: " + SV.Cardiaca + " \n" +
                    "Frecuencia Respiratoria: " + SV.Respiratoria + " \n" +
                    "Peso: " + SV.Peso + " kgs\n\n", font))
                { BorderWidthRight = 0, BorderWidthLeft = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("\n \n" +
                    "Estatura: " + SV.Estatura + " cms\n" +
                    "IMC: " + SV.IMC + "\n" +
                    "Cuello: " + SV.Cuello + " cms\n" +
                    "Cintura: " + SV.Cintura + " cms\n" +
                    "Porcentaje de Grasa: " + SV.Grasa + "%\n\n", font))
                { BorderWidthLeft = 0 });

                tbl2.AddCell(new PdfPCell(new Phrase("\nPLAN DE TRATAMIENTO: \n \n" +
                    cita.PlanTratamiento + "\n\n", font2)));


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);

                p.Add(tbl);
                p.Add(c0111);
                p.Add(tbl2);

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

        public ActionResult Cer_NU(int? id)
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
                string direccion = paciente.Direccion + ", " + paciente.Colonia + ", C.P. " + paciente.CP + ", " + paciente.Ciudad + ", " + paciente.Estado;
                DateTime nacimiento = Convert.ToDateTime(paciente.Nacimiento); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
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
                var font2 = FontFactory.GetFont(coloro, 13, Font.NORMAL, color);
                var fontSpace = FontFactory.GetFont(coloro, 4, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                var tbl = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                Chunk c0111 = new Chunk("\n", fontSpace);
                var tbl2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                tbl.AddCell(new PdfPCell(new Phrase("DATOS DEL PACIENTE: \n\n" +
                    "Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n" +
                    "Nombre: " + nombre + "\n" +
                    "Dirección : " + direccion + "\n" +
                    "Edad: " + edad + "\n" +
                    "Fecha de Nacimiento: " + Convert.ToDateTime(paciente.Nacimiento).ToString("dd-MMMM-yyyy") + "\n", font))
                { BorderWidthRight = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES: \n\n" +
                    "Presión Arterial Sistólica: " + SV.Sistolica + " \n" +
                    "Presión Arterial Diastólica: " + SV.Diastolica + " \n" +
                    "Frecuencia Cardiaca: " + SV.Cardiaca + " \n" +
                    "Frecuencia Respiratoria: " + SV.Respiratoria + " \n" +
                    "Peso: " + SV.Peso + " kgs\n\n", font))
                { BorderWidthRight = 0, BorderWidthLeft = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("\n \n" +
                    "Estatura: " + SV.Estatura + " cms\n" +
                    "IMC: " + SV.IMC + "\n" +
                    "Cuello: " + SV.Cuello + " cms\n" +
                    "Cintura: " + SV.Cintura + " cms\n" +
                    "Porcentaje de Grasa: " + SV.Grasa + "%\n\n", font))
                { BorderWidthLeft = 0 });

                tbl2.AddCell(new PdfPCell(new Phrase("\nCERTIFICADO MÉDICO: \n \n" +
                    cita.CertificadoMedico + "\n\n", font2)));


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);

                p.Add(tbl);
                p.Add(c0111);
                p.Add(tbl2);

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

        public ActionResult Tra_NU(int? id)
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
                string direccion = paciente.Direccion + ", " + paciente.Colonia + ", C.P. " + paciente.CP + ", " + paciente.Ciudad + ", " + paciente.Estado;
                DateTime nacimiento = Convert.ToDateTime(paciente.Nacimiento); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
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
                var font2 = FontFactory.GetFont(coloro, 13, Font.NORMAL, color);
                var fontSpace = FontFactory.GetFont(coloro, 4, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);

                var tbl = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                Chunk c0111 = new Chunk("\n", fontSpace);
                var tbl2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                tbl.AddCell(new PdfPCell(new Phrase("DATOS DEL PACIENTE: \n\n" +
                    "Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n" +
                    "Nombre: " + nombre + "\n" +
                    "Dirección : " + direccion + "\n" +
                    "Edad: " + edad + "\n" +
                    "Fecha de Nacimiento: " + Convert.ToDateTime(paciente.Nacimiento).ToString("dd-MMMM-yyyy") + "\n", font))
                { BorderWidthRight = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES: \n\n" +
                    "Presión Arterial Sistólica: " + SV.Sistolica + " \n" +
                    "Presión Arterial Diastólica: " + SV.Diastolica + " \n" +
                    "Frecuencia Cardiaca: " + SV.Cardiaca + " \n" +
                    "Frecuencia Respiratoria: " + SV.Respiratoria + " \n" +
                    "Peso: " + SV.Peso + " kgs\n\n", font))
                { BorderWidthRight = 0, BorderWidthLeft = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("\n \n" +
                    "Estatura: " + SV.Estatura + " cms\n" +
                    "IMC: " + SV.IMC + "\n" +
                    "Cuello: " + SV.Cuello + " cms\n" +
                    "Cintura: " + SV.Cintura + " cms\n" +
                    "Porcentaje de Grasa: " + SV.Grasa + "%\n\n", font))
                { BorderWidthLeft = 0 });

                tbl2.AddCell(new PdfPCell(new Phrase("\nPLAN DE TRATAMIENTO: \n \n" +
                    cita.PlanTratamiento + "\n\n", font2)));


                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                doc.Add(PNG1);

                p.Add(tbl);
                p.Add(c0111);
                p.Add(tbl2);

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


        public ActionResult Tra_FA(int? id)
        {

            System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
            System.Console.WriteLine("Se entra al metodo descargar");


            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            requiredPath = requiredPath.Replace("file:\\", "");

            System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
            System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

            var headerFA = requiredPath + ConfigurationManager.AppSettings["FARMACIA"];
            var headerMI = requiredPath + ConfigurationManager.AppSettings["MEDICINAGENERAL"];
            var headerNU = requiredPath + ConfigurationManager.AppSettings["NUTRICION"];
            var headerCA = requiredPath + ConfigurationManager.AppSettings["CARDIOLOGIA"];

            var paciente = (from p in db.PacienteDS where p.idPacienteDS == id orderby p.idPacienteDS descending select p).FirstOrDefault();
            var SV = (from p in db.SignosVitalesDS where p.idPacienteDS == id orderby p.idSignosVitalesDS descending select p).FirstOrDefault();
            var revision1 = (from p in db.Nutriologo where p.idPacienteDS == id orderby p.idNutriologo descending select p).FirstOrDefault();
            var revision2 = (from p in db.Oftalmologo where p.idPacienteDS == id orderby p.idOftalmologo descending select p).FirstOrDefault();
            var revision3 = (from p in db.MedicinaInterna where p.idPacienteDS == id orderby p.idMedicinaInterna descending select p).FirstOrDefault();
            var revision4 = (from p in db.Cardiologo where p.idPacienteDS == id orderby p.idCardiologo descending select p).FirstOrDefault();

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
                string direccion = paciente.Direccion + ", " + paciente.Colonia + ", C.P. " + paciente.CP + ", " + paciente.Ciudad + ", " + paciente.Estado;

                DateTime nacimiento = Convert.ToDateTime(paciente.Nacimiento); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;

                string tratamientoMI = "";
                string tratamientoOF = "";
                string tratamientoCA = "";
                string tratamientoNU = "";

                //Comienza el armado del archivo
                Document doc = new Document(PageSize.A4.Rotate());
                var mem = new MemoryStream();
                iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
                //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
                //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                doc.Open();
                System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
                System.Console.WriteLine("Se prepara para obtener las imagenes");

                iTextSharp.text.Image PNGFA = iTextSharp.text.Image.GetInstance(headerFA);
                iTextSharp.text.Image PNGNU = iTextSharp.text.Image.GetInstance(headerNU);
                iTextSharp.text.Image PNGMI = iTextSharp.text.Image.GetInstance(headerMI);
                iTextSharp.text.Image PNGCA = iTextSharp.text.Image.GetInstance(headerCA);


                PNGFA.Alignment = Image.ALIGN_CENTER;
                PNGFA.ScaleAbsolute(650, 120);

                PNGNU.Alignment = Image.ALIGN_CENTER;
                PNGNU.ScaleAbsolute(650, 120);

                PNGCA.Alignment = Image.ALIGN_CENTER;
                PNGCA.ScaleAbsolute(650, 120);

                PNGMI.Alignment = Image.ALIGN_CENTER;
                PNGMI.ScaleAbsolute(650, 120);

                var color = new iTextSharp.text.BaseColor(128, 128, 128);
                var resultado = new iTextSharp.text.BaseColor(0, 0, 255); //apto
                var resultadoNO = new iTextSharp.text.BaseColor(255, 0, 0); //no apto
                string coloro = "";
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                var font = FontFactory.GetFont(coloro, 9, Font.NORMAL, color);
                var font2 = FontFactory.GetFont(coloro, 11, Font.NORMAL, color);
                var font3 = FontFactory.GetFont(coloro, 13, Font.NORMAL, color);
                var fontSpace = FontFactory.GetFont(coloro, 4, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 9, Font.NORMAL, resultadoNO);


                var tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f };
                Chunk c0111 = new Chunk("\n\n", fontSpace);
                var tbl2 = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f };
                var tbl3 = new PdfPTable(new float[] { 100f}) { WidthPercentage = 100f };

                tbl.AddCell(new PdfPCell(new Phrase("DATOS DEL PACIENTE: \n\n" +
                    "Nombre: " + nombre + "\n" +
                    "Edad: " + edad + "\n" +
                    "Fecha de Nacimiento: " + Convert.ToDateTime(paciente.Nacimiento).ToString("dd-MMMM-yyyy") + "\n\n", font2))
                { BorderWidthRight = 0 });

                tbl.AddCell(new PdfPCell(new Phrase("\n\n" +
                    "Fecha: " + DateTime.Now.ToString("dd-MMMM-yyyy") + "\n" +
                    "Dirección : " + direccion + "\n\n", font2))
                { BorderWidthLeft = 0 });

                tbl2.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES: \n\n" +
                    "Presión Arterial Sistólica: " + SV.Sistolica + " \n" +
                    "Presión Arterial Diastólica: " + SV.Diastolica + " \n" +
                    "Frecuencia Cardiaca: " + SV.Cardiaca + " \n" +
                    "Frecuencia Respiratoria: " + SV.Respiratoria + " \n" +
                    "Peso: " + SV.Peso + " kgs\n\n", font3))
                { BorderWidthRight = 0});

                tbl2.AddCell(new PdfPCell(new Phrase("\n \n" +
                    "Estatura: " + SV.Estatura + " cms\n" +
                    "IMC: " + SV.IMC + "\n" +
                    "Cuello: " + SV.Cuello + " cms\n" +
                    "Cintura: " + SV.Cintura + " cms\n" +
                    "Porcentaje de Grasa: " + SV.Grasa + "%\n\n", font3))
                { BorderWidthLeft = 0 });

                tbl3.AddCell(new PdfPCell(new Phrase("\n \n" , font3))
                { BorderWidth = 0 });

                //if (revision3 != null)
                //{
                //    c100 = new Chunk(revision3.PlanTratamiento, font);
                //}
                //if (revision1 != null)
                //{
                //    c101 = new Chunk(revision1.PlanTratamiento, font);
                //}
                //if (revision2 != null)
                //{
                //    c102 = new Chunk(revision2.PlanTratamiento, font);
                //}
                //if (revision4 != null)
                //{
                //    c103 = new Chunk(revision4.PlanTratamiento, font);
                //}

                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;



                doc.Add(PNGFA);

                doc.Add(tbl);
                doc.Add(tbl3);
                doc.Add(tbl2);

                if(revision3 != null)
                {
                    doc.NewPage();

                    var tblMI = new PdfPTable(new float[] { 100f}) { WidthPercentage = 100f };

                    tblMI.AddCell(new PdfPCell(new Phrase("\nPLAN DE TRATAMIENTO: \n \n" +
                    revision3.PlanTratamiento + "\n\n", font3)));

                    doc.Add(PNGMI);
                    doc.Add(tblMI);
                }

                if(revision1 != null)
                {
                    doc.NewPage();

                    var tblNU = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                    tblNU.AddCell(new PdfPCell(new Phrase("\nPLAN DE TRATAMIENTO: \n \n" +
                    revision1.PlanTratamiento + "\n\n", font3)));

                    doc.Add(PNGNU);
                    doc.Add(tblNU);
                }

                if (revision4 != null)
                {
                    doc.NewPage();

                    var tblCA = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                    tblCA.AddCell(new PdfPCell(new Phrase("\nPLAN DE TRATAMIENTO: \n \n" +
                    revision4.PlanTratamiento + "\n\n", font3)));

                    doc.Add(PNGCA);
                    doc.Add(tblCA);
                }

                //if(revision3 != null)
                //{
                //    p.Add(c100);
                //}
                //if (revision1 != null)
                //{
                //    p.Add(c101);
                //}
                //if (revision2 != null)
                //{
                //    p.Add(c102);
                //}
                //if (revision4 != null)
                //{
                //    p.Add(c103);
                //}

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