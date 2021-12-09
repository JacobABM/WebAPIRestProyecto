using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIRestProyecto.Models;
using WebAPIRestProyecto.Repositories;

namespace WebAPIRestProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaestroController : ControllerBase
    {
        //accesar
        public MaestroController(evaluacionalumnosContext context)
        {
            Context = context;
        }

        public evaluacionalumnosContext Context { get; }

        public IActionResult Get(Usuario usuario)
        {
            UsuarioRepository us = new(Context);
            var userdb = us.Get(usuario);
            if (userdb!=null)
            {
                return Ok(userdb);
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            //Autentificar
            UsuarioRepository us = new(Context);
            if (us.GetAll().Any(x=>x.Nombre==usuario.Nombre&&x.Clave==usuario.Clave))
            {
                return Ok();
            }
            
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Put(Usuario usuario)
        {
            UsuarioRepository us = new(Context);
            var usuariodb = us.Get(usuario.Id);
            if (usuariodb!=null)
            {
                usuariodb.Nombre = usuario.Nombre;
                usuariodb.Clave = usuario.Clave;
                Context.Update(usuariodb);
                Context.SaveChanges();
            }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete(Usuario usuario)
        {
            UsuarioRepository us = new(Context);
            var usuarioaux = us.Get(usuario);
            if (usuarioaux!=null)
            {
                Context.Remove(usuarioaux);
                Context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

    }
}
