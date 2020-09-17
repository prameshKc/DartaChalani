using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract;
using BLL.UnitOfWork;
using DomainModel;
using DomainModel.HalsabikChalani;
using DomainModel.Resources;
using DomainModel.Resources.HalsabikChalaniResource;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class HalsabikChalanController : ControllerBase {
        private readonly IMapper _map;
        private readonly IUOW _uow;
        private readonly IWebHostEnvironment env;
        private readonly IGenericRepository<Prefix> _Irepo;
        private readonly IGenericRepository<HalsabikChalaniFile> _IFileRepo;
        private readonly IGenericRepository<Subject> _ISubRepo;

        public HalsabikChalanController (IMapper _map,
            IUOW _uow,
            IWebHostEnvironment _env,
            IGenericRepository<Prefix> _Irepo,
            IGenericRepository<HalsabikChalaniFile> _IFileRepo, IGenericRepository<Subject> iSubRepo = null) {
            this._Irepo = _Irepo;
            this._IFileRepo = _IFileRepo;
            this._uow = _uow;
            env = _env;
            this._map = _map;
            _ISubRepo = iSubRepo;
        }

        [HttpGet ("All")]
        public IActionResult GetAllHalsabikChalani () {

            var domain = _uow._HalsabikChalani.GetAllHalsabik ().GetAwaiter ().GetResult ();
            //var files= d

            var res = _map.Map<List<HalsabikChalani>, List<HalsabikChalaniResource>> (domain.ToList ());
            return Ok (res);
        }

        [HttpPost ("Add")]
        public async Task<IActionResult> Post ([FromForm] HalsabikChalaniResource model) {

            if (model != null) {

                var domainPurji = _map.Map<HalsabikChalani> (model);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "halsabik_chalani");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var chalanFile = new HalsabikChalaniFile () {
                            fileUrl = fileName
                        };

                        domainPurji.patras.Add (new HalsabikChalaniPatras () {
                            file = chalanFile
                        });

                    }

                }
                domainPurji.subject = _ISubRepo.FilterAsync (p => p.Id == model.subjectId)
                    .GetAwaiter ().GetResult ().FirstOrDefault ();

                await _uow._HalsabikChalani.CreateAsync (domainPurji);
                await _uow.CompleteAsync ();
                var res = _map.Map<HalsabikChalaniResource> (domainPurji);
                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpPost ("Edit")]
        public async Task<IActionResult> Update ([FromForm] HalsabikChalaniResource model) {

            if (model != null) {

                var domainPurji = await _uow._HalsabikChalani.GetById (model.Id);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "halsabik_chalani");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var chalanFile = new HalsabikChalaniFile () {
                            fileUrl = fileName
                        };
                        var addedFile = _map.Map<HalsabikChalaniFileResource> (chalanFile);
                        model.patras.Add (new HalsabikChalaniPatraResource () {
                            file = addedFile
                        });

                    }

                }

                _map.Map<HalsabikChalaniResource, HalsabikChalani> (model, domainPurji);
                await _uow._HalsabikChalani.EditAsync (domainPurji);
                await _uow.CompleteAsync ();

                var res = _map.Map<HalsabikChalaniResource> (_uow._HalsabikChalani.GetAllHalsabik ().GetAwaiter ().GetResult ().FirstOrDefault (p => p.Id == model.Id));

                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpDelete ("Delete/{id}")]
        public async Task<IActionResult> Delete (int id) {

            if (id == 0)
                return NotFound ();

            var purji = await _uow._HalsabikChalani.GetById (id);
            await _uow._HalsabikChalani.DeleteAsync (purji);
            await _uow.CompleteAsync ();
            return Ok ();
        }

        [HttpDelete ("DeleteFile/{did}/{fid}")]
        public async Task<IActionResult> DeleteHalsabikChalaniFile (int did, int fid) {

            var patra = await _uow._HalsabikChalani.GetHalsabikWithpatrasById (did);
            var file = patra.patras.Find (p => p.fileId == fid);
            patra.patras.Remove (file);
            await _uow.CompleteAsync ();
            return Ok ();
        }

        [HttpGet ("Prefix")]
        public IActionResult GetPrefix () {

            var lastPrefix = _Irepo.FilterAsync (p => p.type == "halsabik-chalani")
                .GetAwaiter ()
                .GetResult ()
                .OrderByDescending (p => p.startIndex)
                .FirstOrDefault ();

            return Ok (new { lastPrefix = lastPrefix.startIndex + 1 });
        }
    }
}