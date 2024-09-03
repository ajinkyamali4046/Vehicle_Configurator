using v_conf_dn.Models;

namespace v_conf_dn.Services
{
    public interface IComponentService
    {
        Task<Component> GetComponentByIdAsync(long id);
    }
}
