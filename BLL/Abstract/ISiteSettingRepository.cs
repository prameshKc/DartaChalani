using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Setting;

namespace BLL.Abstract
{
    public interface ISiteSettingRepository:IGenericRepository<SiteSetting>
    {
         

         Task<IEnumerable<SiteSetting>> GetAllSiteWithNames();
    }
}