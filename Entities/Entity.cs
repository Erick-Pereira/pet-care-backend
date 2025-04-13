namespace Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public virtual void UpdateDateTime()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual void UpdateDateTime(DateTime time)
        {
            UpdatedAt = time;
        }

        public virtual void EnableEntity()
        {
            Active = true;
        }

        public virtual void DisableEntity()
        {
            Active = false;
        }
    }
}