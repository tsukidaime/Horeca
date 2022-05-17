using System;
using Horeca.Blazor.State;
using Microsoft.AspNetCore.Components;
namespace Horeca.Blazor.Pages.Product
{
    public partial class Index
    {
        [Parameter]
        public Guid? ProductId { get; set; }

        [Parameter]
        public Guid? ProductBid { get; set; }
    }
}
