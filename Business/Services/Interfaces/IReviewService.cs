using nackademin24_umbraco.Models.Blazor;

namespace nackademin24_umbraco.Business.Services.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(Review review);

        Task EnsureTableExistsAsync();

        Task<List<Review>> GetReviewsAsync(string imdbId);

        event Action<Review>? ReviewAdded;
    }
}