using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleProject_API.Models.DTOs.User;
using SampleProject_API.Repository.IRepository;

namespace SampleProject_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userDb;


        public UserController(IUserRepository userDb)
        {
            _userDb = userDb;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var loginRespone = await _userDb.Login(loginRequestDTO);

                if (loginRespone.User == null || string.IsNullOrEmpty(loginRespone.Token))
                {
                    return BadRequest("Invalid User Name or Password");
                }

                return Ok(loginRespone);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            try
            {
                bool isUserNameUnique =  _userDb.IsUniqueUser(registrationRequestDTO.UserName);

                if (!isUserNameUnique)
                {                    
                    return BadRequest("UserName Already Exits!");
                }

                var user = await _userDb.Register(registrationRequestDTO);

                if (user == null)
                {
                    return BadRequest();
                }

                return Ok(user);


            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
