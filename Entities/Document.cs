namespace Entities
{
    public class Document : Entity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? DocumentNumber { get; set; }
        public string? IssuingAgency { get; set; }
        public Guid PetId { get; set; }
        public required Pet Pet { get; set; }
        public ICollection<DocumentAttachment> Attachments { get; set; } = new List<DocumentAttachment>();
    }
}