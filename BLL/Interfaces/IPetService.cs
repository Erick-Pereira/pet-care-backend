﻿using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IPetService : ICRUD<Pet>
    {
        Task<Response> RegisterPetWithOwner(Pet request);

        Task<SingleResponse<Pet>> ToggleActive(Guid id);

        Task<Response> UpdateProfilePhoto(Guid petId, byte[] photoData);

        Task<Response> DeleteProfilePhoto(Guid petId);
    }
}