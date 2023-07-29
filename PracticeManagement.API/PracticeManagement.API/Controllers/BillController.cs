using Microsoft.AspNetCore.Mvc;
using PracticeManagement.API.EC;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Models;
using PracticeManagement.Library.Services.Utilities;

namespace PracticeManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        private readonly ILogger<BillController> _logger;

        public BillController(ILogger<BillController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetBills")]
        public IEnumerable<BillDTO> Get()
        {
            return new BillEC().Search();
        }

     

        [HttpDelete("Delete/{id}")]
        public BillDTO? Delete(int id)
        {
            return new BillEC().Delete(id);
        }

        [HttpPost]
        public BillDTO AddOrUpdate([FromBody] BillDTO dto)
        {
            return new BillEC().AddOrUpdate(dto);
        }

        [HttpPost("Search")]
        public IEnumerable<BillDTO> Search([FromBody] QueryMessage query)
        {
            return new BillEC().Search(query.Query);
        }
    }
}