//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorSalud
{
    using System;
    using System.Collections.Generic;
    
    public partial class CitaDS
    {
        public int idCitaDS { get; set; }
        public Nullable<System.DateTime> FechaCita { get; set; }
        public string EstatusPago { get; set; }
        public string Estado { get; set; }
        public string Recepcionista { get; set; }
        public Nullable<System.DateTime> FechaPago { get; set; }
        public Nullable<int> idPacienteDS { get; set; }
        public string Sucursal { get; set; }
        public Nullable<System.DateTime> Inicio_Oftalmologia { get; set; }
        public Nullable<System.DateTime> Fin_Oftalmologia { get; set; }
        public Nullable<System.DateTime> Inicio_MedicinaInterna { get; set; }
        public Nullable<System.DateTime> Fin_MedicinaInterna { get; set; }
        public Nullable<System.DateTime> Inicio_Cardiologia { get; set; }
        public Nullable<System.DateTime> Fin_Cardiologia { get; set; }
        public Nullable<System.DateTime> Inicio_Nutriciom { get; set; }
        public Nullable<System.DateTime> Fin_Nutricion { get; set; }
        public string Membresia { get; set; }
        public Nullable<System.DateTime> Inicio_SignosVitales { get; set; }
        public Nullable<System.DateTime> Fin_SignosVitales { get; set; }
        public string NoMembresia { get; set; }
    
        public virtual PacienteDS PacienteDS { get; set; }
    }
}
