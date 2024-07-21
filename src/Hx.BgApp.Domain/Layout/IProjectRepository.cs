using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;

namespace Hx.BgApp.Layout
{
    public interface IProjectRepository : IBasicRepository<Project, Guid>
    {
        Task<List<Project>> GetListAsync(
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string? filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);
        Task<long> GetCountAsync(
            string? filter = null,
            CancellationToken cancellationToken = default);
        Task<Project?> GetCurrentProjectAsync(Guid? id = null);
    }
}