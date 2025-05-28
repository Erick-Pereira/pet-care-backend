namespace Entities
{
    public class Exam : Entity
    {
        public Guid MedicalEventId { get; set; }
        public MedicalEvent MedicalEvent { get; set; }
        public string ExamType { get; set; }
        public string Result { get; set; } = string.Empty;
        public DateOnly ExamDate { get; set; }
        public string? Notes { get; set; }
    }
}