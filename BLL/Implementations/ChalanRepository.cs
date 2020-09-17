using BLL.Abstract;
using DAL;
using DomainModel;

namespace BLL.Implementations {
    public class ChalanRepository : GenericRepository<Chalan>, IChalan {
        private readonly DartaDbContext context;

        public ChalanRepository (DartaDbContext context) : base (context) {
            this.context = context;

        }
    }
}