using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using v_conf_dn.Models;
using v_conf_dn.Repository;
using v_conf_dn.Dto;

namespace v_conf_dn.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleDBContext dbContext;

        public VehicleService(VehicleDBContext context)
        {
            dbContext = context;
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesByModelIdAsync(long modelId)
        {
            return await dbContext.Vehicles
                .Include(v => v.Component) // Ensure Component is loaded
                .Where(v => v.ModelId == modelId && v.CompType == CompType.S)
                .Select(v => new VehicleDto
                {
                    Id = v.Id,
                    CompType = v.CompType.HasValue ? v.CompType.ToString() : "Unknown",
                    IsConfigurable = v.IsConfigurable.HasValue ? v.IsConfigurable.ToString() : "Unknown",
                    ModelId = v.ModelId,
                    CompName = v.Component != null ? v.Component.CompName : "Unknown",
                    ModId = v.ModelId
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<VehicleDto>> GetVehiclesByModelIdAsyncExt(long modelId)
        {
            return await dbContext.Vehicles
                .Where(v => v.ModelId == modelId && v.CompType == CompType.E)
                .Select(v => new VehicleDto
                {
                    Id = v.Id,
                    CompType = v.CompType.HasValue ? v.CompType.ToString() : "Unknown",
                    IsConfigurable = v.IsConfigurable.HasValue ? v.IsConfigurable.ToString() : "Unknown",
                    ModelId = v.ModelId,
                    CompName = v.Component != null ? v.Component.CompName : "Unknown",
                    ModId = v.ModelId
                })
                .ToListAsync();
        }



        public async Task<IEnumerable<VehicleDto>> GetVehiclesByModelIdAsyncInt(long modelId)
        {
            return await dbContext.Vehicles
                .Where(v => v.ModelId == modelId && v.CompType == CompType.I)
                .Select(v => new VehicleDto
                {
                    Id = v.Id,
                    CompType = v.CompType.HasValue ? v.CompType.ToString() : "Unknown",
                    IsConfigurable = v.IsConfigurable.HasValue ? v.IsConfigurable.ToString() : "Unknown",
                    ModelId = v.ModelId,
                    CompName = v.Component != null ? v.Component.CompName : "Unknown",
                    ModId = v.ModelId
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<VehicleDto>> GetVehiclesByModelIdAsyncCor(long modelId)
        {
            return await dbContext.Vehicles
                .Where(v => v.ModelId == modelId && v.CompType == CompType.C)
                .Select(v => new VehicleDto
                {
                    Id = v.Id,
                    CompType = v.CompType.HasValue ? v.CompType.ToString() : "Unknown",
                    IsConfigurable = v.IsConfigurable.HasValue ? v.IsConfigurable.ToString() : "Unknown",
                    ModelId = v.ModelId,
                    CompName = v.Component != null ? v.Component.CompName : "Unknown",
                    ModId = v.ModelId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<VehicleDto>> GetConfigurableComponentsAsync(long modelId, string isConfigurable)
        {
            // Convert the string to the IsConfigurable enum
            if (!Enum.TryParse<IsConfigurable>(isConfigurable, true, out var isConfigurableEnum))
            {
                throw new ArgumentException("Invalid value for IsConfigurable", nameof(isConfigurable));
            }

            return await dbContext.Vehicles
                .Where(v => v.ModelId == modelId && v.IsConfigurable == isConfigurableEnum)
                .Select(v => new VehicleDto
                {
                    Id = v.Id,
                    CompType = v.CompType.HasValue ? v.CompType.ToString() : "Unknown",
                    IsConfigurable = v.IsConfigurable.HasValue ? v.IsConfigurable.ToString() : "Unknown",
                    ModelId = v.ModelId,
                    CompName = v.Component != null ? v.Component.CompName : "Unknown",
                    ModId = v.ModelId
                })
                .ToListAsync();
        }


    }
}
