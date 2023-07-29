using Microsoft.AspNetCore.Mvc;
using PracticeManagement.API.EC;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Services.Utilities;

namespace PracticeManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly ILogger<TimeController> _logger;

        public TimeController(ILogger<TimeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetTimes")]
        public IEnumerable<TimeDTO> Get()
        {
            return new TimeEC().Search();
        }


        [HttpDelete("Delete/{id}")]
        public TimeDTO? Delete(int id)
        {
            return new TimeEC().Delete(id);
        }

        [HttpPost]
        public TimeDTO AddOrUpdate([FromBody] TimeDTO dto)
        {
            return new TimeEC().AddOrUpdate(dto);
        }

        [HttpPost("Search")]
        public IEnumerable<TimeDTO> Search([FromBody] QueryMessage query)
        {
            return new TimeEC().Search(query.Query);
        }
    }
}