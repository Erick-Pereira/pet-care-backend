namespace Entities
{
    public class Document : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string IssuingAgency { get; set; } = string.Empty;
        public Guid PetId { get; set; }
        public Pet Pet { get; set; }
        public ICollection<DocumentAttachment> Attachments { get; set; } = new List<DocumentAttachment>();
    }
}