using Volo.Abp.Settings;

namespace Hx.BgApp.Settings;

public class BgAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BgAppSettings.MySetting1));
    }
}
