using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract;
using BLL.UnitOfWork;
using DomainModel;
using DomainModel.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ChalanController : ControllerBase {
        private readonly IMapper _map;
        private readonly IUOW _uow;
        private readonly IGenericRepository<Prefix> _Irepo;
      
        public ChalanController (IMapper _map, IUOW _uow, IGenericRepository<Prefix> _Irepo) {
            this._Irepo = _Irepo;
            this._uow = _uow;
            this._map = _map;

        }

        [HttpGet ("all")]
        public async Task<IActionResult> GetAll () {

            var chalans = await _uow._chalanRepo.GetAllAsync ();
            var res = _map.Map<List<Chalan>, List<ChalanResource>> (chalans.ToList ());
            return Ok (res);
        }

        [HttpPost ("Add")]
        public IActionResult Post (ChalanResource chalan) {

            if (chalan == null) {
                return BadRequest ();
            }

            var newChalan = _map.Map<Chalan> (chalan);
            _uow._chalanRepo.CreateAsync (newChalan).GetAwaiter ().GetResult ();
            _uow.CompleteAsync ().GetAwaiter ().GetResult ();
            return Ok ();
        }

        [HttpPost ("Edit")]
        public IActionResult Update (ChalanResource chalan) {

            if (chalan == null) {
                return BadRequest ();
            }

            var newChalan = _map.Map<Chalan> (chalan);
            _uow._chalanRepo.EditAsync (newChalan).GetAwaiter ().GetResult ();
            _uow.CompleteAsync ().GetAwaiter ().GetResult ();
            return Ok ();
        }

        
    }
}