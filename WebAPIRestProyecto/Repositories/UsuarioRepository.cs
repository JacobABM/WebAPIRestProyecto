using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIRestProyecto.Models;

namespace WebAPIRestProyecto.Repositories
{
    public class UsuarioRepository:Repository<WebAPIRestProyecto.Models.Usuario>
    {

        public UsuarioRepository(evaluacionalumnosContext ctx):base(ctx)
        {

        }
    }
}
