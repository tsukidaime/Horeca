using Horeca.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Horeca;

[DependsOn(
    typeof(HorecaEntityFrameworkCoreTestModule)
    )]
public class HorecaDomainTestModule : AbpModule
{

}
