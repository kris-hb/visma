using System.Collections.Generic;
using System.Threading.Tasks;
using LoanManagementApi.Models;
using LoanManagementApi.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace TransactionManagamentApp.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statistics;

        public StatisticsController(IStatisticsRepository statistics)
        {
            _statistics = statistics;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Balance>> GetUserBalance(int id)
        {
            var user = await _statistics.GetUserBalancceStatus(id);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet]
        public async Task<UserInfo> GetBiggestBorrower()
        {
            return await _statistics.GetBiggestBorrower();
        }

        [HttpGet]
        public async Task<UserInfo> GetBiggestLender()
        {
            return await _statistics.GetBiggestLender();
        }

        [HttpGet]
        public async Task<List<UserInfo>> GetAvarege()
        {
            return await _statistics.GetAvaregeDebt();
        }

    }
}
