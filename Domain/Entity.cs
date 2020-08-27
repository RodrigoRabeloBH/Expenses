using System;

namespace Domain
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public Entity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}