using QuickTalk.Application.DTOs.AccountSettingsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(int userId, string subFolder, FileDto fileDto);
    }
}
