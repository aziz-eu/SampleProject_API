using SampleProject_API.Models.DTOs.Email;

namespace SampleProject_API.Services.EmailService
{
    public interface IEmailService
    {
        void SendMail(EmailDTO request);
    }
}
