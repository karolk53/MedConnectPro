using System.Net.Mail;
using System.Text;
using API.Entities;
using API.Interfaces;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;

namespace API.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task<bool> SendEmail(Patient patient)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false, //for development it is false
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
            });

            StringBuilder template = new();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>Your vist has been registered successfully</p>");
            template.AppendLine("-MedConnect Team");

            Email.DefaultRenderer = new RazorRenderer();
            Email.DefaultSender = sender;

            var email = await Email
                .From("system@example.com")
                .To("user@test.pl", "John")
                .Subject("Test!")
                .UsingTemplate(template.ToString(), patient)
                //.Body("Testing email sending")
                .SendAsync();

            return email.Successful;
        }
    }
}