using Hx.BgApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Hx.BgApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BgAppEntityFrameworkCoreModule),
    typeof(BgAppApplicationContractsModule)
    )]
public class BgAppDbMigratorModule : AbpModule
{
}
