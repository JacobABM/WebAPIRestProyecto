using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPIRestProyecto.Models
{
    public partial class Evaluacion
    {
        public int Id { get; set; }
        public decimal PrimeraEvaluacion { get; set; }
        public decimal SegundaEvaluacion { get; set; }
        public decimal TerceraEvaluacion { get; set; }
        public int IdMateria { get; set; }
        public int IdAlumno { get; set; }

        public virtual Usuario IdAlumnoNavigation { get; set; }
        public virtual Materia IdMateriaNavigation { get; set; }
    }
}
