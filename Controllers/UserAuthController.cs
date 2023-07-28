using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G_CustomeIdentity.Models;
using G_CustomeIdentity.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace  G_CustomeIdentity.Controllers
{

    public class UserAuthController : Controller
    {
        private readonly IUserAuthentication _auth;
        public UserAuthController(IUserAuthentication auth)
        {
            _auth = auth;
        }   
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if(ModelState.IsValid)
            {
                var result = await _auth.LoginAsync(login);
                if(result.StatusCode == 1){
                     ViewData["success"] = result.Message;
                     return RedirectToAction("Index", "Dashboard");
                     
                }else{
                    ViewData["error"] = result.Message;
                    return RedirectToAction("Login");
                }

            }else {
                return View();
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout(){
           await _auth.LogoutAsync();
           TempData["error"] = "You have logged out!";
           return  RedirectToAction(nameof(Login));
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            if(ModelState.IsValid){
                registration.Role = "user";
                var result = await _auth.RegistrationAsync(registration);
                if(result.StatusCode == 1)
                {
                    ViewData["success"] = result.Message;
                    return RedirectToAction("Login");
                }else{
                    ViewData["error"] = result.Message;
                    return RedirectToAction("Register");
                }
            }else{
                return View(registration);
            }
        }


    }
}