using Horeca.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Horeca.Permissions;

public class HorecaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var horeca = context.AddGroup(HorecaPermissions.Horeca);
        var product = horeca.AddPermission(HorecaPermissions.ProductManagement, L("Permission:ProductManagement"));
        product.AddChild(HorecaPermissions.ProductManagementSupplier, L("Permission:ProductManagementSupplier"));
        product.AddChild(HorecaPermissions.ProductManagementAdmin, L("Permission:ProductManagementAdmin"));
        var category = horeca.AddPermission(HorecaPermissions.Category,L("Permission:Category"));
        category.AddChild(HorecaPermissions.CategoryCreate, L("Permission:CategoryCreate"));
        category.AddChild(HorecaPermissions.CategoryEdit, L("Permission:CategoryEdit"));
        category.AddChild(HorecaPermissions.CategoryDelete, L("Permission:CategoryDelete"));
        category.AddChild(HorecaPermissions.CategoryRead, L("Permission:CategoryRead"));
        var address = horeca.AddPermission(HorecaPermissions.AddressManagement, L("Permission:Address"));
        address.AddChild(HorecaPermissions.AddressCreate, L("Permission:AddressCreate"));
        address.AddChild(HorecaPermissions.AddressEdit, L("Permission:AddressEdit"));
        address.AddChild(HorecaPermissions.AddressDelete, L("Permission:AddressDelete"));
        address.AddChild(HorecaPermissions.AddressRead, L("Permission:AddressRead"));
        var order = horeca.AddPermission(HorecaPermissions.Order, L("Permission:Order"));
        order.AddChild(HorecaPermissions.OrderCreate, L("Permission:OrderCreate"));
        order.AddChild(HorecaPermissions.OrderEdit, L("Permission:OrderEdit"));
        order.AddChild(HorecaPermissions.OrderDelete, L("Permission:OrderDelete"));
        order.AddChild(HorecaPermissions.OrderRead, L("Permission:OrderRead"));
        var orderManagement = horeca.AddPermission(HorecaPermissions.OrderManagement, L("Permission:OrderManagement"));
        orderManagement.AddChild(HorecaPermissions.OrderManagementCustomer, L("Permission:OrderManagementCustomer"));
        orderManagement.AddChild(HorecaPermissions.OrderManagementSupplier, L("Permission:OrderManagementSupplier"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HorecaResource>(name);
    }
}
