using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract;
using BLL.UnitOfWork;
using DomainModel;
using DomainModel.Resources;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    
    [Route("api/darta")]
    [ApiController]
    public class DartaController:ControllerBase
    {
              private readonly IMapper _map;
        private readonly IUOW _uow;
        private readonly IGenericRepository<Dartas> _Irepo;

        public DartaController (IMapper _map, IUOW _uow, IGenericRepository<Dartas> _Irepo) {
            this._Irepo = _Irepo;
            this._uow = _uow;
            this._map = _map;

        }

        [HttpGet ("All")]
        public IActionResult GetAllDarta () {
            var darta = _Irepo.GetAllAsync ().GetAwaiter ().GetResult ();
            var res = _map.Map<List<DartaResource>> (darta.ToList ());
            return Ok (res);
        }

        [HttpPost ("Add")]
        public IActionResult Post (DartaResource darta) {

            if (darta == null) {
                return BadRequest ();
            }

            var newDarta = _map.Map<Dartas> (darta);
            _Irepo.CreateAsync (newDarta).GetAwaiter ().GetResult ();
            _uow.CompleteAsync ().GetAwaiter ().GetResult ();
            return Ok ();
        }

        [HttpPut ("Edit/{id}")]
        public async Task<IActionResult> Update (int id,[FromBody] DartaResource darta) {

            if (darta == null) {
                return BadRequest ();
            }

            var domain = await _Irepo.GetById (id);
            _map.Map<DartaResource, Dartas> (darta, domain);
            //  _Irepo.EditAsync (newSubject).GetAwaiter ().GetResult ();
            _uow.CompleteAsync ().GetAwaiter ().GetResult ();
            return Ok ();
        }
    }
}