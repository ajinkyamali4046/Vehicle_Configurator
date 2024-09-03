using Microsoft.AspNetCore.Mvc;
using v_conf_dn.Models;

namespace v_conf_dn.Services
{
    public interface ISegmentService
    {
        Task<ActionResult<Segment>> add(Segment segment);

        Task<ActionResult<IEnumerable<Segment>>> getAllSegments();
    }
}
