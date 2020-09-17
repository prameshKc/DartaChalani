using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract;
using BLL.UnitOfWork;
using DomainModel;
using DomainModel.HalsabikDarta;
using DomainModel.Resources.HalsabikDarta;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class HalsabikDartaController : ControllerBase {

        private readonly IMapper _map;
        private readonly IUOW _uow;
        private readonly IWebHostEnvironment env;
        private readonly IGenericRepository<Prefix> _Irepo;
        private readonly IGenericRepository<HalsabikDartaFile> _IFileRepo;

        public HalsabikDartaController (IMapper _map,
            IUOW _uow,
            IWebHostEnvironment _env,
            IGenericRepository<Prefix> _Irepo,
            IGenericRepository<HalsabikDartaFile> _IFileRepo) {
            this._Irepo = _Irepo;
            this._IFileRepo = _IFileRepo;
            this._uow = _uow;
            env = _env;
            this._map = _map;
        }

        [HttpGet ("All")]
        public IActionResult GetAllHalsabikDarta () {

            var domain = _uow._HalsabikDarta.GetAllHalsabik ().GetAwaiter ().GetResult ();
            //var files= d

            var res = _map.Map<List<HalsabikDarta>, List<HalsabikDartaResource>> (domain.ToList ());
            return Ok (res);
        }

        [HttpPost ("Add")]
        public async Task<IActionResult> Post ([FromForm] HalsabikDartaResource model) {

            if (model != null) {

                var domainPurji = _map.Map<HalsabikDarta> (model);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "halsabik_darta");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var chalanFile = new HalsabikDartaFile () {
                            fileUrl = fileName
                        };

                        domainPurji.patras.Add (new HalsabikDartaPatras () {
                            file = chalanFile
                        });

                    }

                }

                await _uow._HalsabikDarta.CreateAsync (domainPurji);
                await _uow.CompleteAsync ();
                var res = _map.Map<HalsabikDartaResource> (domainPurji);
                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpPost ("Edit")]
        public async Task<IActionResult> Update ([FromForm] HalsabikDartaResource model) {

            if (model != null) {

                var domainPurji = await _uow._HalsabikDarta.GetById (model.Id);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "halsabik_darta");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var chalanFile = new HalsabikDartaFile () {
                            fileUrl = fileName
                        };
                        var addedFile = _map.Map<HalsabikDartaFileResource> (chalanFile);
                        model.patras.Add (new HalsabikDartaPatraResource () {
                            file = addedFile
                        });

                    }

                }

                _map.Map<HalsabikDartaResource, HalsabikDarta> (model, domainPurji);
                await _uow._HalsabikDarta.EditAsync (domainPurji);
                await _uow.CompleteAsync ();

                var res = _map.Map<HalsabikDartaResource> (_uow._HalsabikDarta.GetAllHalsabik ().GetAwaiter ().GetResult ().FirstOrDefault (p => p.Id == model.Id));

                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpDelete ("Delete/{id}")]
        public async Task<IActionResult> Delete (int id) {

            if (id == 0)
                return NotFound ();

            var purji = await _uow._HalsabikDarta.GetById (id);
            await _uow._HalsabikDarta.DeleteAsync (purji);
            await _uow.CompleteAsync ();
            return Ok ();
        }

        [HttpDelete ("DeleteFile/{did}/{fid}")]
        public async Task<IActionResult> DeleteHalsabikDartaFile (int did, int fid) {

            var patra = await _uow._HalsabikDarta.GetHalsabikWithpatrasById (did);
            var file = patra.patras.Find (p => p.fileId == fid);
            patra.patras.Remove (file);
            await _uow.CompleteAsync ();
            return Ok ();
        }

        [HttpGet ("Prefix")]
        public IActionResult GetPrefix () {

            var lastPrefix = _Irepo.FilterAsync (p => p.type == "halsabik-darta")
                .GetAwaiter ()
                .GetResult ()
                .OrderByDescending (p => p.startIndex)
                .FirstOrDefault ();

            return Ok (new { lastPrefix = lastPrefix.startIndex + 1 });
        }
    }
}