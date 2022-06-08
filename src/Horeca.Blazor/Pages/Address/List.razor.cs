using Blazorise;
using Blazorise.DataGrid;
using Horeca.Addresses;
using Horeca.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Horeca.Blazor.Pages.Address
{
    public partial class List
    {
        private IReadOnlyList<AddressDto> AddressList { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetAddressListsync();
        }

        private async Task GetAddressListsync()
        {
            var result = await AddressAppService.GetListAsync(
                new GetAddressListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    UserId = CurrentUser.Id
                }
            );

            AddressList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<AddressDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.None)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetAddressListsync();

            await InvokeAsync(StateHasChanged);
        }
        public void NavigateToCreate()
        {
            NavigationManager.NavigateTo("address/create");
        }

        private async Task DeleteAddressAsync(AddressDto Address)
        {
            var confirmMessage = L["AddressDeletionConfirmationMessage", Address.Name];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await AddressAppService.DeleteAsync(Address.Id);
            await GetAddressListsync();
        }
    }
}
