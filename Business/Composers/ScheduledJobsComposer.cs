using Hangfire;
using nackademin24_umbraco.Business.ScheduledJobs.Interfaces;
using Umbraco.Cms.Core.Composing;

namespace nackademin24_umbraco.Business.Composers
{
    public class ScheduledJobsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            RecurringJob.AddOrUpdate<IMoviesJob>("Add movies", x => x.AddMovies(null), Cron.Never);
        }
    }
}