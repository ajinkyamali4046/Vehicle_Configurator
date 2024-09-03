using System.Collections.Generic;
using System.Threading.Tasks;
using v_conf_dn.Models;

namespace v_conf_dn.Services
{
    public interface IModelService
    {
        //Task<IEnumerable<Model>> GetModelsBySegmentAndManufacturer(long segmentId, long manufacturerId);

        public IEnumerable<object> GetModelsBySegmentAndManufacturer(long segmentId, long manufacturerId);

        Task<object> GetModelByIdAsync(long id);
    }
}
