using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL;
using DomainModel.HalsabikChalani;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementations {
    public class HalsabikChalaniRepository : GenericRepository<HalsabikChalani>, IHalsabikChalani {
        private readonly DartaDbContext context;

        public HalsabikChalaniRepository (DartaDbContext context) : base (context) {
            this.context = context;

        }
        public async Task EditHalsabikAsync (HalsabikChalani entity) {
            if (entity.patras.Any ()) {
                entity.patras.Add (new HalsabikChalaniPatras () {

                });
            }
            await Task.Run (() => context.HalsabikChalanis.Update (entity));
        }

        public async Task<IEnumerable<HalsabikChalani>> GetAllHalsabik () {
            var data = await context.HalsabikChalanis
                .Include (p => p.subject)
                .Include (p => p.chalan)
                .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .ToListAsync ();
            return data;
        }

        public async Task<HalsabikChalani> GetHalsabikWithpatrasById (int id) {
            var data = await context.HalsabikChalanis
                .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .FirstOrDefaultAsync (p => p.Id == id);

            return data;
        }
    }
}