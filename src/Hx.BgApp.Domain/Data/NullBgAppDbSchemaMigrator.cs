using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Hx.BgApp.Data;

/* This is used if database provider does't define
 * IBgAppDbSchemaMigrator implementation.
 */
public class NullBgAppDbSchemaMigrator : IBgAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
