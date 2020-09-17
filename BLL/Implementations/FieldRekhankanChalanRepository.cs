using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementations {
    public class FieldRekhankanChalanRepository : GenericRepository<FieldRekhankanChalani>, IFieldChalan {
        private readonly DartaDbContext context;

        public FieldRekhankanChalanRepository (DartaDbContext context) : base (context) {
            this.context = context;
        }
        public async Task EditRekhankanAsync (FieldRekhankanChalani entity) {
            if (entity.patras.Any ()) {
                entity.patras.Add (new RekhankanChalanPatras () {

                });
            }
            await Task.Run (() => context.fieldRekhankanChalanis.Update (entity));
        }

        public async Task<IEnumerable<FieldRekhankanChalani>> GetAllFieldRekhankan () {
            var data = await context.fieldRekhankanChalanis
                .Include (p => p.subject)
                .Include (p => p.chalan)
                .Include (p => p.patras)
                .ThenInclude (t => t.file)
                .ToListAsync ();
            return data;

        }

        public async Task<FieldRekhankanChalani> GetFieldWithpatrasById (int id) {
             var data = await context.fieldRekhankanChalanis.Include (p => p.patras)
                .ThenInclude (t => t.file)
                .FirstOrDefaultAsync(p=>p.Id==id);
          
            return data;
        }
    }
}