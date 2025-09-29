using nackademin24_umbraco.Business.Extensions;
using nackademin24_umbraco.Business.Services.Interfaces;
using nackademin24_umbraco.Models.ViewModels;
using System.Text;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Business.Services
{
    public class SitemapService(IUmbracoContextAccessor umbracoContextAccessor) : ISitemapService
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor = umbracoContextAccessor;

        public IEnumerable<IPublishedContent> Pages()
        {
            if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                var content = umbracoContext.Content;

                if (content != null)
                {
                    var startPage = content.GetAtRoot().DescendantsOrSelf<Start>().FirstOrDefault();

                    if (startPage != null)                    
                    {
                        return startPage.DescendantsOrSelf<IPublishedContent>()
                            .Where(page => page is IBase basePage && page.IsPublished()).ToList();
                    }
                }
            }

            return [];
        }

        public string GenerateSitemapXml(SitemapViewModel model)
        {
            if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                var currentCulture = umbracoContext.PublishedRequest?.Culture;
                var sb = new StringBuilder();

                var localizedPages = model.Pages
                    .Where(p => p.IsPublished(currentCulture))
                    .Select(p => new
                    {
                        Page = p,
                        Url = p.GetFullUrl(currentCulture),
                        LastModified = p.UpdateDate
                    })
                    .ToList();

                sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                sb.AppendLine("<urlset xmlns=\"https://www.sitemaps.org/schemas/sitemap/0.9\">");

                foreach (var page in localizedPages)
                {
                    sb.AppendLine($"<url><loc>{page.Url}</loc><lastmod>{page.LastModified:yyyy-MM-dd HH:mm:ss}</lastmod></url>");
                }

                sb.AppendLine("</urlset>");

                return sb.ToString();
            }

            return string.Empty;
        }
    }
}