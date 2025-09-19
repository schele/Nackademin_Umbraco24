using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Models.ViewModels
{
    public class StartPageViewModel : PageViewModel<Start>
    {
        public StartPageViewModel(Start content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
        }

        public string Heading { get; set; }
    }
}