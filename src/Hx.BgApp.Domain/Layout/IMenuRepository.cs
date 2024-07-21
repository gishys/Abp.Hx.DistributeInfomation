using System;
using Volo.Abp.Domain.Repositories;

namespace Hx.BgApp.Layout
{
    public interface IMenuRepository : IBasicRepository<Menu, Guid>
    {
    }
}