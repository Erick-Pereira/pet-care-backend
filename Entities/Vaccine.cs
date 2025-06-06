namespace Entities
{
    public class Vaccine : Entity
    {
        public Guid MedicalEventId { get; set; }
        public required MedicalEvent MedicalEvent { get; set; }
        public required string Name { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public string? Batch { get; set; }
        public DateOnly? NextDose { get; set; }
    }
}