using Shared;
using Web.Core.Business.API.Enums;

namespace Web.Core.Business.API.Domain.Interfaces
{
    public interface IPatientsRepository
    {
        Task<RequestResult> GetById(string identification);
        Task<RequestResult> Create(string name, string identification, string city);
    }
}
