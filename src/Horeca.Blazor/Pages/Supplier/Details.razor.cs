using Horeca.Supplier;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Horeca.Blazor.Pages.Supplier
{
    public partial class Details
    {
        [Parameter]
        public Guid SupplierId { get; set; }
        public SupplierDto Supplier { get; private set; } = new SupplierDto();
        [Inject]
        public IIdentityUserAppService IdentityUserAppService { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            var user = await IdentityUserAppService.GetAsync(SupplierId);
            Supplier = new SupplierDto
            {
                BIN = user.GetProperty<string>("BIN"),
                CompanyName = user.GetProperty<string>("CompanyName"),
                PhoneNumber = user.PhoneNumber,
                StartDate = user.CreationTime
            };
        }
    }
}
