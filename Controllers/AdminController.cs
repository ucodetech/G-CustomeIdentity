using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace G_CustomeIdentity.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}