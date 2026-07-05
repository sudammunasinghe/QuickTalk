using QuickTalk.Application.DTOs.AccountSettingsResponse;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Application.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFileAsync(int userId, string subFolder, FileDto fileDto)
        {
            string fullPath = "";
            try
            {
                var folder = Path.Combine("wwwroot", subFolder, userId.ToString());
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileDto.FileName)}";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                fullPath = Path.Combine(folder, uniqueFileName);
                var fileUrl = $"{subFolder}/{userId}/{uniqueFileName}";
                await SaveFileAsync(fullPath, fileDto.FileStream);
                return fileUrl;
            }
            catch
            {
                if(File.Exists(fullPath))
                    File.Delete(fullPath);
                throw;
            }
        }

        public async Task DeleteFileAsync(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return;

            var fullPath = Path.Combine(
                "wwwroot",
                relativePath.Replace('/', Path.DirectorySeparatorChar)
            );

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        private async Task SaveFileAsync(string fullFilePath, Stream fileStream)
        {
            using var fs = new FileStream(fullFilePath, FileMode.Create);
            await fileStream.CopyToAsync(fs);
        }
    }
}