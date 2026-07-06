using QuickTalk.Application.DTOs.AccountSettingsResponse;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(int userId, string subFolder, FileDto fileDto);
        Task DeleteFileAsync(string? relativePath);
    }
}
