using Microsoft.AspNetCore.Mvc;
using sample_dotnet_6_0.Dto;
using sample_dotnet_6_0.Services;

namespace sample_dotnet_6_0.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeesService _employeesService;

        public EmployeesController(
            ILogger<EmployeesController> logger,
            IEmployeesService employeesService)
        {
            _logger = logger;
            _employeesService = employeesService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAsync() =>
            Ok(await _employeesService.GetAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var employee = await _employeesService.GetAsync(id);

            return employee is not null
                ? Ok(employee)
                : NotFound(id);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAsync([FromBody] EmployeeNewDto employee)
        {
            var isAdded = await _employeesService.AddAsync(employee);

            return isAdded
                ? NoContent()
                : BadRequest(employee);
        }

        [HttpPost]
        [Route("bulk")]
        public async Task<IActionResult> AddAsync([FromBody] IEnumerable<EmployeeNewDto> employees)
        {
            var isAdded = await _employeesService.AddAsync(employees);

            return isAdded
                ? NoContent()
                : BadRequest(employees);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAsync([FromBody] EmployeeUpdateDto employee)
        {
            var isExist = await _employeesService.GetAsync(employee.Id);

            if (isExist is null) return NotFound(employee.Id);

            var isUpdated = await _employeesService.UpdateAsync(employee);

            return isUpdated
                ? NoContent()
                : BadRequest(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeteleAsync([FromRoute] Guid id)
        {
            var isDeleted = await _employeesService.DeleteAsync(id);

            return isDeleted
                ? NoContent()
                : NotFound(id);
        }
    }
}
