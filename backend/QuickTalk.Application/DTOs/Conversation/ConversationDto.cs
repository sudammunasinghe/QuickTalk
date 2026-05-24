namespace QuickTalk.Application.DTOs.Conversation
{
    public class ConversationDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
        public int UnreadCount { get; set; }
        public bool IsOnline { get; set; }
    }
}
