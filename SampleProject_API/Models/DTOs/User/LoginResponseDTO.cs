namespace SampleProject_API.Models.DTOs.User;
using SampleProject_API.Models;

public class LoginResponseDTO
{
    public User User { get; set; }
    public string Token { get; set; }
}
