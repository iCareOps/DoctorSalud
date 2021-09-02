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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(string nombre, string telefono, string email, string usuario, string sucursal, string estado)
        {

            PacienteDS paciente = new PacienteDS();
            paciente.Nombre = nombre.ToUpper();
            paciente.Telefono = telefono;
            paciente.Email = email;

            //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
            //string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();

            //Se obtiene el número del contador desde la base de datos
            //int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

            //Contadores por número de citas en cada sucursal
            //string contador = "";
            //if (num == null)
            //{
            //    contador = "100";
            //}
            //else if (num < 10)
            //{
            //    contador = "00" + Convert.ToString(num);
            //}
            //else if (num >= 10 && num < 100)
            //{
            //    contador = "0" + Convert.ToString(num);
            //}

            string mes = DateTime.Now.Month.ToString();
            string dia = DateTime.Now.Day.ToString();
            char[] year = (DateTime.Now.Year.ToString()).ToCharArray();
            string anio = "";

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

            //string numFolio = dia + mes + anio + SUC + "-" + contador;
            //paciente.Folio = dia + mes + anio + SUC + "-" + contador;

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
            cita.Estado = estado.ToUpper();

            //-------------------------------------------------------------
            if (ModelState.IsValid)
            {
                db.CitaDS.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Redirect("Index"); ;
        }


        public ActionResult Editar(int id, string nombre, string email, string telefono, string estado)
        {
            var paciente = db.PacienteDS.Find(id);

            paciente.Nombre = nombre == "" ? paciente.Nombre : nombre;
            paciente.Email = email == "" ? paciente.Email : email;
            paciente.Telefono = telefono == "" ? paciente.Telefono : telefono;

            var cita = (from c in db.CitaDS where c.idPacienteDS == id select c).FirstOrDefault();

            cita.Estado = estado == "" ? cita.Estado : estado.ToUpper();

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

            var oftalmo = (from o in db.Oftalmologo where o.idPacienteDS == id select o).FirstOrDefault();
            var cardiolo = (from o in db.Cardiologo where o.idPacienteDS == id select o).FirstOrDefault();
            var nutriolo = (from o in db.Nutriologo where o.idPacienteDS == id select o).FirstOrDefault();
            var medicina = (from o in db.MedicinaInterna where o.idPacienteDS == id select o).FirstOrDefault();

            if(ofta != null && oftalmo == null)
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


    }
}