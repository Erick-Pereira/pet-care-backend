namespace Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Active { get; set; } = true;
        public DateTime? CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

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