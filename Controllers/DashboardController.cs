using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace G_CustomeIdentity.Controllers
{
    [Authorize(Roles="user")]
    public class DashboardController : Controller
    {
       
        public IActionResult Index()
        {
            
            return View();
        }
    }
}