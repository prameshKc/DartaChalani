using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract;
using BLL.UnitOfWork;
using DomainModel;
using DomainModel.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers {
    [Route ("/api/purjidarta")]
    [ApiController]
    public class PurjiDartaController : ControllerBase {

        private readonly IMapper _map;
        private readonly IUOW _uow;
        private readonly IWebHostEnvironment env;
        private readonly IGenericRepository<Prefix> _Irepo;
        private readonly IGenericRepository<PurjiDartaFiles> _IFileRepo;
        private readonly IGenericRepository<Subject> _ISubRepo;
        private readonly IGenericRepository<Dartas> _darta;

        public PurjiDartaController (IMapper _map,
            IUOW _uow,
            IWebHostEnvironment _env,
            IGenericRepository<Prefix> _Irepo,
            IGenericRepository<PurjiDartaFiles> _IFileRepo,
            IGenericRepository<Dartas> _darta,
            IGenericRepository<Subject> iSubRepo) {
            this._Irepo = _Irepo;
            this._darta = _darta;
            this._IFileRepo = _IFileRepo;
            this._uow = _uow;
            env = _env;
            this._map = _map;
            _ISubRepo = iSubRepo;
        }

        [HttpGet ("All")]
        public IActionResult GetAllPurjis () {

            var domain = _uow._purjiDarta.GetAllPurjiDarta ().GetAwaiter ().GetResult ();
            //var files= d

            var res = _map.Map<List<ChitthiPurjiDarta>, List<ChitthiPurjiDartaResource>> (domain.ToList ());
            return Ok (res);
        }

        [HttpGet ("darta")]
        public IActionResult GetAllDartas () {
            var darta = _darta.GetAllAsync ().GetAwaiter ().GetResult ();
            var res = _map.Map<List<DartaResource>> (darta.ToList ());
            return Ok (res);
        }

        [HttpPost ("Add")]
        public async Task<IActionResult> Post ([FromForm] ChitthiPurjiDartaResource model) {

            if (model != null) {

                var domainPurji = _map.Map<ChitthiPurjiDarta> (model);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "purji_darta");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var dartaFile = new PurjiDartaFiles () {
                            fileUrl = fileName
                        };

                        domainPurji.patras.Add (new ChitthiDartaPatras () {
                            file = dartaFile
                        });

                    }

                }
                domainPurji.subject = _ISubRepo.FilterAsync (p => p.Id == model.subjectId)
                    .GetAwaiter ().GetResult ().FirstOrDefault ();

                await _uow._purjiDarta.CreateAsync (domainPurji);
                await _uow.CompleteAsync ();
                var res = _map.Map<ChitthiPurjiDartaResource> (domainPurji);
                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpPost ("Edit")]
        public async Task<IActionResult> Update ([FromForm] ChitthiPurjiDartaResource model) {

            if (model != null) {

                var domainPurji = await _uow._purjiDarta.GetById (model.Id);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "purji_darta");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var purjiFile = new PurjiDartaFiles () {
                            fileUrl = fileName
                        };
                        var addedFile = _map.Map<PurjiDartaFileResource> (purjiFile);
                        model.patras.Add (new PurjiDartaPatraResource () {
                            file = addedFile
                        });

                    }

                }

                _map.Map<ChitthiPurjiDartaResource, ChitthiPurjiDarta> (model, domainPurji);
                await _uow._purjiDarta.EditAsync (domainPurji);
                await _uow.CompleteAsync ();

                var res = _map.Map<ChitthiPurjiDartaResource> (_uow._purjiDarta.GetAllPurjiDarta ().GetAwaiter ().GetResult ().FirstOrDefault (p => p.Id == model.Id));

                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpDelete ("Delete/{id}")]
        public async Task<IActionResult> Delete (int id) {

            if (id == 0)
                return NotFound ();

            var purji = await _uow._purjiDarta.GetById (id);
            await _uow._purjiDarta.DeleteAsync (purji);
           await  _uow.CompleteAsync();
            return Ok ();
        }

        [HttpDelete ("DeleteFile/{did}/{fid}")]
        public async Task<IActionResult> DeleteDartaFile (int did, int fid) {

            var patra = await _uow._purjiDarta.GetDartaPatraAsync (did);
            var file = patra.patras.Find (p => p.fileId == fid);
            patra.patras.Remove (file);
            await _uow.CompleteAsync ();

            return Ok ();
        }

        [HttpGet ("Prefix")]
        public IActionResult GetPrefix () {

            var lastPrefix = _Irepo.FilterAsync (p => p.type == "purji-darta")
                .GetAwaiter ()
                .GetResult ()
                .OrderByDescending (p => p.startIndex)
                .FirstOrDefault ();

            return Ok (new { lastPrefix = lastPrefix.startIndex + 1});
        }
    }
}