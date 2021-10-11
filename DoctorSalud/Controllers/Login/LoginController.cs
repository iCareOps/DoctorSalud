﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorSalud.Controllers.Login
{
    public class LoginController : Controller
    {
        private DoctorSalud_Entities db = new DoctorSalud_Entities();

        // GET: Login
        public ActionResult Inicio()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Inicio(string User, string Pass)
        {

            try
            {

                Pass = Encrypt.GetSHA256(Pass.Trim());
                var oUser = (from d in db.Usuarios where d.Email == User && d.Password == Pass.Trim() select d).FirstOrDefault();

                if (oUser == null)
                {
                    ViewBag.Error = "Usuario o Contraseña inválida";
                    return View();
                }
                else
                {
                    Session["User"] = oUser;

                    switch (oUser.idRol)
                    {
                        case 14:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Recepcion/Index");
                        case 15:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Recepcion/Inicio");
                        case 16:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Recepcion/Farmacia");
                        case 18:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Recepcion/Dashboard");
                        case 19:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Recepcion/Inicio");
                        default:
                            return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult Redireccionar()
        {
            return RedirectToAction("Inicio");
        }
    }
}