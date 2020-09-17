using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementations {
    public class ChitthiPurjiDartaRepository : GenericRepository<ChitthiPurjiDarta>, IPurjiDarta {
        private readonly DartaDbContext context;

        public ChitthiPurjiDartaRepository (DartaDbContext context) : base (context) {
            this.context = context;

        }

        public async Task<IEnumerable<ChitthiPurjiDarta>> GetAllPurjiDarta () {

            var data = await context.chitthiPurjiDartas
                .Include (p => p.subject)
                .Include (p => p.DartaType)
                .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .ToListAsync ();
            return data;

        }

        public async Task<ChitthiPurjiDarta> GetDartaPatraAsync (int id) {
            var data = await context.chitthiPurjiDartas
                .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .FirstOrDefaultAsync (p=>p.Id==id);
            return data;

        }

        public async Task<ChitthiPurjiDarta> GetPurjiDartaWithpatrasById (int id) {

            var data = await context.chitthiPurjiDartas.FindAsync (id);
            await context.Entry (data).Reference (p => p.subject).LoadAsync ();
            await context.Entry (data).Reference (p => p.DartaType).LoadAsync ();
            await context.Entry (data).Collection (p => p.patras).LoadAsync ();

            return data;
        }

    }
}