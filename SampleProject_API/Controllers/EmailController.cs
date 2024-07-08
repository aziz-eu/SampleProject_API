using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleProject_API.Models.DTOs.Email;
using SampleProject_API.Services.EmailService;

namespace SampleProject_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDTO request)
        {
            _emailService.SendMail(request);
            return Ok();
        }
    }
}
