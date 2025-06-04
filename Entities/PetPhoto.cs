namespace Entities
{
    public class PetPhoto : Entity
    {
        public byte[]? PhotoData { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Guid PetId { get; set; }
        public Pet Pet { get; set; }
    }
}