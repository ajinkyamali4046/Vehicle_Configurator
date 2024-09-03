using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using v_conf_dn.Dto;
using v_conf_dn.Models;
using v_conf_dn.Repository;

namespace v_conf_dn.Services
{
    public class AlternateComponentService : IAlternateComponent
    {
        private readonly VehicleDBContext _context;

        public AlternateComponentService(VehicleDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlternateComponentDto>> GetAlternateComponentsAsync(long selectedId, long compId)
        {
            var alternateComponents = await _context.AlternateComponents
                .Include(ac => ac.AltComp)
                .Include(ac => ac.Component)
                .Where(ac => ac.ModId == selectedId && ac.CompId == compId)
                .ToListAsync();

            var alternateComponentDtos = alternateComponents.Select(ac => new AlternateComponentDto
            {
                Id = ac.Id,
                DeltaPrice = ac.DeltaPrice,
                ModId = ac.ModId,
                CompId = ac.CompId,
                AltCompId = ac.AltCompId,
                ComponentName = ac.AltComp?.CompName, // Mapping Component name
                AlternateComponentName = ac.AltComp?.CompName // Mapping Alternate Component name
            });

            return alternateComponentDtos;
        }


        public async Task<AlternateComponentDto> FindByModelIdAndCompId(long modId, long compId)
        {
            var componentDto = await _context.AlternateComponents
                .Where(ac => ac.ModId == modId && ac.CompId == compId)
                .Select(ac => new AlternateComponentDto
                {
                    Id = ac.Id,
                    DeltaPrice = ac.DeltaPrice,
                    ModId = ac.ModId,
                    CompId = ac.CompId,
                    AltCompId = ac.AltCompId,
                    ComponentName = ac.Component != null ? ac.Component.CompName : null,  // Ensure 'Name' is a property in Component
                    AlternateComponentName = ac.AltComp != null ? ac.AltComp.CompName : null  // Ensure 'Name' is a property in Component
                })
                .FirstOrDefaultAsync();

            return componentDto;
        }






    }
}

