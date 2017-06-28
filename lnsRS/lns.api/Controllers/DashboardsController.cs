using lns.services;
using lns.services.Dashboards;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace lns.api.Controllers
{
    [RoutePrefix("api/dashboards")]
    public class DashboardsController : ApiController
    {
        private readonly IDashboardService _service;

        public DashboardsController(IDashboardService service)
        {
            _service = service;
        }

        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IDashboard))]
        public async Task<IHttpActionResult> GetDashboardSetting()
        {
            var result = await _service.GetDashboardSettingAsync();
            return Ok(result);
        }
    }
}
