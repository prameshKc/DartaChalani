using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.UnitOfWork;
using DomainModel.Fiscal;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class FiscalYearController : ControllerBase {
        private readonly IUOW uow;
        private readonly IGenericRepository<FiscalYear> fiscal;

        public FiscalYearController (IUOW _uow, IGenericRepository<FiscalYear> _fiscal) {
            uow = _uow;
            fiscal = _fiscal;
        }

        [HttpGet ("All")]
        public async Task<IActionResult> GetFiscalYear () {

            var data = await fiscal.GetAllAsync ();
            return Ok (data);
        }

        [HttpPost ("add")]
        public async Task<IActionResult> AddFiscal (FiscalYear model) {

            await fiscal.CreateAsync (model);
            await uow.CompleteAsync ();
            return Ok ();

        }

        [HttpGet ("AllCounts")]
        public IActionResult GetAllChalanDartaCounts () {

            var chitthiCount = uow._purji.GetAllAsync ().GetAwaiter ().GetResult ().Count ();
            var dartaPurji = uow._purjiDarta.GetAllAsync ().GetAwaiter ().GetResult ().Count ();
            var rekhankanChalan = uow._FiledChalanRepo.GetAllAsync ().GetAwaiter ().GetResult ().Count ();
            var rekhankanDarta = uow._FiledDartaRepo.GetAllAsync ().GetAwaiter ().GetResult ().Count ();
            var halsabikChalan = uow._HalsabikChalani.GetAllAsync ().GetAwaiter ().GetResult ().Count ();
            var halsabikDarta = uow._HalsabikDarta.GetAllAsync ().GetAwaiter ().GetResult ().Count ();

            var all = new { purjiChalan = chitthiCount, puurjiDarta = dartaPurji, fieldChalan = rekhankanChalan, fieldDarta = rekhankanDarta, halsabikChalan = halsabikChalan, halsabikDarta = halsabikDarta };
            return Ok (all);
        }
    }
}