using Backend_Project.Enums;   

namespace Backend_Project.DTOs
{
    public class CreateTicketDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketPriority Priority { get; set; }
    }
}