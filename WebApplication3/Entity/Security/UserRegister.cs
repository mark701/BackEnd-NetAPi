namespace WebApplication3.Entity.Security
{
    public class UserRegister
    {
        public int? UserID { get;set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string? PathURL { get; set; }


        public IFormFile? ProfileImage { get; set; }

        //public string? Token { get; set; }
    }
}
