using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GisButtons.Controllers
{
    public class WelcomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            
            return View();
        }
    }
}
