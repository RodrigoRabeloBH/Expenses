using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Services.Interfaces
{
    public interface IExpenseServices : IRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetByDate(DateTime? minDate, DateTime? maxDate, int id);
        Task<IEnumerable<Expense>> GetAllWithUser();
        Task<Expense> GetByIdWithUser(int id);
        Task<IEnumerable<Expense>> GetByUserId(int userId);
    }
}