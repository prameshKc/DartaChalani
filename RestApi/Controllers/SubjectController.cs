using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class SubjectController : ControllerBase {
        private readonly IMapper _map;
        private readonly IUOW _uow;
        private readonly IGenericRepository<Subject> _Irepo;

        public SubjectController (IMapper _map, IUOW _uow, IGenericRepository<Subject> _Irepo) {
            this._Irepo = _Irepo;
            this._uow = _uow;
            this._map = _map;

        }

        [HttpGet ("All")]
        public IActionResult GetAllSubjects () {
            var subjects = _Irepo.GetAllAsync ().GetAwaiter ().GetResult ();
            var res = _map.Map<List<SubjectResource>> (subjects.ToList ());
            return Ok (res);
        }

        [HttpPost ("Add")]
        public IActionResult Post (SubjectResource subject) {

            if (subject == null) {
                return BadRequest ();
            }

            var newSubject = _map.Map<Subject> (subject);
            _Irepo.CreateAsync (newSubject).GetAwaiter ().GetResult ();
            _uow.CompleteAsync ().GetAwaiter ().GetResult ();
            return Ok ();
        }

        [HttpPut ("Edit/{id}")]
        public async Task<IActionResult> Update (int id,[FromBody] SubjectResource subject) {

            if (subject == null) {
                return BadRequest ();
            }

            var domain = await _Irepo.GetById (id);
            _map.Map<SubjectResource, Subject> (subject, domain);
            //  _Irepo.EditAsync (newSubject).GetAwaiter ().GetResult ();
            _uow.CompleteAsync ().GetAwaiter ().GetResult ();
            return Ok ();
        }

    }
}