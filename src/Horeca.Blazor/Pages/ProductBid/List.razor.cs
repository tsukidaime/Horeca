using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blazorise;
using Blazorise.DataGrid;
using Horeca.ProductBids;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Application.Dtos;

namespace Horeca.Blazor.Pages.ProductBid
{
    public partial class List
    {
        [Parameter]
        public string ProductId { get; set; }
        private IReadOnlyList<ProductBidDto> ProductBidList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        private Guid EditingBidId { get; set; }
        private CreateUpdateProductBidDto EditingBid { get; set; }

        private Modal EditBidModal { get; set; }

        private Validations EditValidationsRef;

        public List()
        {
            EditingBid = new CreateUpdateProductBidDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetBidsAsync();
        }

        private async Task GetBidsAsync()
        {
            var result = await ProductBidAppService.GetListByProductIdAsync(
                new GetProductBidListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    ProductId = Guid.Parse(ProductId)
                }
            );

            ProductBidList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProductBidDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.None)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetBidsAsync();

            await InvokeAsync(StateHasChanged);
        }

        private void OpenEditProductBidModal(ProductBidDto bid)
        {
            EditValidationsRef.ClearAll();

            EditingBidId = bid.Id;
            EditingBid = ObjectMapper.Map<ProductBidDto, CreateUpdateProductBidDto>(bid);
            EditBidModal.Show();
        }

        private async Task DeleteProductBidAsync(ProductBidDto bid)
        {
            var confirmMessage = L["DeletionConfirmationMessage"];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await ProductBidAppService.DeleteAsync(bid.Id);
            await GetBidsAsync();
        }

        private void CloseEditProductBidModal()
        {
            EditBidModal.Hide();
        }

        private async Task UpdateProductBidAsync()
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await ProductBidAppService.UpdateAsync(EditingBidId, EditingBid);
                await GetBidsAsync();
                await EditBidModal.Hide();
            }
        }
    }
}