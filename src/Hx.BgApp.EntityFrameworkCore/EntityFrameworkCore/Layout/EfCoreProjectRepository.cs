using Hx.BgApp.Layout;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Hx.BgApp.EntityFrameworkCore.Layout
{
    public class EfCoreProjectRepository
        : EfCoreRepository<BgAppDbContext, Project, Guid>, IProjectRepository
    {
        public EfCoreProjectRepository(IDbContextProvider<BgAppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<List<Project>> GetListAsync(
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string? filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                u => u.Title.Contains(filter))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        public async Task<long> GetCountAsync(
            string? filter = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                u => u.Title.Contains(filter))
                .CountAsync(GetCancellationToken(cancellationToken));
        }
        public async Task<Project?> GetCurrentProjectAsync(Guid? id = null)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(true)
                .WhereIf(id == null, u => u.Current)
                .WhereIf(id != null, u => u.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}