using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IPetPhotoService : ICRUD<PetPhoto>
    {
        Task<DataResponse<PetPhoto>> GetByPetId(Guid petId);
    }
}