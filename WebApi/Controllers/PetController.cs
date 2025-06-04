using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using web_api.Controllers;
using web_api.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase, IController<Pet>
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService ?? throw new ArgumentNullException(nameof(petService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
        {
            try
            {
                var response = await _petService.Get(skip, take);
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
                var response = await _petService.Get(id);
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
        public async Task<IActionResult> Create([FromBody] Pet request)
        {
            try
            {
                var response = await _petService.Insert(request);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] Pet request)
        {
            try
            {
                request.Id = id;
                var response = await _petService.Update(request);
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
                var response = await _petService.Delete(id);
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
                var response = await _petService.ToggleActive(id);
                return response.Success == true
                    ? Ok(new
                    {
                        response.Success,
                        response.Message
                    })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterPet([FromBody] Pet request)
        {
            try
            {
                var response = await _petService.RegisterPetWithOwner(request);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }
    }
}