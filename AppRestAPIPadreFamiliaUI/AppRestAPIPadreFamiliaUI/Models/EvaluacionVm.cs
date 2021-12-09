using System;
using System.Collections.Generic;
using System.Text;

namespace AppRestAPIPadreFamiliaUI.Models
{
   public class EvaluacionVm
    {
        public int Id { get; set; }
        public decimal PrimeraEvaluacion { get; set; }
        public decimal SegundaEvaluacion { get; set; }
        public decimal TerceraEvaluacion { get; set; }
        public string Materia { get; set; }
        public string Alumno { get; set; }
    }
}
