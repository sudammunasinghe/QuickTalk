using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.DTOs.AccountSettingsResponse
{
    public class PrivacySettingsDto : UpdatePrivacySettings
    {
        public int Id { get; set; }
    }
}
