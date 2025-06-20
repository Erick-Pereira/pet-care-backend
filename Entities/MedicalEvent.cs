using Entities.Enums;

namespace Entities
{
    public class MedicalEvent : Entity
    {
        public Guid PetId { get; set; }
        public Pet Pet { get; set; }
        public MedicalEventType Type { get; set; }
        public DateOnly EventDate { get; set; }
        public string? ProfessionalName { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }

        public ICollection<MedicalAttachment>? Attachments { get; set; } = new List<MedicalAttachment>();
    }
}