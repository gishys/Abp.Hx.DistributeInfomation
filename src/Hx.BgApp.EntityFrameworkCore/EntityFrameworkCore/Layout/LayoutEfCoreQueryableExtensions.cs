using Hx.BgApp.Layout;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hx.BgApp.EntityFrameworkCore.Layout
{
    public static class LayoutEfCoreQueryableExtensions
    {
        public static IQueryable<Project> IncludeDetails(this IQueryable<Project> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(d => d.Menus)
                .Include(d => d.Pages);
        }
    }
}
