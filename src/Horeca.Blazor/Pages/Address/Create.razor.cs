using Horeca.Addresses;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Horeca.Blazor.Pages.Address
{
    public partial class Create
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public CreateUpdateAddressDto AddressDto { get; set; } = new CreateUpdateAddressDto(); 
        [Inject]
        public IAddressAppService AddressAppService { get; set; }

        public async Task SubmitAsync()
        {
            AddressDto.UserId = (Guid)CurrentUser.Id;
            await AddressAppService.CreateAsync(AddressDto);
            NavigationManager.NavigateTo("/address/management");
        }
    }
}
