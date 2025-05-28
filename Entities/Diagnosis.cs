namespace Entities
{
    public class Diagnosis : Entity
    {
        public Guid MedicalEventId { get; set; }
        public MedicalEvent MedicalEvent { get; set; }
        public string Condition { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public string recommendedTreatment { get; set; } = string.Empty;
        public DateOnly DiagnosisDate { get; set; }
        public string? Notes { get; set; }
    }
}