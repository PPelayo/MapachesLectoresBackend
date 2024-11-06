using dotenv.net;
using MapachesLectoresBackend.Core.Data.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
builder.Configuration.AddEnvironmentVariables();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MapachesDbContext>(config =>
{
    // var connectionString = DotNevUtils.Get("MAPACHES_CONNECTION_STRING");
    var connectionString = builder.Configuration["MAPACHES_CONNECTION_STRING"];
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
    config.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    config.UseMySql(connectionString, serverVersion);
});

builder.Services.AddControllers();

var app = builder.Build();

// // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}


app.MapControllers();
app.UseHttpsRedirection();

app.Run();