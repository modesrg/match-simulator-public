using GB.MatchSimulator.Clients;
using GB.MatchSimulator.Clients.Interfaces;
using GB.MatchSimulator.DAL;
using GB.MatchSimulator.DAL.Local;
using GB.MatchSimulator.DAL.Repositories;
using GB.MatchSimulator.DAL.Repositories.Interfaces;
using GB.MatchSimulator.Options;
using GB.MatchSimulator.Services;
using GB.MatchSimulator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();

builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<IRoundService, RoundService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.Configure<SimulatorOptions>(builder.Configuration.GetSection(SimulatorOptions.Simulator));

// Using InMemory as a placeholder for the final Database
builder.Services.AddDbContext<SimulatorDbContext>(opt => opt.UseInMemoryDatabase("DemoDB"));

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7207/api/";
builder.Services.AddHttpClient<ITournamentApiClient, TournamentApiClient>(o => o.BaseAddress = new Uri(apiBaseUrl));

var app = builder.Build();

if (app.Environment.IsDevelopment() || apiBaseUrl.Contains("localhost"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Seed the Demo DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SimulatorDbContext>();
    db.Database.EnsureCreated();
    TeamSeeder.SeedIfEmpty(db);
}

app.Run();
