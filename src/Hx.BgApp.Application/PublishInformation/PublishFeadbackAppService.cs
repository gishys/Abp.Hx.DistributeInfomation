using Hx.BgApp.Layout;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hx.BgApp.PublishInformation
{
    public class PublishFeadbackAppService : BgAppAppService
    {
        protected IEfCorePublishFeadbackRepository PublishFeadbackRepository { get; }
        public PublishFeadbackAppService(IEfCorePublishFeadbackRepository publishFeadbackRepository)
        {
            PublishFeadbackRepository = publishFeadbackRepository;
        }
        public async Task CreateAsync(PublishFeadbackInfoCreateDto input)
        {
            var publishInfo = new PublishFeadbackInfo(
                GuidGenerator.Create(),
                input.Title,
                input.StartTime,
                input.EndTime,
                input.ParentId);
            if (publishInfo.Release)
            {
                publishInfo.Publish();
            }
            if (input.ContentInfos.Count > 0)
            {
                foreach (var contentInfo in input.ContentInfos)
                {
                    publishInfo.ContentInfos.Add(new ContentInfo(
                        contentInfo.Title,
                        contentInfo.Required,
                        contentInfo.ContentType,
                        contentInfo.Sort,
                        contentInfo.Value));
                }
            }
            await PublishFeadbackRepository.InsertAsync(publishInfo);
        }
        public async Task<PagedResultDto<PublishFeadbackInfoDto>> GetListAsync(GetPulishFeadbackInfoInput input)
        {
            var list = await PublishFeadbackRepository.GetListAsync(input.MaxResultCount, input.SkipCount, input.Filter);
            var count = await PublishFeadbackRepository.GetCountAsync(input.Filter);
            return new PagedResultDto<PublishFeadbackInfoDto>(
                count,
                ObjectMapper.Map<List<PublishFeadbackInfo>, List<PublishFeadbackInfoDto>>(list));
        }
        public async Task<PublishFeadbackInfoDto?> GetAsync(Guid id)
        {
            var publish = await PublishFeadbackRepository.FindAsync(id);
            var feadback = await PublishFeadbackRepository.GetFeadbackInfoAsync(id);
            if (publish != null && feadback != null)
            {
                feadback.ContentInfos.ForEach(publish.ContentInfos.Add);
            }
            return ObjectMapper.Map<PublishFeadbackInfo?, PublishFeadbackInfoDto?>(publish);
        }
    }
}