using Blazorise;
using Horeca.Categories;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horeca.Blazor.Pages.Category
{
    public partial class SideAccordion
    {
        public IList<CategoryDto> Items { get; set; }
        public List<bool> ItemVisibilities { get; } = new List<bool>();
        [Inject]
        public ICategoryAppService CategoryAppService { get; set; }
        public string SelectedChild { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Items = await CategoryAppService.GetRootCategories();
            foreach (var item in Items)
                ItemVisibilities.Add(false);
        }
    }
}
