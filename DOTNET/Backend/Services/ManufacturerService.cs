using Microsoft.EntityFrameworkCore;
using v_conf_dn.Models;
using v_conf_dn.Repository;
using v_conf_dn.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly VehicleDBContext vehicleDBContext;

    public ManufacturerService(VehicleDBContext vehicleDBContext)
    {
        this.vehicleDBContext = vehicleDBContext;
    }

    public async Task<IEnumerable<Manufacturer>> GetManufacturersBySegmentId(long segmentId)
    {
        // Fetch the Segment entity using the provided segmentId
        var segment = await vehicleDBContext.Segments
            .FirstOrDefaultAsync(s => s.Id == segmentId);

        if (segment == null)
        {
            Console.WriteLine("Segment not found for ID: " + segmentId);
            return Enumerable.Empty<Manufacturer>();
        }

        Console.WriteLine("Segment found: " + segment.SegName);

        // Fetch manufacturers associated with the fetched segment
        var manufacturers = await vehicleDBContext.Manufacturers
            .Where(m => m.Segment == segment)
            .ToListAsync();

        Console.WriteLine("Manufacturers found: " + manufacturers.Count);

        return manufacturers;
    }

}
