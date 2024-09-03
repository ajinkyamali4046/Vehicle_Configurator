using Microsoft.EntityFrameworkCore;
using v_conf_dn.Models;
using v_conf_dn.Repository;

namespace v_conf_dn.Services
{
    public class ComponentService:IComponentService
    {
        private readonly VehicleDBContext _dbContext;

        public ComponentService(VehicleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public async Task<Component> GetComponentByIdAsync(long id)
        {
            return await _dbContext.Components.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
