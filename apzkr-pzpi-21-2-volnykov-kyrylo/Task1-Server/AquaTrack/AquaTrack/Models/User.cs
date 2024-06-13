namespace AquaTrack.Models
{
    public enum Role
    {
        User,
        Admin
    }

    public class User
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }

        public List<Aquarium> Aquariums { get; set; }
    }
}
