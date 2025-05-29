using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace EmailService.Services
{
    public interface IEmailService
    {
        Task SendEmail(string reciever, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _senderEmail;
        private readonly string _senderName;

        public EmailService()
        {
            _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new Exception("SENDGRID_API_KEY environment variable not set.");
            }
            _senderEmail = "support@pornworship.com";
            _senderName = "support@pornworship.com";
        }

        public async Task SendEmail(string reciever, string subject, string body)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_senderEmail, _senderName);
            var to = new EmailAddress(reciever);
            // Plain text version (fallback for clients that don't support HTML)
            var plainTextContent = $"Click the link to reset your password: {body}";

            // HTML version with styling
            var htmlContent = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <div style='background-color: #f4f4f4; padding: 15px; text-align: center;'>
                        <h1 style='color: #333; margin: 0;'>PornWorship</h1>
                    </div>
                    <div style='padding: 20px; background-color: #fff; border: 1px solid #ddd;'>
                        <h2 style='color: #333;'>Password Reset Request</h2>
                        <p style='color: #666; line-height: 1.6;'>You requested to reset your password for PornWorship.com. Click the button below to proceed:</p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='{body}' style='background-color: #007bff; color: #fff; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block;'>Reset Password</a>
                        </div>
                        <p style='color: #666; line-height: 1.6;'>If you didn’t request this, you can ignore this email.</p>
                    </div>
                    <div style='text-align: center; padding: 10px; color: #999; font-size: 12px;'>
                        <p>&copy; {DateTime.Now.Year} PornWorship. All rights reserved.</p>
                    </div>
                </div>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            msg.AddBcc(new EmailAddress("support@pornworship.com")); // BCC for visibility in Porkbun inbox
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                var errorBody = await response.Body.ReadAsStringAsync();
                throw new Exception($"Failed to send email: {response.StatusCode} - {errorBody}");
            }
        }
    }
}