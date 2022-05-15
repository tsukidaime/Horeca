﻿using Blazorise;
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
        private bool CanCreateAddress { get; set; }
        private bool CanEditAddress { get; set; }
        private bool CanDeleteAddress { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetAddresssAsync();
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateAddress = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.AddressCreate);
            CanEditAddress = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.AddressEdit);
            CanDeleteAddress = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.AddressDelete);
        }

        private async Task GetAddresssAsync()
        {
            var result = await AddressAppService.GetListAsync(
                new PagedAndSortedResultRequestDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
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

            await GetAddresssAsync();

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
            await GetAddresssAsync();
        }
    }
}
