using System.ComponentModel.DataAnnotations;

namespace QuickTalk.Application.DTOs.Conversation
{
    public class SendMessageDto
    {
        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
