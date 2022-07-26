﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blazorise;
using Blazorise.DataGrid;
using Horeca.Permissions;
using Horeca.Products;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Horeca.Blazor.Pages.Product
{
    public partial class List
    {
        private IReadOnlyList<ProductDto> ProductList { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool IsSupplier { get; set; }
        private bool IsAdmin { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetProductsAsync();
        }

        private async Task SetPermissionsAsync()
        {
            IsSupplier = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.ProductManagementSupplier);
            IsAdmin = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.ProductManagementAdmin);
        }

        private async Task GetProductsAsync()
        {
            var result = await ProductAppService.GetListAsync(
                new GetProductListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    OnlyApproved = false,
                    SuplierId = IsSupplier ? CurrentUser.Id : null
                }
            );

            ProductList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProductDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.None)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetProductsAsync();

            await InvokeAsync(StateHasChanged);
        }
        public void NavigateToBidsManagement(ProductDto Product)
        {
            NavigationManager.NavigateTo($"productbid/management/{Product.Id}");
        }
        public void NavigateToCreate()
        {
            NavigationManager.NavigateTo("product/create");
        }
        private async Task UpdateProductApproveAsync(ProductDto Product, ApprovalState state)
        {
            await ProductAppService.UpdateApprovalStateAsync(Product.Id, state);
            await GetProductsAsync();
        }
        private async Task DeleteProductAsync(ProductDto Product)
        {
            var confirmMessage = L["ProductDeletionConfirmationMessage", Product.Name];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await ProductAppService.DeleteAsync(Product.Id);
            await GetProductsAsync();
        }
    }
}
