using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using v_conf_dn.Models;
using v_conf_dn.Services;

namespace v_conf_dn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }


        [HttpGet("{id}")]
        public async Task<IEnumerable<Manufacturer>> GetManufacturers(long id)  // Change 'sid' to 'id'
        {
            return await _manufacturerService.GetManufacturersBySegmentId(id);
        }

    }
}
