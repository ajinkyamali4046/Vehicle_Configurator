using Microsoft.AspNetCore.Mvc;
using v_conf_dn.Dto;
using v_conf_dn.Models;

namespace v_conf_dn.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetVehiclesByModelIdAsync(long modelId);


        Task<IEnumerable<VehicleDto>> GetVehiclesByModelIdAsyncExt(long modelId);

        Task<IEnumerable<VehicleDto>> GetVehiclesByModelIdAsyncInt(long modelId);

        Task<IEnumerable<VehicleDto>> GetVehiclesByModelIdAsyncCor(long modelId);

        Task<IEnumerable<VehicleDto>> GetConfigurableComponentsAsync(long modelId, string isConfigurable);

    }
}
