using SampleProject_API.Models;
using SampleProject_API.Models.DTOs.User;

namespace SampleProject_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
