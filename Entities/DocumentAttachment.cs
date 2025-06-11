namespace Entities
{
    public class DocumentAttachment : Entity
    {
        public Guid DocumentId { get; set; }
        public required Document Document { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required byte[] FileData { get; set; }
    }
}