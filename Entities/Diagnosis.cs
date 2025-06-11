namespace Entities
{
    public class Diagnosis : Entity
    {
        public Guid MedicalEventId { get; set; }
        public required MedicalEvent MedicalEvent { get; set; }
        public required string Condition { get; set; }
        public string? Severity { get; set; }
        public string? recommendedTreatment { get; set; }
        public DateOnly DiagnosisDate { get; set; }
        public string? Notes { get; set; }
    }
}