using System;
using Microsoft.AspNetCore.Components;
namespace Horeca.Blazor.Pages.Product
{
    public partial class Index
    {
        [Parameter]
        public Guid? CategoryId { get; set; }

        protected override void OnInitialized()
        {
            CategoryId = CategoryId ?? null;
        }
    }
}
