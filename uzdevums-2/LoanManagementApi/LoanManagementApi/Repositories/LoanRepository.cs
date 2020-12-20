using LoanManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementApi.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ManagementDBContext _ctx;

        public LoanRepository(ManagementDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Loan> CreateLoan(Loan loan)
        {
            _ctx.Loans.Add(loan);
            await _ctx.SaveChangesAsync();
            return loan;
        }

        public async Task<List<Loan>> GetAllLoans()
        {
            return await _ctx.Loans.ToListAsync();
        }

        public async Task<Loan> GetLoanById(int id)
        {
            return await _ctx.Loans.FirstOrDefaultAsync(loan => loan.LoanId == id);
        }
    }
}
