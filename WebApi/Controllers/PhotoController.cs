using BLL.Interfaces;
using BLL.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PhotoController : ControllerBase
    {
        private readonly IPetPhotoService _petPhotoService;
        private readonly IPetService _petService;
        private readonly IUserService _userService;
        private readonly FileValidator _fileValidator;

        public PhotoController(IPetPhotoService petPhotoService, IPetService petService, IUserService userService, FileValidator fileValidator)
        {
            _petPhotoService = petPhotoService;
            _petService = petService;
            _userService = userService;
            _fileValidator = fileValidator;
        }

        private async Task<byte[]> GetBytesFromFormFileAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        // PET PROFILE PHOTO
        [HttpPut("pet/{petId}/profile")]
        public async Task<IActionResult> UpdatePetProfilePhoto(Guid petId, IFormFile photo)
        {
            var validation = await _fileValidator.ValidateAsync(photo);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);
            var bytes = await GetBytesFromFormFileAsync(photo);
            var result = await _petService.UpdateProfilePhoto(petId, bytes);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        // USER PROFILE PHOTO
        [HttpPut("user/{userId}/profile")]
        public async Task<IActionResult> UpdateUserProfilePhoto(Guid userId, IFormFile photo)
        {
            var validation = await _fileValidator.ValidateAsync(photo);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);
            var bytes = await GetBytesFromFormFileAsync(photo);
            var result = await _userService.UpdateProfilePhoto(userId, bytes);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        // PET GALLERY PHOTOS
        [HttpPost("pet/{petId}/photos")]
        public async Task<IActionResult> AddPetPhoto(Guid petId, IFormFile photo, [FromForm] string? description)
        {
            var validation = await _fileValidator.ValidateAsync(photo);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);
            var bytes = await GetBytesFromFormFileAsync(photo);
            var result = await _petPhotoService.AddPetPhoto(petId, bytes, description);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpPut("pet/{petId}/photos/{photoId}")]
        public async Task<IActionResult> UpdatePetPhoto(Guid petId, Guid photoId, IFormFile? photo, [FromForm] string? description)
        {
            byte[]? bytes = null;
            if (photo != null)
            {
                var validation = await _fileValidator.ValidateAsync(photo);
                if (!validation.IsValid)
                    return BadRequest(validation.Errors);
                bytes = await GetBytesFromFormFileAsync(photo);
            }
            var result = await _petPhotoService.UpdatePetPhoto(petId, photoId, bytes, description);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("pet/{petId}/photos/{photoId}")]
        public async Task<IActionResult> DeletePetPhoto(Guid petId, Guid photoId)
        {
            var result = await _petPhotoService.Delete(photoId);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }
    }
}