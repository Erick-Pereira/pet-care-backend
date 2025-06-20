using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicalAttachmentController : ControllerBase
    {
        private readonly IMedicalAttachmentService _medicalAttachmentService;

        public MedicalAttachmentController(IMedicalAttachmentService medicalAttachmentService)
        {
            _medicalAttachmentService = medicalAttachmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalAttachment attachment)
        {
            var result = await _medicalAttachmentService.Insert(attachment);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _medicalAttachmentService.Get(id);
            return result.Success.Value ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 50, [FromQuery] string? filter = null)
        {
            var result = await _medicalAttachmentService.Get(skip, take, filter);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicalAttachment attachment)
        {
            attachment.Id = id;
            var result = await _medicalAttachmentService.Update(attachment);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _medicalAttachmentService.Delete(id);
            return result.Success.Value ? Ok(result) : BadRequest(result);
        }
    }
}