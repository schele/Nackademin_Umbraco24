using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using nackademin24_umbraco.Models.ViewModels;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Controller
{
    public class MovieController : RenderController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public MovieController(ILogger<MovieController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public override IActionResult Index()
        {
            if (CurrentPage is Movie moviePage)
            {
                var model = new MoviePageViewModel(moviePage, _umbracoContextAccessor);

                return View("movie", model);
            }

            return null;
        }
    }
}