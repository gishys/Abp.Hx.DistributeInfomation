using Hx.BgApp.Layout;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Hx.BgApp.EntityFrameworkCore.Layout
{
    public class EfCoreMenuRepository
        : EfCoreRepository<BgAppDbContext, Menu, Guid>, IMenuRepository
    {
        public EfCoreMenuRepository(IDbContextProvider<BgAppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}