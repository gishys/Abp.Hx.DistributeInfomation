using Hx.BgApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using static Hx.BgApp.Permissions.BgAppPermissions;

namespace Hx.BgApp.Permissions;

public class BgAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GroupName, L("Permission:BgApp"));
        myGroup.AddPermission(PublishFeadback.Default, L("App.PublishFeadback"));
    }
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BgAppResource>(name);
    }
}