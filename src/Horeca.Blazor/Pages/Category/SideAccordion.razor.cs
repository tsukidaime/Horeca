using Blazorise;
using Horeca.Blazor.Pages.Product;
using Horeca.Blazor.State;
using Horeca.Categories;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Horeca.Blazor.Pages.Category
{
    public partial class SideAccordion : IDisposable
    {
        public IList<CategoryDto> Items { get; set; }
        public Dictionary<Guid, bool> ItemVisibilities { get; } = new Dictionary<Guid, bool>();
        [Inject]
        public ICategoryAppService CategoryAppService { get; set; }
        [Inject]
        public ProductGridState ProductGridState { get; set; }
        public string SelectedChild { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ProductGridState.OnChange += StateHasChanged;
            Items = await CategoryAppService.GetRootCategories();
            foreach (var item in Items)
                ItemVisibilities.Add(item.Id, false);
        }

        public async Task SelectCategory(Guid id)
        {
            ProductGridState.CategoryId = id;
        }

        public async Task SelectHead(Guid id)
        {
            ItemVisibilities[id] = !ItemVisibilities[id];
            foreach(var item in ItemVisibilities.Where(x=>x.Key != id))
            {
                ItemVisibilities[item.Key] = false;
            }
            await SelectCategory(id);
            SelectedChild = string.Empty;
        }
        public void Dispose()
        {
            ProductGridState.OnChange -= StateHasChanged;
        }
    }
}
