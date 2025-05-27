using EmailService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailsController(IEmailService emailService) 
        { 
                this._emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string reciever, string subject, string body)
        {
            await _emailService.SendEmail(reciever, subject, body);
            return Ok();
        }
    }
}
