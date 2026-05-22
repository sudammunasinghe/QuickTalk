using Microsoft.AspNetCore.Http;
using QuickTalk.Application.Interfaces.IServices;
using System.Security.Claims;

namespace QuickTalk.Infrastructure.Identity
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?
                    .User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                    throw new UnauthorizedAccessException("Unauthorized .");

                return int.Parse(userIdClaim.Value);
            }
        }
    }
}
