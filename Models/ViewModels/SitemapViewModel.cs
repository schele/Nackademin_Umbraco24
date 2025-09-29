using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Models.ViewModels
{
    public class SitemapViewModel : Sitemap
    {
        public SitemapViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public IEnumerable<IPublishedContent> Pages { get; set; }
    }
}