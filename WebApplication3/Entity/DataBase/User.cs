namespace WebApplication3.Entity.DataBase
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public  string? PathURL { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSlot { get; set; }



    }
}
