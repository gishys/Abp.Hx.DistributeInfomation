using System.Threading.Tasks;

namespace Hx.BgApp.Data;

public interface IBgAppDbSchemaMigrator
{
    Task MigrateAsync();
}
