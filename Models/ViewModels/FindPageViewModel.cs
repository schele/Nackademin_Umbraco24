using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Models.ViewModels
{
    public class FindPageViewModel : PageViewModel<Find>
    {
        public FindPageViewModel(Find content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
        }
    }
}