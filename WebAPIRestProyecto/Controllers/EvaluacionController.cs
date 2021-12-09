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
    public class EvaluacionController : ControllerBase
    {
        public EvaluacionController(evaluacionalumnosContext context)
        {
            Context = context;
        }

        public evaluacionalumnosContext Context { get; }
        //verificar que la materia y el alumno estén en los registros
        //Actualizar un registro o crear si no había uno antes de una calificacion de una materia
        //si el registro existe para ese alumno y para esa materia -> entonces actualizalo
        //no existe ->crealo e inseta los datos
        //Materia, alumno y las califiaciones -> las calificaciones están en cero al comenzar el regsitro
        //si se deja un espacio sin rellenar se marca el cero.
        //public IEnumerable<Evaluacion> Get(int? id)
        //{
        //    UsuarioRepository repo = new(Context);
        //}
        public IActionResult Get()
        {
            EvaluarRepository ev = new EvaluarRepository(Context);
            var evaluaciones=(IEnumerable<Evaluacion>)ev.GetAll().Select(x => x).ToList();
            if (evaluaciones!=null)
            {
                return Ok(evaluaciones);
            }
           return NotFound("No hay registros disponibles");          
        }
        [HttpPost]
        public IActionResult Post(Evaluacion param)
        {
            if (param!=null)
            {
                EvaluarRepository ev = new EvaluarRepository(Context);
                var evaluacion = ev.GetAll().Select(x => x).FirstOrDefault(x => x.IdAlumno == param.IdAlumno && x.IdMateria == param.IdMateria);
                if (evaluacion!=null)
                {
                    evaluacion.PrimeraEvaluacion = param.PrimeraEvaluacion;
                    evaluacion.SegundaEvaluacion = param.SegundaEvaluacion;
                    evaluacion.TerceraEvaluacion = param.TerceraEvaluacion;
                    ev.Update(evaluacion);
                    return Ok();
                }
                else
                {
                    Evaluacion nuevaev = new();
                    nuevaev.PrimeraEvaluacion = param.PrimeraEvaluacion;
                    nuevaev.SegundaEvaluacion = param.SegundaEvaluacion;
                    nuevaev.TerceraEvaluacion = param.TerceraEvaluacion;
                    ev.Insert(nuevaev);
                    return Ok();
                }
            }
            return NotFound("La evaluación se dejó vacío");
        }
        [HttpPut]
        public IActionResult Put(Evaluacion evp)
        {
            if (evp != null)
            {
                EvaluarRepository ev = new EvaluarRepository(Context);
                var evaluacion = ev.GetAll().Select(x => x).FirstOrDefault(x => x.IdAlumno == evp.IdAlumno && x.IdMateria == evp.IdMateria);
                if (evaluacion==null)
                {
                    ModelState.AddModelError("", "No se encuentra en los registros o ha sido eliminada");
                }         
                    if (ModelState.IsValid)
                    {
                        evaluacion.PrimeraEvaluacion = evp.PrimeraEvaluacion;
                        evaluacion.SegundaEvaluacion = evp.SegundaEvaluacion;
                        evaluacion.TerceraEvaluacion = evp.TerceraEvaluacion;
                        ev.Update(evaluacion);
                        return Ok();
                    }                                   
            }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete(Evaluacion evparametro)
        {
            EvaluarRepository ev = new EvaluarRepository(Context);
            var evdb = ev.Get(evparametro);
            if (evdb!=null)
            {
                ev.Remove(evdb);
                ev.Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("La evaluación no puede eliminarse ya que no existe");
            }
        }


    }
}
