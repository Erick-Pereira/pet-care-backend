using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IPetPhotoService : ICRUD<PetPhoto>
    {
        Task<DataResponse<PetPhoto>> GetByPetId(Guid petId);

        Task<Response> UpdatePhoto(Guid photoId, byte[] photoData, string? description = null);

        Task<Response> AddPetPhoto(Guid petId, byte[] photoData, string? description = null);

        Task<Response> UpdatePetPhoto(Guid petId, Guid photoId, byte[]? photoData = null, string? description = null);
    }
}