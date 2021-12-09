using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIMaestra.Models
{
    public class Usuario
    {
  
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
