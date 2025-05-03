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
    public class StateController : ControllerBase, IController<State>
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
        {
            try
            {
                var response = await _stateService.Get(skip, take);
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
                var response = await _stateService.Get(id);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message, Data = response.Item })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPost]
        [Permission("Admin")]
        public async Task<IActionResult> Create([FromBody] State request)
        {
            try
            {
                var response = await _stateService.Insert(request);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] State request)
        {
            try
            {
                request.Id = id;
                var response = await _stateService.Update(request);
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
                var response = await _stateService.Delete(id);
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
                var response = await _stateService.ToggleActive(id);
                return response.Success == true
                    ? Ok(new
                    {
                        Success = true,
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