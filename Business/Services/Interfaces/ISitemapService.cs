using nackademin24_umbraco.Models.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace nackademin24_umbraco.Business.Services.Interfaces
{
    public interface ISitemapService
    {
        IEnumerable<IPublishedContent> Pages();

        string GenerateSitemapXml(SitemapViewModel model);
    }
}