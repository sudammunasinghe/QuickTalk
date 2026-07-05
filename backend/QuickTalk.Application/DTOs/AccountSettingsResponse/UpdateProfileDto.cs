using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.DTOs.AccountSettingsResponse
{
    public class UpdateProfileDto : ProfileDetailsDto
    {
        public bool RemoveProfileImage { get; set; }
        public FileDto? profileImage { get; set; }
    }
}
