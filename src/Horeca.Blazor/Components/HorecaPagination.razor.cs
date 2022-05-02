using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Horeca.Blazor.Components
{
    public partial class HorecaPagination
    {
        [Parameter]
        public int Spread { get; set; }
        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }
        [Parameter]
        public PaginationData MetaData { get; set; }

        private List<PagingLink> _links;

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }
        private void CreatePaginationLinks()
        {
            _links = new List<PagingLink>();
            _links.Add(new PagingLink(MetaData.CurrentPage - 1, MetaData.HasPrevious, "Previous"));
            for (int i = 1; i <= MetaData.TotalCount; i++)
            {
                if (i >= MetaData.CurrentPage - Spread && i <= MetaData.CurrentPage + Spread)
                {
                    _links.Add(new PagingLink(i, true, i.ToString()) { Active = MetaData.CurrentPage == i });
                }
            }
            _links.Add(new PagingLink(MetaData.CurrentPage + 1, MetaData.HasNext, "Next"));
        }
        private async Task OnSelectedPage(PagingLink link)
        {
            if (link.Page == MetaData.CurrentPage || !link.Enabled)
                return;
            MetaData.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
        
    }
    public class PaginationData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
    public class PagingLink
    {
        public string Text { get; set; }
        public int Page { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public PagingLink(int page, bool enabled, string text)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
        }
    }
}
