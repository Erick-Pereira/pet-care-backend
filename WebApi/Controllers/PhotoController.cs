using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PhotoController : ControllerBase
    {
        /*private readonly IPhotoService _photoService;
        private readonly FileValidator _fileValidator;

        [HttpPut("user/{id}/profile")]
        public async Task<IActionResult> UpdateUserProfilePhoto(Guid id, IFormFile photo)
        {
            var validation = await _fileValidator.ValidateAsync(photo);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var result = await _photoService.UpdateUserProfilePhoto(id, photo);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("pet/{id}/profile")]
        public async Task<IActionResult> UpdatePetProfilePhoto(Guid id, IFormFile photo)
        {
            var validation = await _fileValidator.ValidateAsync(photo);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var result = await _photoService.UpdatePetProfilePhoto(id, photo);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("pet/{id}/photos")]
        public async Task<IActionResult> AddPetPhoto(Guid id, IFormFile photo)
        {
            var validation = await _fileValidator.ValidateAsync(photo);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var result = await _photoService.AddPetPhoto(id, photo);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("pet/photos/{photoId}")]
        public async Task<IActionResult> DeletePetPhoto(Guid photoId)
        {
            var result = await _photoService.DeletePetPhoto(photoId);
            return result.Success ? Ok(result) : BadRequest(result);
        }*/
    }
}