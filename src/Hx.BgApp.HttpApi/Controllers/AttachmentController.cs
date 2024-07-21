using Hx.BgApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Hx.BgApp.Controllers
{
    [ApiController]
    [Route("api/app/attachment")]
    public class AttachmentController : BgAppController
    {
        protected IAttachmentAppService AttachmentAppService { get; }
        public AttachmentController(
            IAttachmentAppService attachmentAppService)
        {
            AttachmentAppService = attachmentAppService;
        }
        [HttpPost]
        public async Task<DownloadResultDto<AttachmentDownloandDto>> CreateAsync()
        {
            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var file = files[0];
                byte[] fileBytes;
                using (var fileStream = file.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                return new DownloadResultDto<AttachmentDownloandDto>(
                    await AttachmentAppService.DownloadAsync(fileBytes, file.FileName));
            }
            return new DownloadResultDto<AttachmentDownloandDto>("上传文件为空！");
        }
    }
}