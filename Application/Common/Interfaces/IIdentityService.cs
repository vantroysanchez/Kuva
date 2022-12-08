using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<string> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<JsonResult> Login(string userName, string password);

        JsonResult RefreshToken(string UserName);
    }
}
