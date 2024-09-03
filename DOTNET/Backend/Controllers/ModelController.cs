using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using v_conf_dn.Models;
using v_conf_dn.Services;

namespace v_conf_dn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet("{segmentId}/{manufacturerId}")]
        public IActionResult GetModels(long segmentId, long manufacturerId)
        {
            var models = _modelService.GetModelsBySegmentAndManufacturer(segmentId, manufacturerId);
            return Ok(models);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelById(long id)
        {
            var result = await _modelService.GetModelByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


    }
}
