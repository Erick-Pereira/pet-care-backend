namespace Entities
{
    public class Medication : Entity
    {
        public Guid MedicalEventId { get; set; }
        public required MedicalEvent MedicalEvent { get; set; }
        public required string Name { get; set; }
        public string? Dosage { get; set; } 
        public string? Frequency { get; set; } 
        public required DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }
    }
}