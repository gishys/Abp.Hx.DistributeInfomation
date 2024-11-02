using Hx.BgApp.PublishInformation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hx.BgApp.EntityFrameworkCore.PublishInformation
{
    public class EfCorePublishFeadbackRepository
        : EfCoreRepository<BgAppDbContext, PublishFeadbackInfo, Guid>, IEfCorePublishFeadbackRepository
    {
        public EfCorePublishFeadbackRepository(IDbContextProvider<BgAppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<List<PublishFeadbackInfo>> GetListAsync(
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    string? filter = null,
    CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Include(d => d.PublishInfos)
                .ThenInclude(d => d.Terms)
                .Include(d => d.FeadbackInfos)
                .WhereIf(!filter.IsNullOrWhiteSpace(), u => u.Title.Contains(filter))
                .OrderByDescending(u => u.CreationTime)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        public async Task<long> GetCountAsync(
            string? filter = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(
                !filter.IsNullOrWhiteSpace(), u => u.Title.Contains(filter))
                .CountAsync(GetCancellationToken(cancellationToken));
        }
        public async Task<long> GetEvaluationTimesAsync(
            string? filter = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(
                !filter.IsNullOrWhiteSpace(), u => u.Title.Contains(filter))
                .Select(d => d.FeadbackInfos.Count)
                .SumAsync(GetCancellationToken(cancellationToken));
        }
        public async Task<double> GetEvaluationAverageAsync(
    string? filter = null,
    CancellationToken cancellationToken = default)
        {
            var scores = await (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrWhiteSpace(), u => u.Title.Contains(filter))
                .SelectMany(d => d.FeadbackInfos.Select(f => f.TotalScore))
                .ToListAsync(GetCancellationToken(cancellationToken));

            if (scores.Count == 0)
            {
                return 0;
            }

            return scores.Average();
        }
        public async Task<PublishFeadbackInfo?> GetFeadbackInfoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(d => d.ParentId == id)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }
        public async Task<List<StatDo>> GetYearStatAsync(CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .SelectMany(d => d.FeadbackInfos)
                .GroupBy(d => d.CreateTime.Year)
                .Select(d => new StatDo() { Type = $"{d.Key}年", Score = d.Average(f => f.TotalScore) })
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        public async Task<List<StatDo>> GetMonthStatAsync(CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .SelectMany(d => d.FeadbackInfos)
                .GroupBy(d => d.CreateTime.Month)
                .Select(d => new StatDo() { Type = $"{d.Key}月", Score = d.Average(f => f.TotalScore) })
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}