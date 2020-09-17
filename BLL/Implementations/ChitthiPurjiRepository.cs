using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementations {
    public class ChitthiPurjiRepository : GenericRepository<ChitthiPurji>, IChitthiPurji {
        private readonly DartaDbContext context;

        public ChitthiPurjiRepository (DartaDbContext context) : base (context) {
            this.context = context;

        }

        public async Task EditPurjiAsync (ChitthiPurji entity) {

            if (entity.patras.Any ()) {
                entity.patras.Add (new ChalanPatras () {

                });
            }
            await Task.Run (() => context.ChitthiPurjis.Update (entity));
        }

        public async Task<IEnumerable<ChitthiPurji>> GetAllPurji () {

            var data = await context.ChitthiPurjis
                .Include (p => p.subject)
                .Include (p => p.chalan)
                .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .ToListAsync ();
            return data;

        }

        public async Task<ChitthiPurji> GetPurjiWithpatrasById (int id) {

            var data = await context.ChitthiPurjis 
                 .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .FirstOrDefaultAsync (p=>p.Id==id);
          
            return data;
        }
    
    
    }
}