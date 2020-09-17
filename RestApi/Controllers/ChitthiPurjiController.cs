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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers {
    [Route ("api/Purji")]
    [ApiController]
    public class ChitthiPurjiController : ControllerBase {

        private readonly IMapper _map;
        private readonly IUOW _uow;
        private readonly IWebHostEnvironment env;
        private readonly IGenericRepository<Prefix> _Irepo;
        private readonly IGenericRepository<ChalanFiles> _IFileRepo;
        private readonly IGenericRepository<Subject> _ISubRepo;

        public ChitthiPurjiController (IMapper _map,
            IUOW _uow,
            IWebHostEnvironment _env,
            IGenericRepository<Prefix> _Irepo,
            IGenericRepository<ChalanFiles> _IFileRepo, IGenericRepository<Subject> iSubRepo = null) {
            this._Irepo = _Irepo;
            this._IFileRepo = _IFileRepo;
            this._uow = _uow;
            env = _env;
            this._map = _map;
            _ISubRepo = iSubRepo;
        }

        [HttpGet ("All")]
        public IActionResult GetAllPurjis () {
            var domain = _uow._purji.GetAllPurji ().GetAwaiter ().GetResult ();
            //var files= d

            var res = _map.Map<List<ChitthiPurji>, List<ChitthiPurjiResource>> (domain.ToList ());
            return Ok (res);
        }

        [HttpGet ("subjects")]
        public IActionResult GetAllSubjects () {
            var subjects = _ISubRepo.GetAllAsync ().GetAwaiter ().GetResult ();
            var res = _map.Map<List<SubjectResource>> (subjects.ToList ());
            return Ok (res);
        }

        [HttpPost ("Add")]
        public async Task<IActionResult> Post ([FromForm] ChitthiPurjiResource model) {

            if (model != null) {

                var domainPurji = _map.Map<ChitthiPurji> (model);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "purji");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var chalanFile = new ChalanFiles () {
                            fileUrl = fileName
                        };

                        domainPurji.patras.Add (new ChalanPatras () {
                            file = chalanFile
                        });

                    }

                }
                domainPurji.subject = _ISubRepo.FilterAsync (p => p.Id == model.subjectId)
                    .GetAwaiter ().GetResult ().FirstOrDefault ();

                await _uow._purji.CreateAsync (domainPurji);
                await _uow.CompleteAsync ();
                var res = _map.Map<ChitthiPurjiResource> (domainPurji);
                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpPost ("Edit")]
        public async Task<IActionResult> Update ([FromForm] ChitthiPurjiResource model) {

            if (model != null) {

                var domainPurji = await _uow._purji.GetById (model.Id);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "purji");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var chalanFile = new ChalanFiles () {
                            fileUrl = fileName
                        };
                        var addedFile = _map.Map<ChalanFileResource> (chalanFile);
                        model.patras.Add (new ChalanPatrasResource () {
                            file = addedFile
                        });

                    }

                }

                _map.Map<ChitthiPurjiResource, ChitthiPurji> (model, domainPurji);
                await _uow._purji.EditAsync (domainPurji);
                await _uow.CompleteAsync ();

                var res = _map.Map<ChitthiPurjiResource> (_uow._purji.GetAllPurji ().GetAwaiter ().GetResult ().FirstOrDefault (p => p.Id == model.Id));

                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpDelete ("Delete/{id}")]
        public async Task<IActionResult> Delete (int id) {

            if (id == 0)
                return NotFound ();

            var purji = await _uow._purji.GetById (id);
            await _uow._purji.DeleteAsync (purji);
           await  _uow.CompleteAsync();
            return Ok ();
        }

        [HttpDelete ("DeleteFile/{did}/{fid}")]
        public async Task<IActionResult> DeleteDartaFile (int did, int fid) {

            var patra = await _uow._purji.GetPurjiWithpatrasById (did);
            var file = patra.patras.Find (p => p.fileId == fid);
            patra.patras.Remove (file);
            await _uow.CompleteAsync ();
            return Ok ();
        }

        [HttpGet ("Prefix")]
        public IActionResult GetPrefix () {

            var lastPrefix = _Irepo.FilterAsync (p => p.type == "purji")
                .GetAwaiter ()
                .GetResult ()
                .OrderByDescending (p => p.startIndex)
                .FirstOrDefault ();

            return Ok (new { lastPrefix = lastPrefix.startIndex + 1 });
        }
    }
}