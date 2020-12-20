using LoanManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementApi.Repositories
{
    public interface IStatisticsRepository
    {
        Task<Balance> GetUserBalancceStatus(int id);
        Task<UserInfo> GetBiggestBorrower();
        Task<UserInfo> GetBiggestLender();
        Task<List<UserInfo>> GetAvaregeDebt();
    }
}
