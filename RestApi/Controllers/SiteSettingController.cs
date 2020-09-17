using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.UnitOfWork;
using DomainModel.Resources.Setting;
using DomainModel.Setting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers {

    [Route ("api/site")]

    [ApiController]
    public class SiteSettingController : ControllerBase {
        private readonly IMapper mapper;
        private readonly IUOW uow;
        private readonly IWebHostEnvironment web;

        public SiteSettingController (IMapper mapper, IUOW uow, IWebHostEnvironment web) {
            this.uow = uow;
            this.web = web;
            this.mapper = mapper;

        }

        [HttpGet ("all")]
        public async Task<IActionResult> GetAllSites () {
            var sites = await uow._Isite.GetAllSiteWithNames ();
            var res = mapper.Map<IEnumerable<SiteSetting>, IEnumerable<SiteSettingResource>> (sites);
            return Ok (res);
        }

        [HttpPost ("add")]
        public async Task<IActionResult> Add ([FromForm] SiteSettingResource model) {
            var file = Request.Form.Files.FirstOrDefault ();
            if (file != null) {

                var path = Path.Combine (web.WebRootPath, "images");

                if (!Directory.Exists (path)) {
                    Directory.CreateDirectory (path);
                }
                    var FileName = Guid.NewGuid ().ToString () + "" + Path.GetFileNameWithoutExtension (file.FileName) + Path.GetExtension (file.FileName);
                    var filePath = Path.Combine (path, FileName);
                    model.LogoUrl = FileName;

                    using (var stream = new FileStream (filePath, FileMode.Create)) {
                        await file.CopyToAsync (stream);
                    }
                
            }
            var domain = mapper.Map<SiteSetting> (model);
           
            await uow._Isite.CreateAsync(domain);
            await uow.CompleteAsync();
            return Ok ();
        }


        [HttpPost ("Edit")]
        public async Task<IActionResult> Update ([FromForm] SiteSettingResource model) {
        
           if(model ==null)
                return BadRequest();

             var domain = await uow._Isite.GetById(model.Id);
            var file = Request.Form.Files.FirstOrDefault ();
            if (file != null) {

                var path = Path.Combine (web.WebRootPath, "images");

                if (!Directory.Exists (path)) {
                    Directory.CreateDirectory (path);
                }
                    var FileName = Guid.NewGuid ().ToString () + "" + Path.GetFileNameWithoutExtension (file.FileName) + Path.GetExtension (file.FileName);
                    var filePath = Path.Combine (path, FileName);
                    model.LogoUrl = FileName;

                    using (var stream = new FileStream (filePath, FileMode.Create)) {
                        await file.CopyToAsync (stream);
                    }
                
            }
            mapper.Map<SiteSettingResource, SiteSetting> (model,domain);
            
          //  await uow._Isite.CreateAsync(domain);
            await uow.CompleteAsync();
            return Ok ();
        }

    }
}