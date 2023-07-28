using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using G_CustomeIdentity.Models;
using G_CustomeIdentity.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace G_CustomeIdentity.Repositories.Implementation
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAuthentication(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Status> LoginAsync(Login login)
        {
            //invoke status
           var status = new Status();
           //find username supplied by user
           var user = await _userManager.FindByNameAsync(login.Username);
        //    if the username supplied is not in the database table return error
           if(user==null){
                status.StatusCode = 0;
                status.Message = "User not found!";
                return status;
           }
           //check user password, if it matches the password in the database
           if(!await _userManager.CheckPasswordAsync(user, login.Password)){
            status.StatusCode = 0;
            status.Message = "Invalid Password!";
            return status;
           }
           //sign in user if password is true
           var signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, false, true );
           if(signInResult.Succeeded)
           {
            // if user is logged in get the user role
                var userRoles = await _userManager.GetRolesAsync(user);
                var authclaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach(var userRole in userRoles)
                {
                    authclaims.Add(new Claim(ClaimTypes.Role,userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully!";
                return status;
           }
           else if(signInResult.IsLockedOut)
           {
            status.StatusCode = 0;
            status.Message =  "User locked out!";
            return status;
           }
           else 
           {
             status.StatusCode = 0;
             status.Message = "Error logging in!";
             return status;
           }

        }

        public async Task<Status> LogoutAsync()
        {
             await _signInManager.SignOutAsync();
            Status status = new()
            {
                StatusCode = 1,
                Message = "You have logged out of the system!"
            };
            return status;
        }

       public async Task<Status> RegistrationAsync(Registration registration)
        {
            //invoke the status model so as to use its properties
          var status = new Status();
        //   check if user eixsts in the database
          var userExists = await _userManager.FindByNameAsync(registration.Username);
        //   if user exists in the database return status
          if(userExists != null){
                status.Message = "User already exists";
                return status;
          }
        //  if user doesnt exist. get user inputs 
            ApplicationUser user = new()
            {
                  SecurityStamp = Guid.NewGuid().ToString(), // generate hashed id
                  Name = registration.Name,
                  Email = registration.Email,
                  UserName = registration.Username,
                  EmailConfirmed = true
            }; 
            //  create new user  
              var result = await _userManager.CreateAsync(user, registration.Password);
            //   if result is false. if user is not created
              if(!result.Succeeded){
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;

              } 
              //role managment
              //check if user role exists
              if(!await _roleManager.RoleExistsAsync(registration.Role)){
                // if user role does not exists create new identity role 
                await _roleManager.CreateAsync(new IdentityRole(registration.Role));
              }
            //   if the role is been created, assign the role to user
              if(await  _roleManager.RoleExistsAsync(registration.Role)){
                await _userManager.AddToRoleAsync(user, registration.Role);
              }
            //   return status true if above is true 
              status.StatusCode = 1;
              status.Message = "You have successfully registred!";
              return status;
        }
    }
}