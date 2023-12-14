using API.Entities;

namespace API.Interfaces
{
    public interface IEmailSenderService
    {
        Task<bool> SendEmail(Patient patient);
    }
}