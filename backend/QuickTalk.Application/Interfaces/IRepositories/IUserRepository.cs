using QuickTalk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserIdAsync(int userId);
    }
}
