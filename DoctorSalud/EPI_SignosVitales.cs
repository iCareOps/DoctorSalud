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
    
    public partial class EPI_SignosVitales
    {
        public int idSignosVitales { get; set; }
        public string Sistolica { get; set; }
        public string Diastolica { get; set; }
        public string Cardiaca { get; set; }
        public string Respiratoria { get; set; }
        public string Temperatura { get; set; }
        public string Peso { get; set; }
        public string Estatura { get; set; }
        public string IMC { get; set; }
        public string Cintura { get; set; }
        public string Cuello { get; set; }
        public string Grasa { get; set; }
        public Nullable<int> idPaciente { get; set; }
    
        public virtual Paciente Paciente { get; set; }
    }
}
