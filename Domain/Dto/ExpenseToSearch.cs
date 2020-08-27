using System;

namespace Domain.Dto
{
    public class ExpenseToSearch
    {
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}