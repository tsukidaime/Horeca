﻿using Horeca.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Horeca.Permissions;

public class HorecaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var horeca = context.AddGroup(HorecaPermissions.Horeca);
        var product = horeca.AddPermission(HorecaPermissions.ProductManagement, L("Permission:Product"));
        product.AddChild(HorecaPermissions.ProductCreate, L("Permission:ProductCreate"));
        product.AddChild(HorecaPermissions.ProductEdit, L("Permission:ProductEdit"));
        product.AddChild(HorecaPermissions.ProductDelete, L("Permission:ProductDelete"));
        product.AddChild(HorecaPermissions.ProductRead, L("Permission:ProductRead"));
        product.AddChild(HorecaPermissions.ProductApprove, L("Permission:ProductApprove"));
        var productBid = horeca.AddPermission(HorecaPermissions.ProductBid, L("Permission:ProductBid"));
        productBid.AddChild(HorecaPermissions.ProductBidCreate, L("Permission:ProductBidCreate"));
        productBid.AddChild(HorecaPermissions.ProductBidEdit, L("Permission:ProductBidEdit"));
        productBid.AddChild(HorecaPermissions.ProductBidDelete, L("Permission:ProductBidDelete"));
        productBid.AddChild(HorecaPermissions.ProductBidRead, L("Permission:ProductBidRead"));
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

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HorecaResource>(name);
    }
}
