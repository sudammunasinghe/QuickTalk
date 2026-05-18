using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string HashedPassword);
    }
}
