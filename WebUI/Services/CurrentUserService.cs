﻿using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebUI.Services
{    
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                
        public string? Email => _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

        public string? Roles => _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
    }
}
