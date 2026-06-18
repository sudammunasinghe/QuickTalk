namespace QuickTalk.Application.DTOs.Conversation
{
    public class CoversationHistoryDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }
        public DateTime? SendAt { get; set; }
        public bool IsMine { get; set; }
    }
}
