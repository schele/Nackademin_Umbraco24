using Microsoft.Data.SqlClient;
using nackademin24_umbraco.Business.ScheduledJobs;
using nackademin24_umbraco.Business.ScheduledJobs.Interfaces;
using nackademin24_umbraco.Business.Services;
using nackademin24_umbraco.Business.Services.Interfaces;
using NPoco;

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

builder.Services.AddScoped<IDatabase>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("umbracoDbDSN");
    var db = new Database(connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance);

    return db;
});

builder.Services.AddScoped<IMoviesJob, MoviesJob>();
builder.Services.AddScoped<ISitemapService, SitemapService>();
builder.Services.AddScoped<IOmdbService, OmdbService>();
builder.Services.AddScoped<IFindService, FindService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

WebApplication app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var reviewService = scope.ServiceProvider.GetRequiredService<IReviewService>();
    await reviewService.EnsureTableExistsAsync();
}

app.MapBlazorHub();

await app.BootUmbracoAsync();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;
    if (path != "/" && path.EndsWith("/"))
    {
        context.Response.Redirect(path.TrimEnd('/'));
        return;
    }
    await next();
});

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