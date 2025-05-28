using Entities;

namespace WebApi.Models
{
    public class MedicalEventRequest
    {
        public MedicalEvent Event { get; set; }
        public IFormFile[]? Attachments { get; set; }
    }
}