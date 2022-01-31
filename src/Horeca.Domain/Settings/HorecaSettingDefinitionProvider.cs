using Volo.Abp.Settings;

namespace Horeca.Settings;

public class HorecaSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HorecaSettings.MySetting1));
    }
}
