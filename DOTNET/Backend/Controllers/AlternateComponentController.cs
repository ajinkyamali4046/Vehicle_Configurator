using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using v_conf_dn.Models;
using v_conf_dn.Services;

namespace v_conf_dn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AlternateComponentsController : ControllerBase
    {
        private readonly IAlternateComponent _alternateComponentService;

        public AlternateComponentsController(IAlternateComponent alternateComponentService)
        {
            _alternateComponentService = alternateComponentService;
        }

        [HttpGet("alternate-components/{selectedId}/{compId}")]
        public async Task<ActionResult<IEnumerable<AlternateComponent>>> GetAlternateComponents(long selectedId, long compId)
        {
            var alternateComponents = await _alternateComponentService.GetAlternateComponentsAsync(selectedId, compId);

            if (alternateComponents == null || !alternateComponents.Any())
            {
                return NotFound();
            }

            return Ok(alternateComponents);
        }


        [HttpGet("alt/{modId}/{compId}")]
        public async Task<IActionResult> ShowAltenatedComponents(int modId, int compId)
        {
            try
            {
                var alternateComponent = await _alternateComponentService.FindByModelIdAndCompId(modId, compId);
                return Ok(alternateComponent);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
