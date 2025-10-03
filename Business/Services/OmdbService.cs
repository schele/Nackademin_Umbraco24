using nackademin24_umbraco.Business.Services.Interfaces;
using nackademin24_umbraco.Models.Blazor;
using Newtonsoft.Json;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace nackademin24_umbraco.Business.Services
{
    public class OmdbService : IOmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OmdbService> _logger;
        private readonly IContentService _contentService;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IPublishedContentQuery _publishedContentQuery;

        public OmdbService(HttpClient httpClient, ILogger<OmdbService> logger, IContentService contentService, IUmbracoContextAccessor umbracoContextAccessor, IPublishedContentQuery publishedContentQuery)
        {
            _httpClient = httpClient;
            _logger = logger;
            _contentService = contentService;
            _umbracoContextAccessor = umbracoContextAccessor;
            _publishedContentQuery = publishedContentQuery;
        }

        public async Task<List<Movie>> SearchAsync(OmdbSearchModel search)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.omdbapi.com/?s={search.Query}&apikey=504e93de");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Root>(json);

                    if (result != null)
                    {
                        return result.Search;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return [];
        }

        public async Task<string?> AddMovieAsync(string id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.omdbapi.com/?i={id}&apikey=504e93de");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var movie = JsonConvert.DeserializeObject<MovieDetails>(json);

                    if (movie != null)
                    {
                        movie.Culture = "sv";

                        var imdbId = CreateMoviePage(movie);

                        return MoviePageUrl(imdbId);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return string.Empty;
        }

        public string? MoviePageUrl(string imdbId)
        {
            if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                var content = umbracoContext.Content;
                var settingsPage = content?.GetAtRoot().DescendantsOrSelf<Settings>().FirstOrDefault();
                var moviesContainer = _publishedContentQuery.Content(settingsPage.MoviesContainer.Id);
                var movie = moviesContainer?
                    .Children<OmdbMovie>()
                    .FirstOrDefault(x => x.ImdbId == imdbId);

                return movie?.Url();
            }

            return string.Empty;
        }

        private string CreateMoviePage(MovieDetails movie)
        {
            if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                var content = umbracoContext.Content;
                var settingsPage = content?.GetAtRoot().DescendantsOrSelf<Settings>().FirstOrDefault();
                var moviePage = _contentService.Create(movie.Title, settingsPage.MoviesContainer.Id, nameof(OmdbMovie).ToLower());

                moviePage.SetCultureName(movie.Title, movie.Culture);

                moviePage.SetValue("title", movie.Title, movie.Culture);
                moviePage.SetValue("year", movie.Year);
                moviePage.SetValue("imdbid", movie.ImdbID);
                moviePage.SetValue("rated", movie.Rated, movie.Culture);

                var save = _contentService.Save(moviePage);

                if (save.Success)
                {
                    _contentService.Publish(moviePage, [movie.Culture]);

                    return movie.ImdbID;
                }
            }

            return string.Empty;
        }
    }
}