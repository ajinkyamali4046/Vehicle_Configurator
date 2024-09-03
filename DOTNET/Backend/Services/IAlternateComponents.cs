using v_conf_dn.Dto;
using v_conf_dn.Models;

namespace v_conf_dn.Services
{
    public interface IAlternateComponent
    {
        Task<IEnumerable<AlternateComponentDto>> GetAlternateComponentsAsync(long selectedId, long compId);
        Task<AlternateComponentDto> FindByModelIdAndCompId(long modId, long compId);
    }
}
