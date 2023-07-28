using Microsoft.AspNetCore.Mvc;
using PracticeManagement.API.EC;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Services.Utilities;

namespace PracticeManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetProjects")]
        public IEnumerable<ProjectDTO> Get()
        {
            return new ProjectEc().Search();
        }

        //[HttpGet("GetClients/{id}")]
        //public ClientDTO? GetId(int id)
        //{
        //    return new ClientEC().Get(id);
        //}

        //[HttpDelete("Delete/{id}")]
        //public ClientDTO? Delete(int id)
        //{
        //    return new ClientEC().Delete(id);
        //}

        [HttpPost]
        public ProjectDTO AddOrUpdate([FromBody] ProjectDTO dto)
        {
            return new ProjectEc().AddOrUpdate(dto);
        }
    }
}