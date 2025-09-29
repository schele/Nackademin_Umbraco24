using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using nackademin24_umbraco.Business.Services.Interfaces;
using nackademin24_umbraco.Models.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Controller
{
    public class SitemapController : RenderController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ISitemapService _sitemapService;

        public SitemapController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IPublishedValueFallback publishedValueFallback, ISitemapService sitemapService) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _publishedValueFallback = publishedValueFallback;
            _sitemapService = sitemapService;
        }

        public override IActionResult Index()
        {
            if (CurrentPage is Sitemap sitemap)
            {
                var model = new SitemapViewModel(sitemap, _publishedValueFallback)
                {
                    Pages = _sitemapService.Pages()
                };
                
                var xml = _sitemapService.GenerateSitemapXml(model);

                return Content(xml, "text/xml");
            }

            return NotFound();
        }
    }
}