namespace support_tracker.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}