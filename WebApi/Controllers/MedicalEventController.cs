using BLL.Validation;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicalEventController : ControllerBase
    {
        /*private readonly IMedicalEventService _medicalEventService;
        private readonly FileValidator _fileValidator;

        [HttpPost]
        public async Task<IActionResult> CreateMedicalEvent([FromForm] MedicalEventRequest request)
        {
            if (request.Attachments != null)
            {
                foreach (var file in request.Attachments)
                {
                    var validation = await _fileValidator.ValidateAsync(file, false);
                    if (!validation.IsValid)
                        return BadRequest(validation.Errors);
                }
            }

            var result = await _medicalEventService.Create(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{eventId}/vaccine")]
        public async Task<IActionResult> AddVaccine(Guid eventId, [FromBody] Vaccine vaccine)
        {
            var result = await _medicalEventService.AddVaccine(eventId, vaccine);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{eventId}/exam")]
        public async Task<IActionResult> AddExam(Guid eventId, [FromBody] Exam exam)
        {
            var result = await _medicalEventService.AddExam(eventId, exam);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{eventId}/medication")]
        public async Task<IActionResult> AddMedication(Guid eventId, [FromBody] Medication medication)
        {
            var result = await _medicalEventService.AddMedication(eventId, medication);
            return result.Success ? Ok(result) : BadRequest(result);
        }*/
    }
}