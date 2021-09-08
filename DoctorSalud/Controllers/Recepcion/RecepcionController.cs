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

            //string hash;
            //do
            //{
            //    Random numero = new Random();
            //    int randomize = numero.Next(0, 61);
            //    string[] aleatorio = new string[62] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            //    string get_1;
            //    get_1 = aleatorio[randomize];
            //    hash = get_1;
            //    for (int i = 0; i < 9; i++)
            //    {
            //        randomize = numero.Next(0, 61);
            //        get_1 = aleatorio[randomize];
            //        hash += get_1;
            //    }
            //} while ((from i in db.PacienteDS where i.HASH == hash select i) == null);

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

            cita.EstatusPago = "Pagado";
            cita.Membresia = membresia == "on" ? "SI" : null;

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