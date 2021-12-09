using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestProyecto.Models;
using WebAPIRestProyecto.Repositories;

namespace WebAPIRestProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        //Aplicación Xamarin
        public AlumnoController(evaluacionalumnosContext context)
        {
            Context = context;
        }

        public evaluacionalumnosContext Context { get; }
        public IActionResult Get()
        {
            UsuarioRepository us = new(Context);
            return Ok(us.GetAll().Where(x=>x.Rol=="A"));
        }
        //se debe de hacer el inicio de sesion en xamarin verificando usuario y contraseña para poder entregar una evaluación
        //[HttpGet("{id}")]
        //public IActionResult Get(string id)
        //{         
        //        EvaluarRepository ev = new EvaluarRepository(Context);
        //        var usuario = ev.GetAll().Select(x=>x).Where(x=>x.IdAlumno==);
        //        if (usuario == null)
        //        {
        //            return Conflict("Se generó un error, no hay registros disponibles");
        //        }
        //        return Ok(usuario);        
        //}
        [HttpPost]
        public IActionResult Post(Usuario user)
        {
            UsuarioRepository repository = new(Context);
            if (string.IsNullOrWhiteSpace(user.Nombre))
            {
                return BadRequest("Nombre de usuario vacío, Especifique el nombre.");
            }
            if (string.IsNullOrWhiteSpace(user.Clave))
            {
                return BadRequest("Clave de usuario vacío, Especifique la contraseña.");
            }
            var usuarioDb = repository.Get(user);
            if (usuarioDb==null)
            {
                return NotFound();
            }      
            else if(usuarioDb!=null&&usuarioDb.Rol != "A")
            {
                return Conflict("Rol no adecuado");
            }
            else if (usuarioDb.Clave!=user.Clave||usuarioDb.Nombre != user.Nombre)
            {
                return Conflict("Usuario o contraseña incorrectos");
            }
            else
            {
                var evaluaciones = repository.GetEv(usuarioDb);
                var json = JsonConvert.SerializeObject(evaluaciones);
                Console.WriteLine(json);
                return Ok(evaluaciones);
            }
        }
    }
}
