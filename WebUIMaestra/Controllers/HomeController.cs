using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebUIMaestra.Helpers;

namespace WebUIMaestra.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IHttpClientFactory factory)
        {
            cliente = factory.CreateClient("Evaluacion");
        }

        HttpClient cliente;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Models.Usuario user)
        {
            user.Clave = Cifrado.GetHash(user.Clave);
            //Uri u = new Uri("https://192.168.1.69:45457/api/Maestro");
            var json = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var result=await cliente.PostAsync(cliente.BaseAddress+"api/Maestro",content);
            if (result.IsSuccessStatusCode)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Nombre));
                claims.Add(new Claim("Id", User.Identity.ToString()));
                var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(new ClaimsPrincipal(identidad));
                return RedirectToAction("Index", "Alumnos", new { area = "Admin" });
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Models.Usuario user)
        {
            user.Clave = Cifrado.GetHash(user.Clave);
            var json = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await cliente.PostAsync("api/Maestro",content);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Su usuario o contraseña es incorrecto");
            return View();          
        }
       

    }
}
