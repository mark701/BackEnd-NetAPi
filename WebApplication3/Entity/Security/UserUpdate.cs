namespace WebApplication3.Entity.Security
{
    public class UserUpdate
    {
        public int? UserID { get; set; }
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
        public string? PathURL { get; set; }


        public IFormFile? ProfileImage { get; set; }
    }
}
