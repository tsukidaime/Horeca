using Volo.Abp.Modularity;

namespace Horeca;

[DependsOn(
    typeof(HorecaApplicationModule),
    typeof(HorecaDomainTestModule)
    )]
public class HorecaApplicationTestModule : AbpModule
{

}
