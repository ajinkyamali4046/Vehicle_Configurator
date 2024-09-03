using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using v_conf_dn.Services;

namespace v_conf_dn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: api/vehicles/S/{modelId}
        [HttpGet("S/{modelId}")]
        public async Task<IActionResult> GetVehiclesByModelId(long modelId)
        {
            var vehicles = await _vehicleService.GetVehiclesByModelIdAsync(modelId);

            if (vehicles == null || !vehicles.Any())
            {
                return NotFound();
            }

            return Ok(vehicles);
        }




        [HttpGet("E/{modelId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetVehiclesByModelIdExt(long modelId)
        {
            var vehicles = await _vehicleService.GetVehiclesByModelIdAsyncExt(modelId);
            return Ok(vehicles);
        }



        [HttpGet("I/{modelId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetVehiclesByModelIdInt(long modelId)
        {
            var vehicles = await _vehicleService.GetVehiclesByModelIdAsyncInt(modelId);
            return Ok(vehicles);
        }




        [HttpGet("C/{modelId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetVehiclesByModelIdCor(long modelId)
        {
            var vehicles = await _vehicleService.GetVehiclesByModelIdAsyncCor(modelId);
            return Ok(vehicles);
        }


        [HttpGet("config/{modelId}/{isConfigurable}")]
        public async Task<IActionResult> GetConfigurableComponents(long modelId, string isConfigurable)
        {
            var components = await _vehicleService.GetConfigurableComponentsAsync(modelId, isConfigurable);
            if (components == null || !components.Any())
            {
                return NotFound("No configurable components found for the specified model.");
            }

            return Ok(components);
        }
    }
}
