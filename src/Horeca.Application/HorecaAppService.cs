using System;
using System.Collections.Generic;
using System.Text;
using Horeca.Localization;
using Volo.Abp.Application.Services;

namespace Horeca;

/* Inherit your application services from this class.
 */
public abstract class HorecaAppService : ApplicationService
{
    protected HorecaAppService()
    {
        LocalizationResource = typeof(HorecaResource);
    }
}
