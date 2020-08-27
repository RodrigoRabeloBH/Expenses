using System;

namespace Domain
{
    public class Expense : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public double Value { get; set; }

        // Entity Relation
        public int UserId { get; set; }
        public User User { get; set; }
    }
}