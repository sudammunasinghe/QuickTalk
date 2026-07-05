using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.DTOs.AccountSettingsResponse
{
    public class FileDto
    {
        public Stream? FileStream { get; set; }
        public string? FileName { get; set; }
    }
}
