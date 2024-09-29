using Microsoft.Extensions.Configuration;
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
        protected IEfCorePersonnelInfoRepository PersonnelInfoRepository { get; }
        protected IConfiguration Configuration { get; }
        public PublishFeadbackAppService(
            IEfCorePublishFeadbackRepository publishFeadbackRepository,
            IConfiguration configuration,
            IEfCorePersonnelInfoRepository personnelInfoRepository)
        {
            PublishFeadbackRepository = publishFeadbackRepository;
            Configuration = configuration;
            PersonnelInfoRepository = personnelInfoRepository;

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
            foreach (var personnel in input.FeadbackInfos)
            {
                var exist = await PersonnelInfoRepository.ExaminePersonnelExistAsync(personnel.Name, personnel.CertificateNumber);
                if (!exist)
                {
                    await PersonnelInfoRepository.InsertAsync(new PersonnelInfo(
                        GuidGenerator.Create(),
                        personnel.Name,
                        personnel.Sex,
                        personnel.CertificateNumber,
                        personnel.Age,
                        personnel.Phone));
                }
            }
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
            var result = ObjectMapper.Map<List<PublishFeadbackInfo>, List<PublishFeadbackInfoDto>>(list);
            foreach (var item in result)
            {
                foreach (var feadbackInfo in item.FeadbackInfos)
                {
                    feadbackInfo.LivingEnvironmentPics = ConvertSrc(feadbackInfo.LivingEnvironmentPics);
                    feadbackInfo.ObserveLawPics = ConvertSrc(feadbackInfo.ObserveLawPics);
                    feadbackInfo.NeighborsPics = ConvertSrc(feadbackInfo.NeighborsPics);
                    feadbackInfo.FamilyTraditionPics = ConvertSrc(feadbackInfo.FamilyTraditionPics);
                    feadbackInfo.GettingRichPics = ConvertSrc(feadbackInfo.GettingRichPics);
                    feadbackInfo.PolicyImplementationPics = ConvertSrc(feadbackInfo.PolicyImplementationPics);
                    feadbackInfo.PublicSpiritedPics = ConvertSrc(feadbackInfo.PublicSpiritedPics);
                    feadbackInfo.CivilizedPics = ConvertSrc(feadbackInfo.CivilizedPics);
                }
            }
            return new PagedResultDto<PublishFeadbackInfoDto>(count, result);
        }
        public async Task<List<PublishFeadbackInfoDto>> GetListAsync()
        {
            var list = await PublishFeadbackRepository.GetListAsync();
            var result = ObjectMapper.Map<List<PublishFeadbackInfo>, List<PublishFeadbackInfoDto>>(list);
            foreach (var item in result)
            {
                foreach (var feadbackInfo in item.FeadbackInfos)
                {
                    feadbackInfo.LivingEnvironmentPics = ConvertSrc(feadbackInfo.LivingEnvironmentPics);
                    feadbackInfo.ObserveLawPics = ConvertSrc(feadbackInfo.ObserveLawPics);
                    feadbackInfo.NeighborsPics = ConvertSrc(feadbackInfo.NeighborsPics);
                    feadbackInfo.FamilyTraditionPics = ConvertSrc(feadbackInfo.FamilyTraditionPics);
                    feadbackInfo.GettingRichPics = ConvertSrc(feadbackInfo.GettingRichPics);
                    feadbackInfo.PolicyImplementationPics = ConvertSrc(feadbackInfo.PolicyImplementationPics);
                    feadbackInfo.PublicSpiritedPics = ConvertSrc(feadbackInfo.PublicSpiritedPics);
                    feadbackInfo.CivilizedPics = ConvertSrc(feadbackInfo.CivilizedPics);
                }
            }
            return result;
        }
        public async Task<PublishFeadbackInfoDto?> GetAsync(Guid id)
        {
            var publish = await PublishFeadbackRepository.FindAsync(id);
            var result = ObjectMapper.Map<PublishFeadbackInfo?, PublishFeadbackInfoDto?>(publish);
            return result;
        }
        private string ConvertSrc(string pics)
        {
            var src = "";
            if (!string.IsNullOrEmpty(pics))
            {
                foreach (var temp in pics.Split(','))
                {
                    var srcTemp = temp.Contains(AppGlobalProperties.StaticFilesBasePathSign) ?
                        temp.Replace(AppGlobalProperties.StaticFilesBasePathSign, Configuration[AppGlobalProperties.FileServerBasePath]) : "";
                    if (string.IsNullOrEmpty(src))
                        src += $"{srcTemp}";
                }
            }
            return src;
        }
    }
}