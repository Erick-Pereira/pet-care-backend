using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IMedicationService : ICRUD<Medication>
    {
        Task<DataResponse<Medication>> GetByMedicalEventId(Guid medicalEventId);
    }
}