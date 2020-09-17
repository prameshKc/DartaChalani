using System.Threading.Tasks;
using BLL.Abstract;
using BLL.Implementations;
using DAL;

namespace BLL.UnitOfWork {
    public class UOW : IUOW {
        private readonly DartaDbContext context;
        public UOW (DartaDbContext context) {
            this.context = context;
            _chalanRepo = new ChalanRepository (context);
            _purji = new ChitthiPurjiRepository (context);
            _purjiDarta = new ChitthiPurjiDartaRepository (context);
            _FiledChalanRepo = new FieldRekhankanChalanRepository (context);
            _FiledDartaRepo = new FieldRekhankanDartaRepository (context);
            _HalsabikChalani = new HalsabikChalaniRepository (context);
            _HalsabikDarta = new HalsabikDartaRepository (context);
            _Isite = new SiteSettingRepository (context);
        }

        public IChalan _chalanRepo { get; private set; }

        public IGenericRepository<object> _IRepo { get; private set; }

        public IChitthiPurji _purji { get; private set; }

        public IPurjiDarta _purjiDarta { get; private set; }

        public IFieldChalan _FiledChalanRepo { get; private set; }

        public IFieldDarta _FiledDartaRepo { get; private set; }

        public IHalsabikChalani _HalsabikChalani { get; private set; }

        public IHalsabikDarta _HalsabikDarta { get; private set; }

        public ISiteSettingRepository _Isite { get; private set; }

        public async Task<int> CompleteAsync () {
            return await context.SaveChangesAsync ();
        }

        public void Dispose () {
            this.context.Dispose ();
        }
    }
}