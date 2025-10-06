using nackademin24_umbraco.Models.Blazor;
using System.Globalization;

namespace nackademin24_umbraco.Business.Services.Interfaces
{
    public interface IFindService
    {
        List<Hit> FindContent(string query, CultureInfo cultureInfo);

        //Task<List<MovieDetails>> GetMoviesWithDetailsAsync(string query);

        //bool MovieExists(string id);

        //void AddMovie(MovieDetails item);
    }
}