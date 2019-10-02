namespace support_issue_tracker.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}