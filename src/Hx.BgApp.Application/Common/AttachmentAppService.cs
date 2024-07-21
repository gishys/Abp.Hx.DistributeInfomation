using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BlobStoring;

namespace Hx.BgApp.Common
{
    [RemoteService(IsEnabled = false)]
    public class AttachmentAppService : BgAppAppService, IAttachmentAppService
    {
        protected IBlobContainer<AttachmentContainer> BlobContainer { get; }
        protected IConfiguration Configuration { get; }
        public AttachmentAppService(
            IBlobContainer<AttachmentContainer> blobContainer,
            IConfiguration configuration)
        {
            BlobContainer = blobContainer;
            Configuration = configuration;
        }
        public async Task<AttachmentDownloandDto> DownloadAsync(byte[] fileBytes, string fileName)
        {
            var id = GuidGenerator.Create();
            var storeName = $"{id}{Path.GetExtension(fileName)}";
            var tenant = CurrentTenant.Id == null ? "host" : $"tenants/{CurrentTenant.Id}";
            var fileUrl = $"/cmsfile/{tenant}/attachment/{storeName}";
            var src = $"{Configuration[AppGlobalProperties.FileServerBasePath]}{fileUrl}";
            await BlobContainer.SaveAsync(storeName, fileBytes);
            return new AttachmentDownloandDto()
            {
                Name = fileName,
                StoreName = storeName,
                StaticFileUrl = src,
                Url = $"{AppGlobalProperties.StaticFilesBasePathSign}{fileUrl}",
            };
        }
    }
}