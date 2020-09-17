using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL;
using DomainModel.FieldRekhankanDarta;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementations {
    public class FieldRekhankanDartaRepository : GenericRepository<FieldRekhankanDarta>, IFieldDarta {
        private readonly DartaDbContext context;

        public FieldRekhankanDartaRepository (DartaDbContext context) : base (context) {
            this.context = context;
        }
        public async Task EditRekhankanAsync (FieldRekhankanDarta entity) {
            if (entity.patras.Any ()) {
                entity.patras.Add (new RekhankanDartaPatras () {

                });
            }
            await Task.Run (() => context.FieldRekhankanDartas.Update (entity));
        }

        public async Task<IEnumerable<FieldRekhankanDarta>> GetAllFieldRekhankan () {
            var data = await context.FieldRekhankanDartas

                .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .ToListAsync ();
            return data;

        }

         public async Task<FieldRekhankanDarta> GetFieldWithpatrasById (int id) {
             var data = await context.FieldRekhankanDartas.Include (p => p.patras)
                .ThenInclude (t => t.file)
                .FirstOrDefaultAsync(p=>p.Id==id);
          
            return data;
        }
    }
}