namespace QuickTalk.Application.DTOs.Conversation
{
    public class ConversationDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LastMessage { get; set; }
        public string LastMessageDisplayTime { get; set; }
        public int UnreadCount { get; set; }
        public bool IsOnline { get; set; }
    }
}
