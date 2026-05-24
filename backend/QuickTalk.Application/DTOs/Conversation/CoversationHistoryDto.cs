namespace QuickTalk.Application.DTOs.Conversation
{
    public class CoversationHistoryDto
    {
        public int SenderId { get; set; }
        public string Message { get; set; }
        public DateTime? SendAt { get; set; }
    }
}
