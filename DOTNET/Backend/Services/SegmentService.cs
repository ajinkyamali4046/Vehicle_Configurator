using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using v_conf_dn.Models;
using v_conf_dn.Repository;

namespace v_conf_dn.Services
{
    public class SegmentService : ISegmentService
    {
        private readonly VehicleDBContext vehicleDBContext;

        public SegmentService(VehicleDBContext vehicleDBContext)
        {
            this.vehicleDBContext = vehicleDBContext;
        }

        public async Task<ActionResult<Segment>> add(Segment seg)
        {
           vehicleDBContext.Segments.Add(seg);
            vehicleDBContext.SaveChanges();
            return seg;
        }

        public async Task<ActionResult<IEnumerable<Segment>>> getAllSegments()
        {
            return await vehicleDBContext.Segments.ToListAsync<Segment>();
        }
    }
}
