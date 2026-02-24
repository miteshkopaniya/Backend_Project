using Backend_Project.Enums;

namespace Backend_Project.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public TicketStatus Status { get; set; } = TicketStatus.OPEN;
        public TicketPriority Priority { get; set; } = TicketPriority.MEDIUM;

        public int CreatedBy { get; set; }
        public int? AssignedTo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}