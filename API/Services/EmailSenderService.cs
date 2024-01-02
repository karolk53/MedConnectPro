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
        public async Task<bool> SendVisitRegisteredEmail(Patient patient, Visit visit)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false, //for development it is false
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
            });

            StringBuilder template = new();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine(
                "<p>Your vist to dr " + 
                visit.Doctor.FirstName + 
                " " +
                visit.Doctor.LastName + 
                " has been registered successfully on " + 
                visit.PlannedDate.ToString() + 
                "</p>");
            template.AppendLine("-MedConnect Team");

            Email.DefaultRenderer = new RazorRenderer();
            Email.DefaultSender = sender;

            var email = await Email
                .From("medconnect@example.com")
                .To(patient.Email, patient.FirstName)
                .Subject("Visit registered")
                .UsingTemplate(template.ToString(), patient)
                .SendAsync();

            return email.Successful;
        }
    }
}