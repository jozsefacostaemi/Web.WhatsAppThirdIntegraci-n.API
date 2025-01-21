using Microsoft.AspNetCore.Mvc;
using Shared;
using Web.Core.Business.API.Domain.Interfaces;
using Web.WhatsAppThirdIntegración.API.DTOs;

namespace Web.WhatsAppThirdIntegración.API
{
    [Route("Patients")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsController(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }

        [HttpGet("GetById")]
        public async Task<RequestResult> GetById(string identification)
        {
            return await _patientsRepository.GetById(identification);
        }

        [HttpPost("Create")]
        public async Task<RequestResult> Create([FromBody] CreatePatientDTO createPatientDTO)
        {
            return await _patientsRepository.Create(createPatientDTO.name, createPatientDTO.identification, createPatientDTO.city);
        }


    }
}
