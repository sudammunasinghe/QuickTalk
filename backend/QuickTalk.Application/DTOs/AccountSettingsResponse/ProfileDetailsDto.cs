using System.ComponentModel.DataAnnotations;

namespace QuickTalk.Application.DTOs.AccountSettingsResponse
{
    public class ProfileDetailsDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        public string Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
