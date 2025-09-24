using Hangfire.Server;

namespace nackademin24_umbraco.Business.ScheduledJobs.Interfaces
{
    public interface IMoviesJob
    {
        void AddMovies(PerformContext context);
    }
}