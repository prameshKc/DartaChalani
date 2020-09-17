using System;
using System.Threading.Tasks;
using BLL.Abstract;

namespace BLL.UnitOfWork {
    public interface IUOW : IDisposable {

       public IChalan _chalanRepo { get; }
       public IChitthiPurji _purji { get; }
       public IPurjiDarta _purjiDarta { get; }
       public IFieldChalan _FiledChalanRepo { get; }
       public IFieldDarta _FiledDartaRepo { get; }
       public IHalsabikChalani _HalsabikChalani { get; }
       public IHalsabikDarta _HalsabikDarta { get; }
       public ISiteSettingRepository _Isite { get; }
        Task<int> CompleteAsync ();
    }
}