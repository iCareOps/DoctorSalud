using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return RedirectToAction("Index", "Modulos", new { id = id });
        }

    }
}