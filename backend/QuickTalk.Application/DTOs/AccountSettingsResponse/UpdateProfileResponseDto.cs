using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.DTOs.AccountSettingsResponse
{
    public class UpdateProfileResponseDto : ProfileDetailsDto
    {
        public Guid Id { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
