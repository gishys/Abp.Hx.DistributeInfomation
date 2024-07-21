using Hx.BgApp.Layout;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Hx.BgApp.EntityFrameworkCore.Layout
{
    public class EfCorePageRepository : EfCoreRepository<BgAppDbContext, Page, Guid>, IPageRepository
    {
        public EfCorePageRepository(IDbContextProvider<BgAppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}