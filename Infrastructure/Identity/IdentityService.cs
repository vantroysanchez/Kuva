using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private RoleManager<IdentityRole> _roleManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService, IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _configuration = configuration;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<JsonResult> Login(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new JsonResult(CreateToken(userName));
            }

            return new JsonResult( new { Token = "401", Expiration = "401" });
        }

        public async Task<string> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return user.Id;
        }

        private async Task<bool> ValidateRoles(string userName)
        {
            var user = await _userManager.FindByEmailAsync(userName);

            var isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");

            return isAdmin;
        }

        private AuthenticationAnswer CreateToken(string userName)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userName)
            };

            if (ValidateRoles(userName).Result)
            {
                claims.Add(new Claim("role", "Administrator"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["KeyJwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expirationToken = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                                                        expires: expirationToken, signingCredentials: creds);
            var authenticationAnswer = new AuthenticationAnswer()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expirationToken
            };

            if (authenticationAnswer.Token.Any() && authenticationAnswer.Expiration.HasValue)
            {

                _httpContextAccessor.HttpContext?.Response.Cookies.Append("token", authenticationAnswer.Token,
                    new CookieOptions
                    {
                        Expires = authenticationAnswer.Expiration.Value,
                        HttpOnly = true,
                        Secure = true,                        
                        IsEssential = true,
                        SameSite = SameSiteMode.None
                    });

                return new AuthenticationAnswer()
                {
                    Token =authenticationAnswer.Token,
                    Expiration = authenticationAnswer.Expiration
                };
            }

            return new AuthenticationAnswer()
            {
                Token = "Fail",
                Expiration = null
            };
        }

        public JsonResult RefreshToken(string? UserName)
        {
            string Token = "Fail";
            DateTime? Expiration = null;

            if (!string.IsNullOrEmpty(UserName))
            {               
               return new JsonResult(CreateToken(UserName));
            }

            return new JsonResult(
                Token,
                Expiration
            );
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
