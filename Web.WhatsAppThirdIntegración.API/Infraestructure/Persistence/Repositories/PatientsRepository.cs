using Microsoft.EntityFrameworkCore;
using Shared;
using Web.Core.Business.API.Domain.Interfaces;
using Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Entities;

namespace Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        #region Variables
        private readonly ApplicationDbContext _context;
        #endregion

        #region Ctor
        public PatientsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        /* Función que consulta un paciente por identificación*/
        public async Task<RequestResult> GetById(string identification)
        {
            var patient = await _context.Patients.AsNoTracking()
                .Where(x => x.Identification.Equals(identification))
                .FirstOrDefaultAsync();

            if (patient != null)
                return RequestResult.SuccessResult(patient.Name);
            else
                return RequestResult.SuccessResultNoRecords();
        }
        /* Función que crea un paciente en la base de datos */
        public async Task<RequestResult> Create(string name, string identification, string city)
        {
            var isvalid = await isValidForCreate(name, identification, city);
            if (isvalid.success == false)
                return RequestResult.ErrorRecord(isvalid.result);
            var patient = await _context.Patients.AsNoTracking()
                .Where(x => x.Identification.Equals(identification))
                .FirstOrDefaultAsync();
            if (patient != null)
                return RequestResult.ErrorRecord($"El paciente con la identificación {identification} ya existe en el sistema");
            await _context.AddAsync(new Patient { Name = name, Id = Guid.NewGuid(), Identification = identification, City = city, CreatedAt = DateTime.Now });
            await _context.SaveChangesAsync();
            return RequestResult.SuccessRecord();
        }
        #endregion

        #region Private Methods

        /* Función que valida que los datos del paciente sean correctos */
        private async Task<(bool? success, string? result)> isValidForCreate(string name, string identification, string city)
        {
            bool success = true;
            string result = string.Empty;
            if (string.IsNullOrEmpty(name))
            {
                result += "El campo nombre está vacio";
                success = false;
            }
            if (string.IsNullOrEmpty(identification))
            {
                result += "El campo identificación está vacio";
                success = false;
            }
            if (string.IsNullOrEmpty(city))
            {
                result += "El campo ciudad está vacio";
                success = false;
            }
            return (success, success == true ? "Success" : result);
        }
        #endregion
    }
}
