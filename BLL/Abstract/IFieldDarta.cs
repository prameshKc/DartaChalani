using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.FieldRekhankanDarta;

namespace BLL.Abstract
{
    public interface IFieldDarta:IGenericRepository<FieldRekhankanDarta>
    {
         
         Task<IEnumerable<FieldRekhankanDarta>> GetAllFieldRekhankan();

         Task EditRekhankanAsync(FieldRekhankanDarta entity);

         Task<FieldRekhankanDarta> GetFieldWithpatrasById(int id);
    }
}