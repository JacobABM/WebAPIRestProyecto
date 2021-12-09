using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIMaestra.Models
{
    public class Materia
    {

        public int Id { get; set; }
        public string NombreMateria { get; set; }

        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
