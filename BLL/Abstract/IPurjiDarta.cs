using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;

namespace BLL.Abstract
{
    public interface IPurjiDarta: IGenericRepository<ChitthiPurjiDarta>
    {
         
         Task<IEnumerable<ChitthiPurjiDarta>> GetAllPurjiDarta();

         Task<ChitthiPurjiDarta> GetDartaPatraAsync(int id);

      //   Task EditPurjiDartaAsync(ChitthiPurjiDarta entity);

         Task<ChitthiPurjiDarta> GetPurjiDartaWithpatrasById(int id);
    }
}