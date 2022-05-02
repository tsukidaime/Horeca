using Blazorise;
using Horeca.Categories;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Horeca.Blazor.Pages.Category
{
    public partial class SideAccordion
    {
        public IList<CategoryDto> Items { get; set; }
        public Dictionary<Guid, bool> ItemVisibilities { get; } = new Dictionary<Guid, bool>();
        [Inject]
        public ICategoryAppService CategoryAppService { get; set; }
        public string SelectedChild { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Items = await CategoryAppService.GetRootCategories();
            foreach (var item in Items)
                ItemVisibilities.Add(item.Id, false);
        }
    }
}
