
using MailKit.Net.Smtp;
using MimeKit;
using Data.Helpers;
using Service.Abstracts;
namespace Service.Implementations
{
    public class EmailsService : IEmailsService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion
        #region Constructors
        public EmailsService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        #endregion
        #region Handle Functions
        public async Task<string> SendEmail(string email, string Message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    // Use SSL for port 465, or StartTLS for port 587
                    bool useSsl = _emailSettings.Port == 465;
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, useSsl);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Future Team", _emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = reason==null ? "No Submitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return "Success";
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return $"Failed: {ex.Message}";
            }
        }
        #endregion
    }
}
