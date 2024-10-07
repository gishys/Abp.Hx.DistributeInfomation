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
    public interface IEfCorePersonnelInfoRepository : IBasicRepository<PersonnelInfo, Guid>
    {
        Task<PersonnelInfo?> ExaminePersonnelExistAsync(string name, string certificateNumber);
        Task<List<PersonnelInfo>> GetListAsync(string? name, string? phone);
    }
}
