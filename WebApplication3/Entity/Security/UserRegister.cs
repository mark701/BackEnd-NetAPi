namespace WebApplication3.Entity.Security
{
    public class UserRegister
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Token { get; set; }
    }
}
