using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MovieShopMVC.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId => Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        //public bool IsAuthenticated => true;
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity != null &&
                                       _httpContextAccessor.HttpContext != null &&
                                       _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string FullName => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value
                          + " " + _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

        public string Email => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public IEnumerable<string> Roles { get; }

        public bool IsAdmin { get; }
        public DateTime DateOfBirth => Convert.ToDateTime(_httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)?.Value);

        IEnumerable<string> ICurrentUserService.Roles => throw new NotImplementedException();

        DateTime ICurrentUserService.DateOfBirth => throw new NotImplementedException();
    }
}
