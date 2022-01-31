﻿using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Horeca.Blazor;

[Dependency(ReplaceServices = true)]
public class HorecaBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Horeca";
}
