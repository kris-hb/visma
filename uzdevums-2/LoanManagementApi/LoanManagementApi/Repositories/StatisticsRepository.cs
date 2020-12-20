using LoanManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LoanManagementApi.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ManagementDBContext _ctx;

        public StatisticsRepository(ManagementDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Balance> GetUserBalancceStatus(int id)
   {
            var query = await
               (from u in _ctx.Users
                where u.UserId == id
                select new { u.GivenLoans, u.TakenLoans })
               .FirstOrDefaultAsync();

            if (query == null)
            {
                return null;
            }

            var gLoans = query.GivenLoans != null ? query.GivenLoans.ToList().Sum(g => g.Amount) : 0;
            var tLoans = query.TakenLoans != null ? query.TakenLoans.ToList().Sum(t => t.Amount) : 0;

            var result = new Balance
            {
                Status = gLoans == tLoans ? BalanceStatus.Zero : gLoans > tLoans ? BalanceStatus.Positive : BalanceStatus.Negative
            };

            return result;
        }

        public async Task<UserInfo> GetBiggestBorrower()
        
        {

            var query = from user in _ctx.Users
                        join loan in _ctx.Loans on user.UserId equals loan.BorrowerId
                        select new { UserName = user.Username, Amount = loan.Amount };

            var result = query
                .GroupBy(u => u.UserName)
                .Select(g => new UserInfo { Username = g.Key, Amount = g.Sum(l => l.Amount) })
                .OrderByDescending(l => l.Amount);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<UserInfo> GetBiggestLender()
        {
            var query = from user in _ctx.Users
                        join loan in _ctx.Loans on user.UserId equals loan.LenderId
                        select new { UserName = user.Username, Amount = loan.Amount };

            var result = query
                .GroupBy(u => u.UserName)
                .Select(g => new UserInfo { Username = g.Key, Amount = g.Sum(l => l.Amount) })
                .OrderByDescending(l => l.Amount);
                

            return await result.FirstOrDefaultAsync(); ;
        }

        public async Task<List<UserInfo>> GetAvaregeDebt()
        {
            var query = from user in _ctx.Users
                        join loan in _ctx.Loans on user.UserId equals loan.BorrowerId
                        select new { UserName = user.Username, Amount = loan.Amount };

            var result = query
                .GroupBy(x => x.UserName)
                .Select(g => new UserInfo { Username = g.Key, Amount = g.Average(x => x.Amount) });

            return await result.ToListAsync(); ;
        }

    }
}
