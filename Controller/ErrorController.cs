using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using nackademin24_umbraco.Models.ViewModels;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Controller
{
    public class ErrorController : RenderController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public ErrorController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public override IActionResult Index()
        {
            if (CurrentPage is Error errorPage)
            {
                var model = new ErrorPageViewModel(errorPage, _umbracoContextAccessor);

                return View("error", model);
            }

            return null;
        }
    }
}