using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication3.Entity.Security;
using WebApplication3.InterFace;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApplication3.Entity.DataBase;

namespace WebApplication3.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IDataBaseService<User> _userData;
        private readonly Iuser _user;

        public UserController( IDataBaseService<User> userData , Iuser user) 
        {
            _user = user;
            _userData = userData;

        }

        [AllowAnonymous]
        [HttpPost("loginuser")]
        public async Task<IActionResult> login([FromBody] UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {


                return BadRequest(ModelState);
            }



            try
            {

                var (userRegister, token) = await  _user.Login(userLogin);

                return Ok(new
                {
                    User = userRegister,
                    Token = token
                });
            }
            catch (SqlException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("UserRegister")]
        public async Task<IActionResult> Register([FromForm] UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {


                return BadRequest(ModelState);
            }



            try
            {

                var Data = await _user.Register(userRegister);

                return Ok(Data);
            }
            catch (SqlException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
