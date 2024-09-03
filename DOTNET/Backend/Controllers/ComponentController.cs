using Microsoft.AspNetCore.Mvc;
using v_conf_dn.Services;

namespace v_conf_dn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComponentController:ControllerBase
    {

        private readonly IComponentService _componentService;

        public ComponentController(IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComponentById(long id)
        {
            try
            {
                var component = await _componentService.GetComponentByIdAsync(id);
                if (component == null)
                {
                    return NotFound();
                }
                return Ok(component); // Corrected this line
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
