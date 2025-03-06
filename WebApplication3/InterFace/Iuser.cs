using WebApplication3.Entity;
using WebApplication3.Entity.Security;

namespace WebApplication3.InterFace
{
    public interface Iuser
    {

        Task<(UserRegister? userRegister, string? token)> Login(UserLogin userLogin);

        Task<(UserRegister? userRegister, string? token)> Register(UserRegister userRegister);
        Task<(UserRegister? userRegister, string? token)> ChangePassword(ChangePassword password);


        Task<(UserRegister? userRegister, string? token)> update(UserUpdate password);

        int? GetUserID();



    }
}
