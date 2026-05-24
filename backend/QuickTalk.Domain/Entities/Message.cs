namespace QuickTalk.Domain.Entities
{
    public class Message : BaseEntity
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? MessageText { get; set; }
        public bool? IsRead { get; set; }
    }
}
