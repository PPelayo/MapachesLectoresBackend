using dotenv.net;
using MapachesLectoresBackend.Auth.Data.Utils;
using MapachesLectoresBackend.Auth.Domain.Service;
using MapachesLectoresBackend.Auth.Domain.UseCase;
using MapachesLectoresBackend.Auth.Domain.Utils;
using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Books.Data.UnitOfWork;
using MapachesLectoresBackend.Books.Domain.UnitOfWork;
using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Core.Data.Repository;
using MapachesLectoresBackend.Core.Data.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.UseCase;
using MapachesLectoresBackend.Core.Presentation.Specification;
using MapachesLectoresBackend.Reviews.Domain.UseCase;
using MapachesLectoresBackend.Users.Domain.UseCase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load(new DotEnvOptions().WithEnvFiles(".env.dev", ".env"));
builder.Configuration.AddEnvironmentVariables();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(conf =>
{
    conf.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .Build();
    });
});

builder.Services.AddDbContext<MapachesDbContext>(config =>
{
    // var connectionString = DotNevUtils.Get("MAPACHES_CONNECTION_STRING");
    var connectionString = builder.Configuration["MAPACHES_CONNECTION_STRING"];
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
    config.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    config.UseMySql(connectionString, serverVersion);
});

builder.Services.AddControllers();

#region CORE
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IHttpContextService), typeof(HttpContextService));
builder.Services.AddScoped(typeof(GetItemByUuidUseCase<>));

#endregion

#region USER
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<GetUserByIdUseCase>();
#endregion

#region AUTH
builder.Services.AddScoped(typeof(IJwtUtils), typeof(JwtUtils));
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<RefreshTokenUseCase>();
#endregion

#region BOOKS

builder.Services.AddScoped<GetBooksUseCase>();
builder.Services.AddScoped<GetBookByIdUseCase>();
builder.Services.AddScoped<GetBookByUuidUseCase>();
builder.Services.AddScoped<CreateBookUseCase>();

builder.Services.AddScoped<CreateAuthorUseCase>();
builder.Services.AddScoped<GetAuthorsUseCase>();

builder.Services.AddScoped<CreatePublisherUseCase>();

builder.Services.AddScoped<ICreateBookUnitOfWork, CreateBookUnitOfWork>();

#endregion

#region Review

builder.Services.AddScoped<CreateReviewUseCase>();
builder.Services.AddScoped<GetReviewsFromBookUseCase>();

#endregion




var app = builder.Build();

// // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}


app.UseCors("AllowAll");
app.UseMiddleware<AuthenticatedMiddleware>();
app.MapControllers();
// app.UseHttpsRedirection();

app.Run();