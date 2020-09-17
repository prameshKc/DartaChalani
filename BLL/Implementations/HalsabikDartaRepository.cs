using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL;
using DomainModel.HalsabikDarta;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementations {
    public class HalsabikDartaRepository : GenericRepository<HalsabikDarta>, IHalsabikDarta {
        private readonly DartaDbContext context;

        public HalsabikDartaRepository (DartaDbContext context) : base (context) {
            this.context = context;

        }
        public async Task EditHalsabikAsync (HalsabikDarta entity) {
            if (entity.patras.Any ()) {
                entity.patras.Add (new HalsabikDartaPatras () {

                });
            }
            await Task.Run (() => context.HalsabikDartas.Update (entity));
        }

        public async Task<IEnumerable<HalsabikDarta>> GetAllHalsabik () {
            var data = await context.HalsabikDartas
                .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .ToListAsync ();
            return data;
        }

        public async Task<HalsabikDarta> GetHalsabikWithpatrasById (int id) {
            var data = await context.HalsabikDartas
                .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .FirstOrDefaultAsync (p => p.Id == id);

            return data;
        }
    }
}