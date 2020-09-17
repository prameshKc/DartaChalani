using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;

namespace BLL.Abstract
{
    public interface IChitthiPurji:IGenericRepository<ChitthiPurji>
    {
         Task<IEnumerable<ChitthiPurji>> GetAllPurji();

         Task EditPurjiAsync(ChitthiPurji entity);

         Task<ChitthiPurji> GetPurjiWithpatrasById(int id);
    }
}