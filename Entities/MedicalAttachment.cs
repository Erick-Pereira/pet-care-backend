namespace Entities
{
    public class MedicalAttachment : Entity
    {
        public Guid MedicalEventId { get; set; }
        public MedicalEvent MedicalEvent { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public byte[]? FileData { get; set; }
    }
}