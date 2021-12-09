using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIMaestra.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EvaluacionController : Controller
    {
        //Mostrar las evaluaciones
        public IActionResult Index()
        { 
            return View();
        }
    }
}
