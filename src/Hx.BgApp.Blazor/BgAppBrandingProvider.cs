using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Hx.BgApp.Blazor;

[Dependency(ReplaceServices = true)]
public class BgAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BgApp";
}
