using nackademin24_umbraco.Business.ScheduledJobs;
using nackademin24_umbraco.Business.ScheduledJobs.Interfaces;
using nackademin24_umbraco.Business.Services;
using nackademin24_umbraco.Business.Services.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var environmentName = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.GetConnectionString("umbracoDbDSN");

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IMoviesJob, MoviesJob>();
builder.Services.AddScoped<ISitemapService, SitemapService>();
builder.Services.AddScoped<IOmdbService, OmdbService>();

WebApplication app = builder.Build();

app.MapBlazorHub();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
