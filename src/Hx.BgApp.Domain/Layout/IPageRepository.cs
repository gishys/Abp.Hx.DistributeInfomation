using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Hx.BgApp.Layout
{
    public interface IPageRepository : IBasicRepository<Page, Guid>
    {
    }
}
