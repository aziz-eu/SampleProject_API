using Microsoft.IdentityModel.Tokens;
using SampleProject_API.Data;
using SampleProject_API.Models;
using SampleProject_API.Models.DTOs.User;
using SampleProject_API.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleProject_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("JWT:Secret");
        }
        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null) { return true; }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower() &&
            u.Password == loginRequestDTO.Password);

            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null,
                };
            }

            // If User Found then Genetate JWT token

            var tokenHendler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Name, user.Id.ToString()),
                     new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHendler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHendler.WriteToken(token),
                User = user,
            };
            return loginResponseDTO;
        }

        public async Task<User> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            try
            {
                User user = new()
                {
                    UserName = registrationRequestDTO.UserName,
                    Name = registrationRequestDTO.Name,
                    Password = registrationRequestDTO.Password,
                    Role = registrationRequestDTO.Role,
                };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                user.Password = "";
                return user;
            }
            catch (Exception ex) { throw; }
        }
    }
}
