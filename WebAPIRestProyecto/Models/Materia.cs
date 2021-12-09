using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPIRestProyecto.Models
{
    public partial class Materia
    {
        public Materia()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        public int Id { get; set; }
        public string NombreMateria { get; set; }

        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
