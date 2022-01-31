using Horeca.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Horeca.Blazor;

public abstract class HorecaComponentBase : AbpComponentBase
{
    protected HorecaComponentBase()
    {
        LocalizationResource = typeof(HorecaResource);
    }
}
