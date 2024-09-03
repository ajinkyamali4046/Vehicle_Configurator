using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using v_conf_dn.Models;
using v_conf_dn.Services;

namespace v_conf_dn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SegmentController : ControllerBase
    {
        private readonly ISegmentService iref;

        public SegmentController(ISegmentService iref)
        {
            this.iref = iref;
        }

        [HttpPost]
        public async Task<IActionResult> AddSegment(Segment seg)
        {
            await iref.add(seg);
            return CreatedAtAction("AddSegment", new { id = seg.Id }, seg);
        }


       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Segment>>> GetSegments()
        {
            return await iref.getAllSegments();
        }
        
    }
}
