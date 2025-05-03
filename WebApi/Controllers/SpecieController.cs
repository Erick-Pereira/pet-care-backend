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
    public class SpecieController : ControllerBase, IController<Specie>
    {
        private readonly ISpecieService _specieService;

        public SpecieController(ISpecieService specieService)
        {
            _specieService = specieService ?? throw new ArgumentNullException(nameof(specieService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
        {
            try
            {
                var response = await _specieService.Get(skip, take);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message, response.Data })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _specieService.Get(id);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message, Data = response.Item })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPost]
        [Permission("Admin")]
        public async Task<IActionResult> Create([FromBody] Specie request)
        {
            try
            {
                var response = await _specieService.Insert(request);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        [Permission("Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Specie request)
        {
            try
            {
                request.Id = id;
                var response = await _specieService.Update(request);
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
                var response = await _specieService.Delete(id);
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
                var response = await _specieService.ToggleActive(id);
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
    }
}