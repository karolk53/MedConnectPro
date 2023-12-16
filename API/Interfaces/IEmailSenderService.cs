using API.Entities;

namespace API.Interfaces
{
    public interface IEmailSenderService
    {
        Task<bool> SendVisitRegisteredEmail(Patient patient);
    }
}