using Commons.Interfaces;
using Commons.Responses;
using Entities;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface IPetPhotoService : ICRUD<PetPhoto>
    {
        Task<DataResponse<PetPhoto>> GetByPetId(Guid petId);
    }
}