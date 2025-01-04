using Microsoft.AspNetCore.Identity.UI.Services;

namespace ECommercePlatform.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Sending email to {email}: {subject}");
            Console.WriteLine(htmlMessage );
            return Task.CompletedTask;
        }
    }
}
