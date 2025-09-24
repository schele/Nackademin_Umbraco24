using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Models.ViewModels
{
    public class ErrorPageViewModel : PageViewModel<Error>
    {
        public ErrorPageViewModel(Error content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
        }
    }
}