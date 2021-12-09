using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIMaestra.Models
{
    public class Evaluacion
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
