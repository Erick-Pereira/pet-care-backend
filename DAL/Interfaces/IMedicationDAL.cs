using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace DAL.Interfaces
{
    public interface IMedicationDAL : ICRUD<Medication>
    {
        Task<DataResponse<Medication>> GetByMedicalEventId(Guid medicalEventId);
    }
}