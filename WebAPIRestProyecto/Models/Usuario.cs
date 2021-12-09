using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPIRestProyecto.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }

        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
