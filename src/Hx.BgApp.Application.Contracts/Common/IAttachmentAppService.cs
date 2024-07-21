using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Hx.BgApp.Common
{
    public interface IAttachmentAppService : IApplicationService
    {
        Task<AttachmentDownloandDto> DownloadAsync(byte[] fileBytes, string fileName);
    }
}