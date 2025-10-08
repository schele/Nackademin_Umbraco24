using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

public class PageViewModel<T> : IBase where T : IBase
{
    public readonly T Content;
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;

    public PageViewModel(T content, IUmbracoContextAccessor umbracoContextAccessor)
    {
        Content = content;
        _umbracoContextAccessor = umbracoContextAccessor;
    }

    public Start StartPage
    {
        get
        {
            if (_umbracoContextAccessor.TryGetUmbracoContext(out var context))
            {
                var content = context.Content;
                return content.GetAtRoot().DescendantsOrSelf<Start>().FirstOrDefault();
            }

            return null;
        }
    }

    public string MetaDescription => Content.MetaDescription;

    public int Id => Content.Id;

    public string Name => Content.Name;

    public string? UrlSegment => Content.UrlSegment;

    public int SortOrder => Content.SortOrder;

    public int Level => Content.Level;

    public string Path => Content.Path;

    public int? TemplateId => Content.TemplateId;

    public int CreatorId => Content.CreatorId;

    public DateTime CreateDate => Content.CreateDate;

    public int WriterId => Content.WriterId;

    public DateTime UpdateDate => Content.UpdateDate;

    public IReadOnlyDictionary<string, PublishedCultureInfo> Cultures => Content.Cultures;

    public PublishedItemType ItemType => Content.ItemType;

    public IPublishedContent? Parent => Content.Parent;

    public IEnumerable<IPublishedContent> Children => Content.Children;

    public IEnumerable<IPublishedContent> ChildrenForAllCultures => Content.ChildrenForAllCultures;

    public IPublishedContentType ContentType => Content.ContentType;

    public Guid Key => Content.Key;

    public IEnumerable<IPublishedProperty> Properties => Content.Properties;

    public IPublishedProperty? GetProperty(string alias) => Content.GetProperty(alias);

    public bool IsDraft(string? culture = null) => Content.IsDraft(culture);

    public bool IsPublished(string? culture = null) => Content.IsPublished(culture);
}