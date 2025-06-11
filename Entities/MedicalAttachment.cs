namespace Entities
{
    public class MedicalAttachment : Entity
    {
        public Guid MedicalEventId { get; set; }
        public required MedicalEvent MedicalEvent { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required byte[] FileData { get; set; }
    }
}