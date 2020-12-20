using LoanManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementApi.Repositories
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllLoans();
        Task<Loan> GetLoanById(int id);
        Task<Loan> CreateLoan(Loan loan);
    }
}
