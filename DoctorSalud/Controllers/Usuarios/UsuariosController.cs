using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorSalud.Controllers.Usuarios
{
    public class UsuariosController : Controller
    {
        DoctorSalud_Entities db = new DoctorSalud_Entities();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Modulos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearUsuario(string nombre, string email, string password, string rol)
        {
            DoctorSalud.Usuarios usuarios = new DoctorSalud.Usuarios();

            usuarios.Email = email;
            usuarios.Nombre = nombre;

            int puesto = (from p in db.Roles where p.Nombre == rol select p.idRol).FirstOrDefault();

            usuarios.idRol = puesto;

            if (ModelState.IsValid)
            {
                usuarios.Password = Encrypt.GetSHA256(password);

                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult EditarUsuario(string nombre, string email, string password, string rol, int? id)
        {
            DoctorSalud.Usuarios usuarios = db.Usuarios.Find(id);


            if (nombre != "")
            {
                usuarios.Nombre = nombre;
            }
            else
            {
                usuarios.Nombre = usuarios.Nombre;
            }

            if (email != "")
            {
                usuarios.Email = email;
            }
            else
            {
                usuarios.Email = usuarios.Email;
            }
            int puesto = (from p in db.Roles where p.Nombre == rol select p.idRol).FirstOrDefault();

            usuarios.idRol = puesto;

            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult EliminarUsuario(int? id)
        {
            DoctorSalud.Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarSucursal(int id, int sucursal)
        {
            var recepcion = (from r in db.RecepcionistaDS where r.idUsuario == id select r).FirstOrDefault();
            var idSucursal = (from i in db.SucursalDS where i.idSucursalDS == sucursal select i.idSucursalDS).FirstOrDefault();

            if (recepcion != null)
            {
                RecepcionistaDS recepcionista = db.RecepcionistaDS.Find(id);
                recepcionista.idUsuario = id;
                recepcionista.idSucursalDS = idSucursal;

                if (ModelState.IsValid)
                {
                    db.Entry(recepcionista).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    RecepcionistaDS recepcionista = new RecepcionistaDS();
                    recepcionista.idUsuario = id;
                    recepcionista.idSucursalDS = idSucursal;

                    db.RecepcionistaDS.Add(recepcionista);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AsignarModulo(int modulo, int sucursal, int doctor)
        {
            DoctorModuloDS dm = new DoctorModuloDS();

            dm.idUsuario = doctor;
            dm.idModulo = modulo;
            dm.idSucursalDS = sucursal;

            if (ModelState.IsValid)
            {
                db.DoctorModuloDS.Add(dm);
                db.SaveChanges();
            }

            return Redirect("Modulos");
        }


    }
}