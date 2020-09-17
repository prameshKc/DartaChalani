using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL;
using DomainModel.Setting;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementations {
    public class SiteSettingRepository : GenericRepository<SiteSetting>, ISiteSettingRepository {

        private readonly DartaDbContext context;

        public SiteSettingRepository (DartaDbContext context) : base (context) {
            this.context = context;
        }

        public async Task<IEnumerable<SiteSetting>> GetAllSiteWithNames () {
            return await context.siteSettings.Include (p => p.Names).ToListAsync ();
        }
    }
}