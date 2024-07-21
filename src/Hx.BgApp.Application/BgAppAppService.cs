using System;
using System.Collections.Generic;
using System.Text;
using Hx.BgApp.Localization;
using Volo.Abp.Application.Services;

namespace Hx.BgApp;

/* Inherit your application services from this class.
 */
public abstract class BgAppAppService : ApplicationService
{
    protected BgAppAppService()
    {
        LocalizationResource = typeof(BgAppResource);
    }
}
