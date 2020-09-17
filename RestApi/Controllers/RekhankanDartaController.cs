using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract;
using BLL.UnitOfWork;
using DomainModel;
using DomainModel.FieldRekhankanDarta;
using DomainModel.Resources.FieldRekhankanDarta;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class RekhankanDartaController : ControllerBase {

        private readonly IMapper _map;
        private readonly IUOW _uow;
        private readonly IWebHostEnvironment env;
        private readonly IGenericRepository<Prefix> _Irepo;

        public RekhankanDartaController (IMapper _map,
            IUOW _uow,
            IWebHostEnvironment _env,
            IGenericRepository<Prefix> _Irepo
        ) {
            this._Irepo = _Irepo;
            this._uow = _uow;
            env = _env;
            this._map = _map;
        }

        [HttpGet ("All")]
        public IActionResult GetAllFiledRekhankanDarta () {

            var domain = _uow._FiledDartaRepo.GetAllFieldRekhankan ().GetAwaiter ().GetResult ();

            var res = _map.Map<List<FieldRekhankanDarta>, List<FieldRekhankanDartaResource>> (domain.ToList ());
            return Ok (res);
        }

        [HttpPost ("Add")]
        public async Task<IActionResult> Post ([FromForm] FieldRekhankanDartaResource model) {

            if (model != null) {

                var domainDarta = _map.Map<FieldRekhankanDarta> (model);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "field_rekhankan_darta");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var fieldFile = new FieldRekhankanDartaFile () {
                            fileUrl = fileName
                        };

                        domainDarta.patras.Add (new RekhankanDartaPatras () {
                            file = fieldFile
                        });

                    }

                }

                await _uow._FiledDartaRepo.CreateAsync (domainDarta);
                await _uow.CompleteAsync ();
                var res = _map.Map<FieldRekhankanDartaResource> (domainDarta);
                return Ok (res);
            } else {
                return BadRequest ();
            }
        }

        [HttpPost ("Edit")]
        public async Task<IActionResult> Update ([FromForm] FieldRekhankanDartaResource model) {

            if (model != null) {

                var domainField = await _uow._FiledDartaRepo.GetById (model.Id);

                var files = Request.Form.Files;
                if (files.Count > 0) {
                    var root = Path.Combine (env.WebRootPath, "files", "field_rekhankan_darta");
                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }

                    foreach (var file in files) {

                        var fileName = Guid.NewGuid ().ToString () + "_" + Path.GetFileNameWithoutExtension (file.FileName) + "" + Path.GetExtension (file.FileName);

                        var filePath = Path.Combine (root, fileName);
                        using (var stream = new FileStream (filePath, FileMode.Create)) {
                            await file.CopyToAsync (stream);
                        }

                        var fieldFile = new FieldRekhankanDartaFile () {
                            fileUrl = fileName
                        };
                        var addedFile = _map.Map<RekhankanDartaFileResource> (fieldFile);
                        model.patras.Add (new RekhankanDartaPatrasResource () {
                            file = addedFile
                        });

                    }

                }

                _map.Map<FieldRekhankanDartaResource, FieldRekhankanDarta> (model, domainField);
                await _uow._FiledDartaRepo.EditAsync (domainField);
                await _uow.CompleteAsync ();

                var res = _map.Map<FieldRekhankanDartaResource> (_uow._FiledDartaRepo.GetAllFieldRekhankan ()
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

            var purji = await _uow._FiledDartaRepo.GetById (id);
            await _uow._FiledDartaRepo.DeleteAsync (purji);
           await  _uow.CompleteAsync();
            return Ok ();
        }

        [HttpDelete ("DeleteFile/{did}/{fid}")]
        public async Task<IActionResult> DeleteDartaFile (int did, int fid) {

            var patra = await _uow._FiledDartaRepo.GetFieldWithpatrasById (did);
            var file = patra.patras.Find (p => p.fileId == fid);
            patra.patras.Remove (file);
            await _uow.CompleteAsync ();
            return Ok ();
        }

        [HttpGet ("Prefix")]
        public IActionResult GetPrefix () {

            var lastPrefix = _Irepo.FilterAsync (p => p.type == "field-darta")
                .GetAwaiter ()
                .GetResult ()
                .OrderByDescending (p => p.startIndex)
                .FirstOrDefault ();

            return Ok (new { lastPrefix =lastPrefix.startIndex + 1 });
        }
    }
}