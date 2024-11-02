using Microsoft.Extensions.Configuration;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;
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
        public async Task<List<PersonnelInfoDto>> GetListPersonnelInfoAsync(string? name, string? phone)
        {
            var result = await PersonnelInfoRepository.GetListAsync(name, phone);
            return ObjectMapper.Map<List<PersonnelInfo>, List<PersonnelInfoDto>>(result);
        }
        public async Task UpdateFeadbackAsync(FeadbackInfoCreateDto input)
        {
            foreach (var personnel in input.FeadbackInfos)
            {
                var personnelInfo = await PersonnelInfoRepository.ExaminePersonnelExistAsync(personnel.Name, personnel.CertificateNumber);
                if (personnelInfo == null)
                {
                    await PersonnelInfoRepository.InsertAsync(new PersonnelInfo(
                        GuidGenerator.Create(),
                        personnel.Name,
                        personnel.Sex,
                        personnel.CertificateNumber,
                        personnel.Age,
                        personnel.Phone));
                }
                else
                {
                    personnelInfo.SetName(personnel.Name);
                    personnelInfo.SetSex(personnel.Sex);
                    personnelInfo.SetCertificateNumber(personnel.CertificateNumber);
                    personnelInfo.SetAge(personnel.Age);
                    personnelInfo.SetPhone(personnel.Phone);
                }
            }
            var publishInfo = await PublishFeadbackRepository.FindAsync(input.Id);
            if (publishInfo != null)
            {
                foreach (var fd in input.FeadbackInfos)
                {
                    if (publishInfo.FeadbackInfos.Any(d => fd.Name == d.Name && fd.CertificateNumber == d.CertificateNumber))
                    {
                        throw new UserFriendlyException($"姓名：{fd.Name} 已接龙！");
                    }
                }
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
        public async Task<OverallStat> GetOverallStat()
        {
            var totalCount = await PublishFeadbackRepository.GetCountAsync();
            var evaluationTimes = await PublishFeadbackRepository.GetEvaluationTimesAsync();
            var householdCount = await PersonnelInfoRepository.GetCountAsync();
            var evaluationsAverage = await PublishFeadbackRepository.GetEvaluationAverageAsync();
            return new OverallStat()
            {
                InitiateEvaluations = totalCount,
                EvaluationsTotalTimes = evaluationTimes,
                HouseholdCount = householdCount,
                EvaluationsAverage = evaluationsAverage,
            };
        }
        public async Task<List<StatDo>> GetMonthStatAsync(CancellationToken cancellationToken = default)
        {
            var list = await PublishFeadbackRepository.GetMonthStatAsync(cancellationToken);
            var result = new List<StatDo>();
            for (var i = 1; i < 13; i++)
            {
                var mouth = new StatDo() { Type = $"{i}月" };
                mouth.Score = list.FirstOrDefault(d => d.Type == mouth.Type)?.Score ?? 0;
                result.Add(mouth);
            }
            return result;
        }
        public async Task<List<StatDo>> GetYearStatAsync(CancellationToken cancellationToken = default)
        {
            var list = await PublishFeadbackRepository.GetYearStatAsync(cancellationToken);
            if (list.Count < 10)
            {
                var last = list.LastOrDefault()?.Type ?? DateTime.Now.Year.ToString();
                var maxCount = 10 - list.Count + 1;
                for (var year = 1; year < maxCount; year++)
                {
                    list.AddFirst(new StatDo() { Type = $"{Convert.ToInt32(last.Substring(0, 4)) - year}年", Score = 0 });
                }
            }
            return list;
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