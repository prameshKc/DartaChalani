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

    [Route ("api/[controller]")]
    [ApiController]
    public class RekhankanChalaniController : ControllerBase {

        private readonly IMapper _map;
        private readonly IUOW _uow;
        private readonly IWebHostEnvironment env;
        private readonly IGenericRepository<Prefix> _Irepo;
        private readonly IGenericRepository<ChalanFiles> _IFileRepo;
        private readonly IGenericRepository<Subject> _ISubRepo;

        public RekhankanChalaniController (IMapper _map,
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
        public IActionResult GetAllFiledRekhankanChalani () {

            var domain = _uow._FiledChalanRepo.GetAllFieldRekhankan ().GetAwaiter ().GetResult ();

            var res = _map.Map<List<FieldRekhankanChalani>, List<FieldRekhankanChalaniResource>> (domain.ToList ());
            return Ok (res);
        }

        [HttpPost ("Add")]
        public async Task<IActionResult> Post ([FromForm] FieldRekhankanChalaniResource model) {

            if (model != null) {

                var domainChalani = _map.Map<FieldRekhankanChalani> (model);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "field_rekhankan_chalani");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var fieldFile = new RekhankanChalanFiles () {
                            fileUrl = fileName
                        };

                        domainChalani.patras.Add (new RekhankanChalanPatras () {
                            file = fieldFile
                        });

                    }

                }
                domainChalani.subject = _ISubRepo.FilterAsync (p => p.Id == model.subjectId)
                    .GetAwaiter ().GetResult ().FirstOrDefault ();

                await _uow._FiledChalanRepo.CreateAsync (domainChalani);
                await _uow.CompleteAsync ();
                var res = _map.Map<FieldRekhankanChalaniResource> (domainChalani);
                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpPost ("Edit")]
        public async Task<IActionResult> Update ([FromForm] FieldRekhankanChalaniResource model) {

            if (model != null) {

                var domainField = await _uow._FiledChalanRepo.GetById (model.Id);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "field_rekhankan_chalani");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var fieldFile = new RekhankanChalanFiles () {
                            fileUrl = fileName
                        };
                        var addedFile = _map.Map<RekhankanChalanFileResource> (fieldFile);
                        model.patras.Add (new RekhankanChalanPatraResource () {
                            file = addedFile
                        });

                    }

                }

                _map.Map<FieldRekhankanChalaniResource, FieldRekhankanChalani> (model, domainField);
                await _uow._FiledChalanRepo.EditAsync (domainField);
                await _uow.CompleteAsync ();

                var res = _map.Map<FieldRekhankanChalaniResource> (_uow._FiledChalanRepo.GetAllFieldRekhankan ()
                    .GetAwaiter ().GetResult ().FirstOrDefault (p => p.Id == model.Id));

                return Ok (res);
            } else {
                return BadRequest ();
            }
        }
       [HttpDelete ("Delete/{id}")]
        public async Task<IActionResult> Delete (int id) {

            if (id == 0)
                return NotFound ();

            var purji = await _uow._FiledChalanRepo.GetById (id);
            await _uow._FiledChalanRepo.DeleteAsync (purji);
           await  _uow.CompleteAsync();
            return Ok ();
        }

        [HttpDelete ("DeleteFile/{did}/{fid}")]
        public async Task<IActionResult> DeleteDartaFile (int did, int fid) {

            var patra = await _uow._FiledChalanRepo.GetFieldWithpatrasById (did);
            var file = patra.patras.Find (p => p.fileId == fid);
            patra.patras.Remove (file);
            await _uow.CompleteAsync ();
            return Ok ();
        }

        [HttpGet ("Prefix")]
        public IActionResult GetPrefix () {

            var lastPrefix = _Irepo.FilterAsync (p => p.type == "field-chalani")
                .GetAwaiter ()
                .GetResult ()
                .OrderByDescending (p => p.startIndex)
                .FirstOrDefault ();

            return Ok (new { lastPrefix =lastPrefix.startIndex + 1 });
        }
    }
}