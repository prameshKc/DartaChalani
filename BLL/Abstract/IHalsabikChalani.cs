using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.HalsabikChalani;

namespace BLL.Abstract
{
    public interface IHalsabikChalani:IGenericRepository<HalsabikChalani>
    {
         
         Task<IEnumerable<HalsabikChalani>> GetAllHalsabik();

         Task EditHalsabikAsync(HalsabikChalani entity);

         Task<HalsabikChalani> GetHalsabikWithpatrasById(int id);
    }
}