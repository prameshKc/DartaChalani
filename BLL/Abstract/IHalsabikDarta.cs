using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.HalsabikDarta;

namespace BLL.Abstract
{
    public interface IHalsabikDarta: IGenericRepository<HalsabikDarta>
    {
         
         Task<IEnumerable<HalsabikDarta>> GetAllHalsabik();

         Task EditHalsabikAsync(HalsabikDarta entity);

         Task<HalsabikDarta> GetHalsabikWithpatrasById(int id);
    }
}