using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using AutoMapper;
using Domain;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseServices _rep;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseServices rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        [HttpGet("{index}/{length}")]
        public async Task<IActionResult> GetAll(int index, int length)
        {
            var expenses = _mapper.Map<IEnumerable<ExpenseToReturn>>(await _rep.GetAllWithUser());

            return Ok(new Pagination<ExpenseToReturn>(expenses, index, length));
        }
        [HttpPost("{userId}/{index}/{length}")]
        public async Task<IActionResult> GetByInterval(ExpenseToSearch search, int index, int length, int userId)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int lastDay = DateTime.DaysInMonth(year, month);

            search.MinDate = !search.MinDate.HasValue ? new DateTime(year, month, 1) : search.MinDate;
            search.MaxDate = !search.MaxDate.HasValue ? new DateTime(year, month, lastDay) : search.MaxDate;

            var expenses = _mapper.Map<IEnumerable<ExpenseToReturn>>(await _rep.GetByDate(search.MinDate, search.MaxDate, userId));

            return Ok(new Pagination<ExpenseToReturn>(expenses.OrderBy(e => e.DueDate), index, length));
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertExpense(Expense expense)
        {
            await _rep.Create(expense);

            return Ok(expense);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var expense = await _rep.GetByIdWithUser(id);

            return Ok(_mapper.Map<ExpenseToReturn>(expense));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Expense expenseToUpdate)
        {
            Expense expense = await _rep.GetByIdWithUser(expenseToUpdate.Id);

            await _rep.Update(_mapper.Map(expenseToUpdate, expense));

            return Ok(_mapper.Map<ExpenseToReturn>(await _rep.GetByIdWithUser(expenseToUpdate.Id)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _rep.Delete(id);

            return Ok();
        }
        [HttpGet("{userId}/{index}/{length}")]
        public async Task<IActionResult> GetByUserId(int userId, int index, int length)
        {
            var expenses = await _rep.GetByUserId(userId);

            return Ok(new Pagination<ExpenseToReturn>(_mapper.Map<IEnumerable<ExpenseToReturn>>(expenses), index, length));
        }
    }
}