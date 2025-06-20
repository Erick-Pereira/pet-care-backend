using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;

        public VaccineController(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vaccine vaccine)
        {
            var result = await _vaccineService.Insert(vaccine);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _vaccineService.Get(id);
            return result.Success.Value ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 50, [FromQuery] string? filter = null)
        {
            var result = await _vaccineService.Get(skip, take, filter);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Vaccine vaccine)
        {
            vaccine.Id = id;
            var result = await _vaccineService.Update(vaccine);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _vaccineService.Delete(id);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }
    }
}