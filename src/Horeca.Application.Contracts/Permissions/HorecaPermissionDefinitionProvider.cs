using Horeca.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Horeca.Permissions;

public class HorecaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HorecaPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HorecaPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HorecaResource>(name);
    }
}
