using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementApi.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int LenderId { get; set; }
        public int BorrowerId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPayedBack { get; set; }
        public User Lender { get; set; }
        public User Borrower { get; set; }
    }
}
