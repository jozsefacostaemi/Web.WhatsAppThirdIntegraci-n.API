using Microsoft.AspNetCore.Mvc;
using Shared;
using Web.WhatsAppThirdIntegración.API.Domain.Interfaces;
using Web.WhatsAppThirdIntegración.API.DTOs;

namespace Web.WhatsAppThirdIntegración.API
{
    [Route("Appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppoinmentsRepository _appoinmentsRepository;
        public AppointmentController(IAppoinmentsRepository appoinmentsRepository)
        {
            _appoinmentsRepository = appoinmentsRepository;
        }


        [HttpGet("GetById")]
        public async Task<string> GetById(string id)
        {
            return await _appoinmentsRepository.GetById(id);
        }

        [HttpPost("Create")]
        public async Task<RequestResult> Create([FromBody] CreateAppoinmentDTO createAppoinmentDTO)
        {
            return await _appoinmentsRepository.Create(createAppoinmentDTO.processCode, createAppoinmentDTO.idCode);
        }


    }
}
