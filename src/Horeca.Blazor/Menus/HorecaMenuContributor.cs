using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Horeca.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using Horeca.Permissions;
using Volo.Abp.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Horeca.Blazor.Menus;

public class HorecaMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;
    public HorecaMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<HorecaResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                HorecaMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );
        if (await context.IsGrantedAsync(HorecaPermissions.OrderManagement))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                "Horeca.Products",
                l["Menu:Products"],
                url: "/products"
            ));
        }
        if (await context.IsGrantedAsync(HorecaPermissions.Order))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                "Horeca.Order",
                l["Menu:Order"],
                url: "/order"
            ));
        }
        if (await context.IsGrantedAsync(HorecaPermissions.OrderManagement))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                "Horeca.OrderManagement",
                l["Menu:Order:Management"],
                url: "/order/management"
            ));
        }
        if (await context.IsGrantedAsync(HorecaPermissions.AddressManagementCustomer))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                "Horeca.Addresses",
                l["Menu:Address:Management"],
                url: "/address/management"
            ));
        }
        if (await context.IsGrantedAsync(HorecaPermissions.ProductManagement))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                "Horeca.Products",
                l["Menu:Product:Management"],
                url: "/product/management"
            ));
        }
    }

    private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

    }
}
