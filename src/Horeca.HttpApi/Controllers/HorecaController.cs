using Horeca.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Horeca.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HorecaController : AbpControllerBase
{
    protected HorecaController()
    {
        LocalizationResource = typeof(HorecaResource);
    }
}
