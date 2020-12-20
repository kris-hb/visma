using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanManagementApi.Models;
using LoanManagementApi.Repositories;

namespace LoanManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _loans;

        public LoansController(ILoanRepository loans)
        {
            _loans = loans;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            return await _loans.GetAllLoans();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _loans.GetLoanById(id);

            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan)
        {
            return await _loans.CreateLoan(loan);

        }
    }
}
