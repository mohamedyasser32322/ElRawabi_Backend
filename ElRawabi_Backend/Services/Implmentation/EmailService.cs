using ElRawabi_Backend.Services.Interface;
using System.Net;
using System.Net.Mail;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587");
            var smtpUser = _configuration["EmailSettings:SmtpUser"];
            var smtpPass = _configuration["EmailSettings:SmtpPass"];

            using var client = new SmtpClient(smtpServer, smtpPort)
            {
                UseDefaultCredentials = false, // مهم جداً لـ Gmail
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUser!),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            try
            {
                // الإرسال يكون داخل الـ try فقط
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // طباعة الخطأ بالتفصيل في الـ Console للباك إند
                Console.WriteLine($"Error sending email: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw; // إعادة رمي الخطأ ليصل للكنترولر ويظهر في الفرونت إند
            }
        }
    }
}
