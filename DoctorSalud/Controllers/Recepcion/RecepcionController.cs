using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorSalud.Controllers.Recepcion
{
    public class RecepcionController : Controller
    {
        DoctorSalud_Entities db = new DoctorSalud_Entities();
        // GET: Recepcion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Farmacia()
        {
            return View();
        }

        public ActionResult Dashboard(DateTime? inicio, DateTime? final)
        {
            DateTime thisDate = new DateTime();
            DateTime tomorrowDate = new DateTime();

            DateTime start1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime finish1 = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);

            int nulos = 0;

            if (inicio != null || final != null)
            {
                nulos = 1;
            }

            if (inicio != null)
            {
                DateTime start = Convert.ToDateTime(inicio);
                int year = start.Year;
                int month = start.Month;
                int day = start.Day;

                inicio = new DateTime(year, month, day);
                thisDate = new DateTime(year, month, day);
            }
            if (final != null)
            {
                DateTime finish = Convert.ToDateTime(final).AddDays(1);
                int year = finish.Year;
                int month = finish.Month;
                int day = finish.Day;

                final = new DateTime(year, month, day);
                tomorrowDate = new DateTime(year, month, day);
            }

            inicio = (inicio ?? start1);
            final = (final ?? finish1);

            ViewBag.Inicio = inicio;
            ViewBag.Final = final;
            ViewBag.Estado = nulos;

            ViewBag.Parameter = "";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(string nombre, string telefono, string email, string usuario, string sucursal, string hash,
            string calle, string colonia, string cp, string ciudad, string estado, DateTime nacimiento, string referido)
        {

            PacienteDS paciente = new PacienteDS();
            paciente.Nombre = nombre.ToUpper();
            paciente.Telefono = telefono;
            paciente.Email = email;
            paciente.Direccion = calle.ToUpper();
            paciente.Colonia = colonia.ToUpper();
            paciente.CP = cp;
            paciente.Ciudad = ciudad.ToUpper();
            paciente.Estado = estado.ToUpper();
            paciente.Nacimiento = nacimiento;


            string mes = DateTime.Now.Month.ToString();
            string dia = DateTime.Now.Day.ToString();
            char[] year = (DateTime.Now.Year.ToString()).ToCharArray();
            string anio = "";

            paciente.HASH = hash;

            for (int i = 2; i < year.Length; i++)
            {
                anio += year[i];
            }

            if (Convert.ToInt32(mes) < 10)
            {
                mes = "0" + mes;
            }

            if (Convert.ToInt32(dia) < 10)
            {
                dia = "0" + dia;
            }

            if (ModelState.IsValid)
            {
                db.PacienteDS.Add(paciente);
                db.SaveChanges();
            }

            //var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

            CitaDS cita = new CitaDS();

            cita.idPacienteDS = paciente.idPacienteDS;
            cita.Sucursal = sucursal;
            cita.FechaCita = DateTime.Now;
            cita.Recepcionista = usuario;
            cita.EstatusPago = "Pendiente";
            cita.ReferidoPor = referido.ToUpper();
            //cita.Estado = estado.ToUpper();

            //-------------------------------------------------------------
            if (ModelState.IsValid)
            {
                db.CitaDS.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Redirect("Index"); ;
        }


        public ActionResult Editar(int id, string nombre, string email, string telefono, string hash, string membresia,
            string calle, string colonia, string cp, string ciudad, string estado, DateTime? nacimiento)
        {
            var paciente = db.PacienteDS.Find(id);

            paciente.Nombre = nombre == "" ? paciente.Nombre : nombre.ToUpper();
            paciente.Email = email == "" ? paciente.Email : email;
            paciente.Telefono = telefono == "" ? paciente.Telefono : telefono;
            paciente.Direccion = calle == "" ? paciente.Direccion : calle.ToUpper();
            paciente.Colonia = colonia == "" ? paciente.Colonia : colonia.ToUpper();
            paciente.CP = cp == "" ? paciente.CP : cp;
            paciente.Ciudad = ciudad == "" ? paciente.Ciudad : ciudad.ToUpper();
            paciente.Estado = estado == "" ? paciente.Estado : estado.ToUpper();
            paciente.Nacimiento = nacimiento == null ? paciente.Nacimiento : nacimiento;

            var cita = (from c in db.CitaDS where c.idPacienteDS == id select c).FirstOrDefault();
            cita.NoMembresia = membresia == "" ? cita.NoMembresia : membresia;

            paciente.HASH = hash == "" ? paciente.HASH : hash;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }
 
            return Redirect("Index");
        }


        public ActionResult Pagado(int id, string mi, string ofta, string cardio, string nutri, string membresia)
        {
            var paciente = db.PacienteDS.Find(id);
            var cita = (from c in db.CitaDS where c.idPacienteDS == id select c).FirstOrDefault();

            var oftalmo = (from o in db.Oftalmologo where o.idPacienteDS == id select o).FirstOrDefault();
            var cardiolo = (from o in db.Cardiologo where o.idPacienteDS == id select o).FirstOrDefault();
            var nutriolo = (from o in db.Nutriologo where o.idPacienteDS == id select o).FirstOrDefault();
            var medicina = (from o in db.MedicinaInterna where o.idPacienteDS == id select o).FirstOrDefault();
            var signos = (from o in db.SignosVitalesDS where o.idPacienteDS == id select o).FirstOrDefault();

            cita.EstatusPago = "Pagado";
            cita.NoMembresia = membresia == "" ? cita.NoMembresia : membresia;

            SignosVitalesDS signosV = new SignosVitalesDS();
            signosV.idPacienteDS = id;
            if(signos == null)
            {
                if (ModelState.IsValid)
                {
                    db.SignosVitalesDS.Add(signosV);
                    db.SaveChanges();
                }
            }

            
            if (ofta != null && oftalmo == null)
            {
                Oftalmologo oftalmologo = new Oftalmologo();
                oftalmologo.idPacienteDS = id;

                if (ModelState.IsValid)
                {
                    db.Oftalmologo.Add(oftalmologo);
                    db.SaveChanges();
                }
            }
            else if(ofta == null && oftalmo != null)
            {
                var ide = (from i in db.Oftalmologo where i.idPacienteDS == id orderby i.idOftalmologo descending select i.idOftalmologo).FirstOrDefault();
                Oftalmologo oftalmologo = db.Oftalmologo.Find(ide);
                cita.Inicio_Oftalmologia = null;

                if (ModelState.IsValid)
                {
                    db.Oftalmologo.Remove(oftalmologo);
                    db.SaveChanges();
                }  
            }

            if (cardio != null && cardiolo == null)
            {
                Cardiologo cardiologo = new Cardiologo();
                cardiologo.idPacienteDS = id;

                if (ModelState.IsValid)
                {
                    db.Cardiologo.Add(cardiologo);
                    db.SaveChanges();
                }
            }
            else if (cardio == null && cardiolo != null)
            {
                var ide = (from i in db.Cardiologo where i.idPacienteDS == id orderby i.idCardiologo descending select i.idCardiologo).FirstOrDefault();
                Cardiologo cardiologo = db.Cardiologo.Find(ide);
                cita.Inicio_Cardiologia = null;

                if (ModelState.IsValid)
                {
                    db.Cardiologo.Remove(cardiologo);
                    db.SaveChanges();
                }
            }

            if (mi != null && medicina == null)
            {
                MedicinaInterna medicinaInterna = new MedicinaInterna();
                medicinaInterna.idPacienteDS = id;

                if (ModelState.IsValid)
                {
                    db.MedicinaInterna.Add(medicinaInterna);
                    db.SaveChanges();
                }
            }
            else if (mi == null && medicina != null)
            {
                var ide = (from i in db.MedicinaInterna where i.idPacienteDS == id orderby i.idMedicinaInterna descending select i.idMedicinaInterna).FirstOrDefault();
                MedicinaInterna medicinaInterna = db.MedicinaInterna.Find(ide);
                cita.Inicio_MedicinaInterna = null;

                if (ModelState.IsValid)
                {
                    db.MedicinaInterna.Remove(medicinaInterna);
                    db.SaveChanges();
                }
            }

            if (nutri != null && nutriolo == null)
            {
                Nutriologo nutriologo = new Nutriologo();
                nutriologo.idPacienteDS = id;

                if (ModelState.IsValid)
                {
                    db.Nutriologo.Add(nutriologo);
                    db.SaveChanges();
                }
            }
            else if (nutri == null && nutriolo != null)
            {
                var ide = (from i in db.Nutriologo where i.idPacienteDS == id orderby i.idNutriologo descending select i.idNutriologo).FirstOrDefault();
                Nutriologo nutriologo = db.Nutriologo.Find(ide);
                cita.Inicio_Nutriciom = null;

                if (ModelState.IsValid)
                {
                    db.Nutriologo.Remove(nutriologo);
                    db.SaveChanges();
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("Index");
        }

        public ActionResult PagadoMedicamento(int id, string mi, string ofta, string cardio, string nutri)
        {
            var paciente = db.PacienteDS.Find(id);
            var cita = (from c in db.CitaDS where c.idPacienteDS == id select c).FirstOrDefault();

            var oftalmo = (from o in db.Oftalmologo where o.idPacienteDS == id orderby o.idOftalmologo descending select o).FirstOrDefault();
            var cardiolo = (from o in db.Cardiologo where o.idPacienteDS == id orderby o.idCardiologo descending select o).FirstOrDefault();
            var nutriolo = (from o in db.Nutriologo where o.idPacienteDS == id orderby o.idNutriologo descending select o).FirstOrDefault();
            var medicina = (from o in db.MedicinaInterna where o.idPacienteDS == id orderby o.idMedicinaInterna descending select o).FirstOrDefault();


            if (ofta == "on" && oftalmo.Medicamento == null)
            {
                Oftalmologo oftalmologo = db.Oftalmologo.Find(oftalmo.idOftalmologo);
                oftalmologo.Medicamento = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(oftalmologo).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (cardio == "on" && cardiolo.Medicamento == null)
            {
                Cardiologo cardiologo = db.Cardiologo.Find(cardiolo.idCardiologo);
                cardiologo.Medicamento = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cardiologo).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (mi == "on" && medicina.Medicamento == null)
            {
                MedicinaInterna medicinaInterna = db.MedicinaInterna.Find(medicina.idMedicinaInterna);
                medicinaInterna.Medicamento = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(medicinaInterna).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (nutri == "on" && nutriolo.Medicamento == null)
            {
                Nutriologo nutriologo = db.Nutriologo.Find(nutriolo.idNutriologo);
                nutriologo.Medicamento = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(nutriologo).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Redirect("Farmacia");
        }

        public JsonResult BuscarTodo(string dato)
        {
            var selected = (from i in db.Paciente where i.HASH == dato select i).Select(s => new { s.Nombre, s.Telefono, s.Email }).FirstOrDefault();

            if(selected == null)
            {
                var nulo = "nulo";

                return Json(nulo, JsonRequestBehavior.AllowGet);
            }

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

    }
}