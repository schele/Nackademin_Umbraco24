using Microsoft.AspNetCore.Components;
using nackademin24_umbraco.Business.Services.Interfaces;
using nackademin24_umbraco.Models.Blazor;
using System.Globalization;

namespace nackademin24_umbraco.Shared
{
    public partial class Find : ComponentBase
    {
        [Inject] private IFindService _findService { get; set; } = default!;

        [Parameter] public string CultureName { get; set; } = default!;

        protected CultureInfo CultureInfo { get; set; } = default!;

        protected FindModel FindModel { get; set; } = new();
        
        protected bool IsSearching { get; set; }
        
        protected List<Hit> Pages { get; set; } = [];
        
        protected bool HasSearched { get; set; }

        protected override Task OnInitializedAsync()
        {
            CultureInfo = new CultureInfo(CultureName);

            return Task.CompletedTask;
        }

        protected async Task FindContentAsync()
        {
            if (string.IsNullOrWhiteSpace(FindModel.Query))
            {
                return;
            }
            else
            {
                Pages.Clear();
                IsSearching = true;
                HasSearched = false;

                try
                {
                    await Task.Delay(750); // Simulate delay or debounce
                    Pages = _findService.FindContent(FindModel.Query, CultureInfo);
                }
                finally
                {
                    IsSearching = false;
                    HasSearched = true;
                }
            }
        }
    }
}