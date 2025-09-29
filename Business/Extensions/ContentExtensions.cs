using Umbraco.Cms.Core.Models.PublishedContent;

namespace nackademin24_umbraco.Business.Extensions
{
    public static class ContentExtensions
    {
        public static string? GetFullUrl(this IPublishedContent content, string language) =>
        content?.IsPublished(language) == true
            ? content.Url(mode: UrlMode.Absolute, culture: language)
            : null;
    }
}