using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace G_CustomeIdentity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name {get; set;} = string.Empty;
        public string Profile_photo {get; set;} = string.Empty;
    }
}