using Microsoft.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Horeca.EntityFrameworkCore;

public static class HorecaEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        HorecaGlobalFeatureConfigurator.Configure();
        HorecaModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<IdentityUser, string>(
                    "BIN",
                    (entityBuilder, propertyBuilder) =>
                    {
                        entityBuilder.HasIndex("BIN");
                        propertyBuilder.IsRequired();
                        propertyBuilder.HasMaxLength(12);
                    }
                )
                .MapEfCoreProperty<IdentityUser, string>(
                    "CompanyName",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.IsRequired();
                    }
                );
        });
    }
}
