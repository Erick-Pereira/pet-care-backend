using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DiagnosisController : ControllerBase
    {
        private readonly IDiagnosisService _diagnosisService;

        public DiagnosisController(IDiagnosisService diagnosisService)
        {
            _diagnosisService = diagnosisService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Diagnosis diagnosis)
        {
            var result = await _diagnosisService.Insert(diagnosis);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _diagnosisService.Get(id);
            return result.Success.Value ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 50, [FromQuery] string? filter = null)
        {
            var result = await _diagnosisService.Get(skip, take, filter);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Diagnosis diagnosis)
        {
            diagnosis.Id = id;
            var result = await _diagnosisService.Update(diagnosis);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _diagnosisService.Delete(id);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }
    }
}