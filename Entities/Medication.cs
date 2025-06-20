namespace Entities
{
    public class Medication : Entity
    {
        public Guid MedicalEventId { get; set; }
        public MedicalEvent MedicalEvent { get; set; }
        public string Name { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }
    }
}