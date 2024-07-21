using Hx.BgApp.Layout;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Hx.BgApp.EntityFrameworkCore.Layout
{
    public class EfCorePublishFeadbackRepository
        : EfCoreRepository<BgAppDbContext, Menu, Guid>, IMenuRepository
    {
        public EfCorePublishFeadbackRepository(IDbContextProvider<BgAppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}