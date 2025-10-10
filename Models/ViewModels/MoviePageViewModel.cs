using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Models.ViewModels
{
    public class MoviePageViewModel : PageViewModel<Movie>
    {
        public MoviePageViewModel(Movie content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
        }
    }
}