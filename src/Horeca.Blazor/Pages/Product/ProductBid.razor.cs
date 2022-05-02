using Horeca.ProductBids;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Horeca.Blazor.Pages.Product
{
    public partial class ProductBid
    {
        [Parameter]
        public CreateUpdateProductBidDto ProductBidDto { get; set; }
        [Parameter]
        public EventCallback<CreateUpdateProductBidDto> ProductBidDtoChanged { get; set; }
        [Parameter]
        public string SelectedStep { get; set; }
        [Parameter]
        public EventCallback<string> SelectedStepChanged { get; set; }
        public async Task NavigateTo(string step)
        {
            await SelectedStepChanged.InvokeAsync(step);
            await ProductBidDtoChanged.InvokeAsync(ProductBidDto);
        }
    }
}
