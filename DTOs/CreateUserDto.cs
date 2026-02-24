using Backend_Project.Enums;

namespace Backend_Project.DTOs
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}