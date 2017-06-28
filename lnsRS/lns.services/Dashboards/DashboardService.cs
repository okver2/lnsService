using Dapper;
using lns.services.Charts;
using lns.services.SqlServer;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace lns.services.Dashboards
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepository _repo;

        public DashboardService(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<Dashboard> GetDashboardSettingAsync()
        {
            return await _repo.WithConnection(async c =>
            {
                // execute the stored procedure called GetDashboardSetting
                var reader = await c.QueryMultipleAsync("GetDashboardSetting", commandType: CommandType.StoredProcedure);
                // map the result from stored procedure to Dashboard data model
                var results = new Dashboard
                {
                    TotalPositions = reader.ReadAsync<int>().Result.SingleOrDefault(),
                    TotalOffices = reader.ReadAsync<int>().Result.SingleOrDefault(),
                    TotalEmployees = reader.ReadAsync<int>().Result.SingleOrDefault(),
                    EmployeesPerYear = reader.ReadAsync<ChartData>().Result.AsEnumerable(),
                    EmployeesPerOffice = reader.ReadAsync<ChartData>().Result.AsEnumerable()
                };
                return results;
            });
        }
    }
}
