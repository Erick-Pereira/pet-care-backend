namespace Entities
{
    public class DocumentAttachment : Entity
    {
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
    }
}