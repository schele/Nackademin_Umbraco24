using nackademin24_umbraco.Models.Blazor;

namespace nackademin24_umbraco.Business.Services.Interfaces
{
    public interface IOmdbService
    {
        Task<List<OmdbMovie>> SearchAsync(OmdbSearchModel search);

        Task<string?> AddMovieAsync(string id);

        string? MoviePageUrl(string imdbId);
    }
}