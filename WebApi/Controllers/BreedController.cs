using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Services;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BreedController : ControllerBase, IController<Breed>
    {
        private readonly IBreedService _breedService;

        public BreedController(IBreedService breedService)
        {
            _breedService = breedService ?? throw new ArgumentNullException(nameof(breedService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
        {
            try
            {
                var response = await _breedService.Get(skip, take);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message, response.Data })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _breedService.Get(id);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message, response.Item })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPost]
        [Permission("Admin")]
        public async Task<IActionResult> Create([FromBody] Breed request)
        {
            try
            {
                var response = await _breedService.Insert(request);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        [Permission("Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Breed request)
        {
            try
            {
                request.Id = id;
                var response = await _breedService.Update(request);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        [Permission("Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var response = await _breedService.Delete(id);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPatch("{id}/toggle-active")]
        [Permission("Admin")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            try
            {
                var response = await _breedService.ToggleActive(id);
                return response.Success == true
                    ? Ok(new
                    {
                        Success = true,
                        Message = $"Breed {(response.Item?.Active == true ? "activated" : "deactivated")} successfully"
                    })
                    : BadRequest(new { Success = false, Message = response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }
    }
}