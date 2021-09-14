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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(string nombre, string telefono, string email, string usuario, string sucursal, string hash)
        {

            PacienteDS paciente = new PacienteDS();
            paciente.Nombre = nombre.ToUpper();
            paciente.Telefono = telefono;
            paciente.Email = email;


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


        public ActionResult Editar(int id, string nombre, string email, string telefono, string hash)
        {
            var paciente = db.PacienteDS.Find(id);

            paciente.Nombre = nombre == "" ? paciente.Nombre : nombre;
            paciente.Email = email == "" ? paciente.Email : email;
            paciente.Telefono = telefono == "" ? paciente.Telefono : telefono;

            var cita = (from c in db.CitaDS where c.idPacienteDS == id select c).FirstOrDefault();

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
            cita.Membresia = membresia == "on" ? "SI" : null;

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

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
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

    }
}