using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebUIMaestra.Models;

namespace WebUIMaestra.Areas.Admin.Models
{
    [Area("Admin")]
    [Authorize]
    public class AlumnosController : Controller
    {
        public AlumnosController(IHttpClientFactory factory)
        {
            cliente = factory.CreateClient("Evaluacion");
        }
        HttpClient cliente;
        [HttpGet("Admin/Alumnos/")]
        [HttpGet("Admin/Alumnos/Index")]
        public async Task<IActionResult> Index()
        {
            var result =await cliente.GetAsync(cliente.BaseAddress+"api/Alumno/");
            var array = await result.Content.ReadAsByteArrayAsync();
            var json = Encoding.UTF8.GetString(array);
            var listaUsuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(json);         
            return View(listaUsuarios);
        }
        public IActionResult AgregarAlumno()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AgregarAlumno(Usuario usuario)
        {
            if (usuario != null)
            {
                usuario.Rol = "A";
                var json = JsonConvert.SerializeObject(usuario);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await cliente.PostAsync("api/Maestro", content);
            }
            return RedirectToAction("Index");
        }
        [HttpGet("Admin/Alumnos/EditarAlumno/{id}")]
        public async Task<IActionResult> EditarAlumno(int id)
        {

            var result=await cliente.GetAsync($"api/Maestro/{id}");
            var contenido =await result.Content.ReadAsByteArrayAsync();
            var usuario = JsonConvert.DeserializeObject<Usuario>(Encoding.UTF8.GetString(contenido));         
            return View(usuario);
        }
        //[HttpPost]
        //public async Task<IActionResult> EditarAlumno(WebUIMaestra.Models.Usuario usuario)
        //{


        //}
        [HttpGet("Admin/Alumnos/EliminarAlumno/{id}")]
        public async Task<IActionResult> EliminarAlumno(int id)
        {

            var result = await cliente.GetAsync($"api/Maestro/{id}");
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "No se encontró o no existe el alumno");
            return View();
        }
        [HttpPost("Admin/Alumnos/EliminarAlumno")]
        public async Task<IActionResult> EliminarAlumno(WebUIMaestra.Models.Usuario usuario)
        {
            var result = await cliente.DeleteAsync($"api/Maestro/{usuario.Id}");
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "No existe el registro o ha sido eliminado.");
            return View();       
        }
    }
}
