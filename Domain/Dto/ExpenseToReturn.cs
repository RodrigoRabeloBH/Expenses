using System;

namespace Domain.Dto
{
    public class ExpenseToReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public double Value { get; set; }
        public int UserId { get; set; }
    }
}