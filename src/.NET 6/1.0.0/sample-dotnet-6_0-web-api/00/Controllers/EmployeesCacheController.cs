using Microsoft.AspNetCore.Mvc;
using sample_dotnet_6_0.Dto;
using sample_dotnet_6_0.Services.Cache;

namespace sample_dotnet_6_0.Controllers
{
    [ApiController]
    [Route("cache/employees")]
    public sealed class EmployeesCacheController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeesCacheService _employeesCacheService;
        private readonly IEmployeeStatsService _employeesStatsService;

        public EmployeesCacheController(
            ILogger<EmployeesController> logger,
            IEmployeesCacheService employeesCacheService,
            IEmployeeStatsService employeesStatsService)
        {
            _logger = logger;
            _employeesCacheService = employeesCacheService;
            _employeesStatsService = employeesStatsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAsync() =>
            Ok(await _employeesCacheService.GetAsync());

        [HttpGet]
        [Route("stats")]
        public async Task<IActionResult> GetStatsAsync() =>
            Ok(await _employeesStatsService.GetAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var employee = await _employeesCacheService.GetAsync(id);

            return employee is not null
                ? Ok(employee)
                : NotFound(id);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAsync([FromBody] EmployeeNewDto employee)
        {
            var isAdded = await _employeesCacheService.AddAsync(employee);

            return isAdded
                ? NoContent()
                : BadRequest(employee);
        }

        [HttpPost]
        [Route("bulk")]
        public async Task<IActionResult> AddAsync([FromBody] IEnumerable<EmployeeNewDto> employees)
        {
            var isAdded = await _employeesCacheService.AddAsync(employees);

            return isAdded
                ? NoContent()
                : BadRequest(employees);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAsync([FromBody] EmployeeUpdateDto employee)
        {
            var isExist = await _employeesCacheService.GetAsync(employee.Id);

            if (isExist is null) return NotFound(employee.Id);

            var isUpdated = await _employeesCacheService.UpdateAsync(employee);

            return isUpdated
                ? NoContent()
                : BadRequest(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeteleAsync([FromRoute] Guid id)
        {
            var isDeleted = await _employeesCacheService.DeleteAsync(id);

            return isDeleted
                ? NoContent()
                : NotFound(id);
        }
    }
}
