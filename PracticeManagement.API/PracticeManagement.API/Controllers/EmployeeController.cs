using Microsoft.AspNetCore.Mvc;
using PracticeManagement.API.EC;
using PracticeManagement.Library.DTO;

namespace PracticeManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetEmployees")]
        public IEnumerable<EmployeeDTO> Get()
        {
            return new EmployeeEC().Search();
        }

        //[HttpGet("GetClients/{id}")]
        //public ClientDTO? GetId(int id)
        //{
        //    return new ClientEC().Get(id);
        //}

        [HttpDelete("Delete/{id}")]
        public EmployeeDTO? Delete(int id)
        {
            return new EmployeeEC().Delete(id);
        }

        [HttpPost]
        public EmployeeDTO AddOrUpdate([FromBody] EmployeeDTO dto)
        {
            return new EmployeeEC().AddOrUpdate(dto);
        }
    }
}
