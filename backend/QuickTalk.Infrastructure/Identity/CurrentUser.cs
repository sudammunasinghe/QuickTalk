using Microsoft.AspNetCore.Http;
using QuickTalk.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Infrastructure.Identity
{
    public class CurrentUser : IcurrentUser
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
