using Horeca.Categories;
using Horeca.Products;
using Horeca.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Horeca.Blazor.Pages.Product
{
    public partial class Category
    {
        public IEnumerable<CategoryDto> Items { get; set; }
        public IList<CategoryDto> ExpandedNodes = new List<CategoryDto>();
        public CategoryDto SelectedNode { get; set; }
        [Parameter]
        public string SelectedStep { get; set; }
        [Parameter]
        public EventCallback<string> SelectedStepChanged { get; set; }
        [Parameter]
        public CreateUpdateProductDto ProductDetails { get; set; }
        [Parameter]
        public EventCallback<CreateUpdateProductDto> ProductDetailsChanged { get; set; }
        [Parameter]
        public bool IsExistingProduct { get; set; }
        [Parameter]
        public EventCallback<bool> IsExistingProductChanged { get; set; }
        [Inject]
        public ICategoryAppService CategoryAppService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Items = await CategoryAppService.GetRootCategories();
        }

        public async Task NavigateTo(string step)
        {
            if (step == StepName.Details)
            {
                await ProductDetailsChanged.InvokeAsync(new CreateUpdateProductDto
                {
                    CategoryId = SelectedNode.Id,
                    CategoryName = SelectedNode.Name
                });
                await IsExistingProductChanged.InvokeAsync(false);
            }
            await SelectedStepChanged.InvokeAsync(step);
        }
    }
}
