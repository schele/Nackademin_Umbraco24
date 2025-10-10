using nackademin24_umbraco.Business.Services.Interfaces;
using nackademin24_umbraco.Models.Blazor;
using NPoco;

namespace nackademin24_umbraco.Business.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IDatabase _db;
        public event Action<Review>? ReviewAdded;

        public ReviewService(IDatabase db)
        {
            _db = db;
        }

        public async Task AddReviewAsync(Review review)
        {
            await _db.InsertAsync(review);

            ReviewAdded?.Invoke(review);
        }

        public async Task EnsureTableExistsAsync()
        {
            var sql = @"
                IF NOT EXISTS (SELECT * 
                               FROM INFORMATION_SCHEMA.TABLES 
                               WHERE TABLE_SCHEMA = 'dbo' 
                                 AND TABLE_NAME = 'Reviews')
                BEGIN
                    CREATE TABLE [dbo].[Reviews] (
                        [Id] INT IDENTITY(1,1) PRIMARY KEY,
                        [ImdbId] NVARCHAR(50) NOT NULL,
                        [Name] NVARCHAR(100) NOT NULL,
                        [Comment] NVARCHAR(MAX) NOT NULL,
                        [Rating] INT NOT NULL,
                        [Date] DATETIME NOT NULL DEFAULT GETDATE()
                    );

                    CREATE NONCLUSTERED INDEX IX_Reviews_ImdbId
                    ON [dbo].[Reviews] ([ImdbId]);
                END
                ";
            await _db.ExecuteAsync(sql);
        }

        public async Task<List<Review>> GetReviewsAsync(string imdbId)
        {
            var reviews = await _db.Query<Review>()
            .Where(x => x.ImdbId == imdbId)
            .OrderByDescending(x => x.Date).ToListAsync();

            return reviews;
        }
    }
}