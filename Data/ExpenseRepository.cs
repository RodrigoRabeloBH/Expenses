using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ExpenseRepository : Repository<Expense>, IExpenseServices
    {
        public ExpenseRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Expense>> GetAllWithUser()
        {
            return await _context.Expense
            .Include(e => e.User)
            .AsNoTracking()
            .ToListAsync();
        }
        public async Task<IEnumerable<Expense>> GetByDate(DateTime? minDate, DateTime? maxDate, int id)
        {
            var expenses = from expense in _context.Expense select expense;

            if (minDate.HasValue)
            {
                expenses = expenses.Where(e => e.DueDate >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                expenses = expenses.Where(e => e.DueDate <= maxDate.Value);
            }
            return await expenses
                .Include(u => u.User)
                .OrderByDescending(v => v.Value)
                .Where(e => e.UserId == id)
                .ToListAsync();
        }
        public async Task<Expense> GetByIdWithUser(int id)
        {
            return await _context.Expense
            .Include(e => e.User)
            .AsNoTracking()
            .FirstAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Expense>> GetByUserId(int userId)
        {
            return await _context.Expense
            .Include(e => e.User)
            .AsNoTracking()
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.Value)
            .ToListAsync();
        }
    }
}