using EmailService.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            // Validate the request
            if (request == null)
                return BadRequest(new { error = "Request body cannot be null." });

            if (string.IsNullOrWhiteSpace(request.To))
                return BadRequest(new { error = "Recipient email (To) is required." });

            if (!new EmailAddressAttribute().IsValid(request.To))
                return BadRequest(new { error = "Recipient email (To) must be a valid email address." });

            if (string.IsNullOrWhiteSpace(request.Subject))
                return BadRequest(new { error = "Subject is required." });

            if (string.IsNullOrWhiteSpace(request.Body))
                return BadRequest(new { error = "Body is required." });

            try
            {
                // Decode the Body (since EmailClient URL-encodes it)
                var decodedBody = System.Web.HttpUtility.UrlDecode(request.Body);
                await _emailService.SendEmail(request.To, request.Subject, decodedBody);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to send email.", details = ex.Message });
            }
        }
    }

    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}