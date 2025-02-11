using WebApplication3.Entity;
using WebApplication3.Entity.Security;

namespace WebApplication3.InterFace
{
    public interface Iuser
    {

        Task<UserRegister> Login(UserLogin userLogin);

        Task<UserRegister> Register(UserRegister userRegister);

        int? GetUserID();



    }
}
