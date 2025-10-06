using Examine;
using nackademin24_umbraco.Business.Services.Interfaces;
using nackademin24_umbraco.Models.Blazor;
using System.Globalization;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace nackademin24_umbraco.Business.Services
{
    public class FindService : IFindService
    {
        private readonly IExamineManager _examineManager;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IVariationContextAccessor _variationContextAccessor;

        public FindService(IExamineManager examineManager, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor)
        {
            _examineManager = examineManager;
            _umbracoContextAccessor = umbracoContextAccessor;
            _variationContextAccessor = variationContextAccessor;
        }

        public List<Hit> FindContent(string query, CultureInfo cultureInfo)
        {
            if (!_examineManager.TryGetIndex("ExternalIndex", out var index))
            {
                return [];
            }

            var searcher = index.Searcher;
            var criteria = searcher.CreateQuery("content");
            var hits = new List<Hit>();
            var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();

            // Execute the search query
            var results = criteria
                .ManagedQuery(query)
                .Execute();

            foreach (var item in results)
            {
                var page = umbracoContext?.Content?.GetById(int.Parse(item.Id));
                var name = page.Name(_variationContextAccessor, cultureInfo.Name);

                var hit = new Hit
                {
                    Name = name,
                    Description = "",
                    Url = page.Url(cultureInfo.Name),
                    UpdateDate = page.UpdateDate
                };

                hits.Add(hit);
            }

            return hits;
        }
    }
}