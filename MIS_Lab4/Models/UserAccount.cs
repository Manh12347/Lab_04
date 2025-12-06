namespace MIS_Lab4.Models
{
    public class UserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; } = false;
        public string Role { get; set; }
    }
}

