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
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
