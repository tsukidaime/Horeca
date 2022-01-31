using Horeca.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Horeca.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HorecaEntityFrameworkCoreModule),
    typeof(HorecaApplicationContractsModule)
    )]
public class HorecaDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
