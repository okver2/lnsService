using lns.services.Employees;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace lns.api.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEmployee))]
        public async Task<IHttpActionResult> GetEmployees()
        {
            var result = await _service.GetEmployeesAsync();
            return Ok(result);
        }
    }
}
