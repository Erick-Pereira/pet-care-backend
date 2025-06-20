using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Exam exam)
        {
            var result = await _examService.Insert(exam);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _examService.Get(id);
            return result.Success.Value ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 50, [FromQuery] string? filter = null)
        {
            var result = await _examService.Get(skip, take, filter);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Exam exam)
        {
            exam.Id = id;
            var result = await _examService.Update(exam);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _examService.Delete(id);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }
    }
}