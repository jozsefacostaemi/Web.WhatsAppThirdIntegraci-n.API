using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shared;
using Web.WhatsAppThirdIntegración.API.Domain.Interfaces;
using Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Entities;

namespace Web.Core.Business.API.Infraestructure.Persistence.Repositories.EmitMessage
{
    public class AppoinmentsRepository : IAppoinmentsRepository
    {
        #region Variables
        private readonly ApplicationDbContext _context;
        #endregion

        #region Ctor
        public AppoinmentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        /* Función que crea un paciente en la base de datos */
        public async Task<RequestResult> Create(string processCode, string idCode)
        {
            var isvalid = await isValidForCreate(processCode, idCode);
            if (isvalid.success == false)
                return RequestResult.ErrorRecord(isvalid.result);
            var patient = await _context.Patients.AsNoTracking()
                .Where(x => x.Identification.Equals(idCode))
                .FirstOrDefaultAsync();
            if (patient == null)
                return RequestResult.ErrorRecord($"El paciente con la identificación {idCode} no existe en el sistema");
            var process = await _context.Processors.AsNoTracking()
              .Where(x => x.Code.Equals(processCode))
              .FirstOrDefaultAsync();
            if (process == null)
                return RequestResult.ErrorRecord($"El proceso con código {processCode} no existe en el sistema");
            DateTime actualDate = DateTime.Now;
            await _context.AddAsync(new Appointment { Id = Guid.NewGuid(), PatientId = patient.Id, ProcessId = process.Id, StartDate = actualDate, EndDate = actualDate.AddHours(1) });
            await _context.SaveChangesAsync();
            return RequestResult.SuccessRecord();
        }

        /* Función que consultas las citas de un paciente  */
        public async Task<string> GetById(string id)
        {
            string result = string.Empty;
            var appointents = await _context.Appointments.AsNoTracking().Where(x=>x.Patient.Identification.Equals(id)).OrderBy(x => x.StartDate).Select(x => new { x.StartDate, x.Process.Name }).ToListAsync();
            if (appointents?.Count == 0)
                return "No tiene citas relacionadas a la fecha";
            else
            {
                result += "Usted tiene relacionada las siguientes citas:\n";
                foreach (var drAppoinment in appointents)
                    result += $"{drAppoinment.Name} agendada el {drAppoinment.StartDate.ToShortDateString()} a las {drAppoinment.StartDate.ToShortTimeString()}\n";

            }
            return result;
        }

        #endregion

        #region Private Methods

        /* Función que valida que los datos del paciente sean correctos */
        private async Task<(bool? success, string? result)> isValidForCreate(string processCode, string idCode)
        {
            bool success = true;
            string result = string.Empty;
            if (string.IsNullOrEmpty(processCode))
            {
                result += "No indicó el proceso";
                success = false;
            }
            if (string.IsNullOrEmpty(idCode))
            {
                result += "No indicó la idenificación";
                success = false;
            }
            return (success, success == true ? "Success" : result);
        }
        #endregion
    }
}
