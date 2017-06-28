using System.Threading.Tasks;

namespace lns.services.Dashboards
{
    public interface IDashboardService
    {
        Task<Dashboard> GetDashboardSettingAsync();
    }
}
