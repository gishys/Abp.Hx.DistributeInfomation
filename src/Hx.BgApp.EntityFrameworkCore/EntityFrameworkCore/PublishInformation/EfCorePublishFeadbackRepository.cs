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
                .ThenInclude(d => d.Terms)
                .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                u => u.Title.Contains(filter))
                .OrderBy(u => u.CreationTime)
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
        public async Task<PublishFeadbackInfo?> GetFeadbackInfoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(d => d.ParentId == id)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }
    }
}