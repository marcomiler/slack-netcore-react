using System.Linq;
using Microsoft.AspNetCore.Http;

using Application.Interfaces;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public class UserAccesor : IUserAccessor
    {
        public readonly IHttpContextAccessor httpContextAccessor;
        public UserAccesor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserName()
        {
            var userName = httpContextAccessor
                            .HttpContext
                            .User?
                            .Claims?
                            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
                            .Value;
            
            return userName;
        }
    }
}