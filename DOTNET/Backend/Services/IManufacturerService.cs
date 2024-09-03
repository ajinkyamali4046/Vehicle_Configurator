using Microsoft.AspNetCore.Mvc;
using v_conf_dn.Models;

namespace v_conf_dn.Services
{
    public interface IManufacturerService
    {
        /*Task<ActionResult<IEnumerable<Manufacturer>>> getAllManufacturers(long sid);*/

        Task<IEnumerable<Manufacturer>> GetManufacturersBySegmentId(long segmentId);
    }
}
