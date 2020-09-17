using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;

namespace BLL.Abstract
{
    public interface IFieldChalan:IGenericRepository<FieldRekhankanChalani>
    {
         
         Task<IEnumerable<FieldRekhankanChalani>> GetAllFieldRekhankan();

         Task EditRekhankanAsync(FieldRekhankanChalani entity);

         Task<FieldRekhankanChalani> GetFieldWithpatrasById(int id);
    }
}