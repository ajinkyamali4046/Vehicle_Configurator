using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using v_conf_dn.Models;
using v_conf_dn.Repository;

namespace v_conf_dn.Services
{
    public class ModelService : IModelService
    {
        private readonly VehicleDBContext vehicleDBContext;

        public ModelService(VehicleDBContext vehicleDBContext)
        {
            this.vehicleDBContext = vehicleDBContext;
        }

        public IEnumerable<object> GetModelsBySegmentAndManufacturer(long segmentId, long manufacturerId)
        {
            var result = vehicleDBContext.Models
                .Include(m => m.Segment)
                .Include(m => m.Manufacturer)
                .Where(m => m.Segment.Id == segmentId && m.Manufacturer.Id == manufacturerId)
                .Select(m => new
                {
                    m.Id,
                    m.ImagePath,
                    Manufacturer = new
                    {
                        m.Manufacturer.Id,
                        Name = m.Manufacturer.ManuName
                    },
                    m.MinQty,
                    m.ModName,
                    m.Price,
                    m.SafetyRating,
                    Segment = new
                    {
                        m.Segment.Id,
                        Name = m.Segment.SegName
                    }
                })
                .ToList();

            return result;
        }

        public async Task<object> GetModelByIdAsync(long id)
        {
            var model = await vehicleDBContext.Models
                .Include(m => m.Manufacturer)
                .Include(m => m.Segment)
                .Where(m => m.Id == id)
                .Select(m => new
                {
                    Id = m.Id,
                    ImagePath = m.ImagePath,
                    Manufacturer = new
                    {
                        Id = m.Manufacturer.Id,
                        Name = m.Manufacturer.ManuName // Use ManuName from Manufacturer
                    },
                    MinQty = m.MinQty,
                    ModName = m.ModName,
                    Price = m.Price,
                    SafetyRating = m.SafetyRating,
                    Segment = new
                    {
                        Id = m.Segment.Id,
                        Name = m.Segment.SegName // Use SegName from Segment
                    }
                })
                .FirstOrDefaultAsync();

            return model;
        }




    }
}
