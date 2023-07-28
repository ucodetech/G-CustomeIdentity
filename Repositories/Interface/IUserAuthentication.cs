using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G_CustomeIdentity.Models;

namespace G_CustomeIdentity.Repositories.Interface
{
    public interface IUserAuthentication
    {
        Task<Status> LoginAsync(Login login);
        Task<Status> RegistrationAsync(Registration registration);
        Task<Status> LogoutAsync();
    }
}