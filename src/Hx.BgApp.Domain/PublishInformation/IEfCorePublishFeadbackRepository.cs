using Hx.BgApp.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Hx.BgApp.PublishInformation
{
    public interface IEfCorePublishFeadbackRepository : IBasicRepository<PublishFeadbackInfo, Guid>
    {
        Task<List<PublishFeadbackInfo>> GetListAsync(
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    string? filter = null,
    CancellationToken cancellationToken = default);
        Task<long> GetCountAsync(
            string? filter = null,
            CancellationToken cancellationToken = default);
        Task<PublishFeadbackInfo?> GetFeadbackInfoAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
