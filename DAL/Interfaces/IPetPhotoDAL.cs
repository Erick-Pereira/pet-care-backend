using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace DAL.Interfaces
{
    public interface IPetPhotoDAL : ICRUD<PetPhoto>
    {
        Task<DataResponse<PetPhoto>> GetByPetId(Guid petId);
    }
}