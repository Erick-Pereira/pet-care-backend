using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class MedicationDALImpl : BaseDAL<Medication>, IMedicationDAL
    {
        public MedicationDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Medication>> logger)
            : base(dbContext, logger)
        {
        }

        public async Task<DataResponse<Medication>> GetByMedicalEventId(Guid medicalEventId)
        {
            try
            {
                var medications = await _dbContext.Medication
                    .Where(m => m.MedicalEventId == medicalEventId)
                    .OrderByDescending(m => m.StartDate)
                    .ToListAsync();

                return ResponseFactory.CreateInstance().CreateSuccessDataResponse(medications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving medications");
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Medication>(ex);
            }
        }
    }
}