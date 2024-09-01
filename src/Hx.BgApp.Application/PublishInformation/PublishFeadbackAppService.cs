using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace Hx.BgApp.PublishInformation
{
    public class PublishFeadbackAppService : BgAppAppService
    {
        protected IEfCorePublishFeadbackRepository PublishFeadbackRepository { get; }
        public PublishFeadbackAppService(IEfCorePublishFeadbackRepository publishFeadbackRepository)
        {
            PublishFeadbackRepository = publishFeadbackRepository;
        }
        public async Task CreateAsync(PublishInfoCreateDto input)
        {
            var publishInfo = new PublishFeadbackInfo(
                GuidGenerator.Create(),
                input.Title,
                input.StartTime,
                input.EndTime,
                input.Release,
                input.Description);
            if (publishInfo.Release)
            {
                publishInfo.Publish();
            }
            if (input.PublishInfos?.Count > 0)
            {
                foreach (var contentInfo in input.PublishInfos)
                {
                    publishInfo.PublishInfos.Add(new ContentInfo(
                        contentInfo.Title,
                        contentInfo.Required,
                        contentInfo.ContentType,
                        contentInfo.Sort,
                        contentInfo.Value));
                }
            }
            await PublishFeadbackRepository.InsertAsync(publishInfo);
        }
        public async Task UpdateFeadbackAsync(FeadbackInfoCreateDto input)
        {
            var publishInfo = await PublishFeadbackRepository.FindAsync(input.Id);
            if (publishInfo != null)
            {
                var feadbacks = ObjectMapper.Map<ICollection<FeadbackCreateDto>, ICollection<FeadbackInfo>>(input.FeadbackInfos);
                int sort = publishInfo.FeadbackInfos.Count > 0 ? publishInfo.FeadbackInfos.Max(d => d.Sort) : 0;
                feadbacks.ForEach(d =>
                {
                    d.SetSort(++sort);
                    publishInfo.AddFeadbackInfo(d);
                });
                await PublishFeadbackRepository.UpdateAsync(publishInfo);
            }
        }
        public async Task<PagedResultDto<PublishFeadbackInfoDto>> GetListPagedAsync(GetPulishFeadbackInfoInput input)
        {
            var list = await PublishFeadbackRepository.GetListAsync(input.MaxResultCount, input.SkipCount, input.Filter);
            var count = await PublishFeadbackRepository.GetCountAsync(input.Filter);
            return new PagedResultDto<PublishFeadbackInfoDto>(
                count,
                ObjectMapper.Map<List<PublishFeadbackInfo>, List<PublishFeadbackInfoDto>>(list));
        }
        public async Task<List<PublishFeadbackInfoDto>> GetListAsync()
        {
            var list = await PublishFeadbackRepository.GetListAsync();
            return ObjectMapper.Map<List<PublishFeadbackInfo>, List<PublishFeadbackInfoDto>>(list);
        }
        public async Task<PublishFeadbackInfoDto?> GetAsync(Guid id)
        {
            var publish = await PublishFeadbackRepository.FindAsync(id);
            return ObjectMapper.Map<PublishFeadbackInfo?, PublishFeadbackInfoDto?>(publish);
        }
    }
}