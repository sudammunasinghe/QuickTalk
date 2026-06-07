using QuickTalk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync();
        Task SaveOtpAsync(User user, string otp);
    }
}
