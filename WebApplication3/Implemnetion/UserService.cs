﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication3.Context;
using WebApplication3.Entity;
using WebApplication3.Entity.Security;
using WebApplication3.InterFace;



namespace WebApplication3.Implemnetion
{
    public class UserService : Iuser
    {
        private readonly IDataBaseService<User> _User;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IHttpContextAccessor _access;
        public UserService(IConfiguration configuration, IDataBaseService<User> User, IHttpContextAccessor access)
        {
            _User = User;
            _secretKey = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _access = access;

        }

        // Helper method to create password hash and salt
        private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }

        }

        // Helper method to verify the password
        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return computedHash == storedHash;
            }
        }

        // Helper method to generate a JWT token
        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Use UTC time
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Registration: hash the plain password, save the user and optionally generate a token
        public async Task<UserRegister> Login(UserLogin userLogin)
        {
            var Data = await _User.Find(x => x.Email == userLogin.Email);



            if (Data != null)
            {
                var IsVerifyPassword = VerifyPassword(userLogin.Password, Data.PasswordHash, Data.PasswordSlot);
                if (IsVerifyPassword)
                {
                    var token = GenerateJwtToken(Data);


                    UserRegister userRegister =
                        new UserRegister()
                        {
                            UserName = Data.UserName,
                            Email = userLogin.Email,
                            Token = token,
                            Password = userLogin.Password,

                        };
                    return userRegister;

                }
            }
         
            return null;
        }

        public async Task<UserRegister> Register(UserRegister userRegister)
        {
            CreatePasswordHash(userRegister.Password, out string hash, out string salt);



            User Data =
          new User()
          {
              UserName = userRegister.UserName,
              Email = userRegister.Email,
              PasswordHash = hash,
              PasswordSlot = salt,

          };

            // Save user to the database
            Data = await _User.Save(Data);

            // Optionally, generate a token upon registration

            userRegister.Token = GenerateJwtToken(Data);


            return userRegister;
        }
            
      public int? GetUserID()
{
            try
            {
                var user = _access.HttpContext?.User;
                if (user == null || !user.Identity.IsAuthenticated)
                {
                    return null; // User not authenticated
                }

                var claim = user.FindFirst(ClaimTypes.NameIdentifier);
                return claim != null ? int.Parse(claim.Value) : null;
            }
            catch
            {
                return null;
            }
        }
    }
}

