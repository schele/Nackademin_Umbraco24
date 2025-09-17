using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using nackademin24_umbraco.Views;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Controller
{
    public class StartController : RenderController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public StartController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public override IActionResult Index()
        {
            if (CurrentPage is Start startPage)
            {
                var model = new StartPageViewModel(startPage, _umbracoContextAccessor)
                {
                    Heading = startPage.Heading
                };

                return CurrentTemplate(model);
            }

            return null;
        }
    }
}