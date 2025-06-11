namespace Entities
{
    public class Exam : Entity
    {
        public Guid MedicalEventId { get; set; }
        public required MedicalEvent MedicalEvent { get; set; }
        public required string ExamType { get; set; }
        public string? Result { get; set; }
        public DateOnly? ExamDate { get; set; }
        public string? Notes { get; set; }
    }
}