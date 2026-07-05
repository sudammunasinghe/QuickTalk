using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.DTOs.AccountSettingsResponse
{
    public class UpdateProfileDto : ProfileDetailsDto
    {
        public FileDto? profileImage { get; set; }
    }
}
