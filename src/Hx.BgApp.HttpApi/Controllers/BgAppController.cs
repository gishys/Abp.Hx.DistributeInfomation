using Hx.BgApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Hx.BgApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BgAppController : AbpControllerBase
{
    protected BgAppController()
    {
        LocalizationResource = typeof(BgAppResource);
    }
}
