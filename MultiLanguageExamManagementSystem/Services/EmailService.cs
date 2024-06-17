using System.Net.Mail;

namespace MultiLanguageExamManagementSystem.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendExamResultsAsync(string email, double score)
        {
            using (var client = new SmtpClient("smtp.example.com"))
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("noreply@example.com"),
                    Subject = "Your Exam Results",
                    Body = $"Your score is {score}%.",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
