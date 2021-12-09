using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIRestProyecto.Models;

namespace WebAPIRestProyecto.Repositories
{
    public class EvaluarRepository: Repository<WebAPIRestProyecto.Models.Evaluacion>
    {
        public EvaluarRepository(evaluacionalumnosContext ctx) : base(ctx)
        {

        }
    }
}
