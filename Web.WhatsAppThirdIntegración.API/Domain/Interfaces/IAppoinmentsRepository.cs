using Shared;

namespace Web.WhatsAppThirdIntegración.API.Domain.Interfaces
{
    public interface IAppoinmentsRepository
    {
        Task<string> GetById(string id);
        Task<RequestResult> Create(string processCode, string idCode);
    }
}
