using Hx.BgApp.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Hx.BgApp.Blazor;

public abstract class BgAppComponentBase : AbpComponentBase
{
    protected BgAppComponentBase()
    {
        LocalizationResource = typeof(BgAppResource);
    }
}
