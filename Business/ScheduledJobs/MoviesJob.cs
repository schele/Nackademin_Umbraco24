using Hangfire.Console;
using Hangfire.Server;
using nackademin24_umbraco.Business.ScheduledJobs.Interfaces;

namespace nackademin24_umbraco.Business.ScheduledJobs
{
    public class MoviesJob : IMoviesJob
    {
        public void AddMovies(PerformContext context)
        {
            var progressBar = context.WriteProgressBar();
            var movies = new List<MyMovie>();

            for (int i = 0; i < 5000; i++)
            {
                var movie = new MyMovie
                {
                    Name = i.ToString()
                };

                movies.Add(movie);
            }

            foreach (var movie in  movies.WithProgress(progressBar, movies.Count()))
            {
                context.WriteLine($"Movie {movie.Name} added");
            }
        }
    }

    internal class MyMovie
    {
        public string Name { get; set; }
    }
}