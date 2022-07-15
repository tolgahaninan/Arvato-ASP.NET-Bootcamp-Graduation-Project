using Microsoft.EntityFrameworkCore;
using GradBootcamp_Tolgahaninan.Data;
using GradBootcamp_Tolgahaninan.Repository;
using GradBootcamp_Tolgahaninan.Repository.IRepository;
using GradBootcamp_Tolgahaninan.Mapper;
using StackExchange.Redis;
using GradBootcamp_Tolgahaninan.Data.Redis.IRedis;
using GradBootcamp_Tolgahaninan.Data.Redis;
using GradBootcamp_Tolgahaninan.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using GradBootcamp_Tolgahaninan.BackgroundJobWorker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<BackgroundJobWorker>();

builder.Services.AddSwaggerGen(opt => // Swagger Configuration
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "TolgahanInan-Arvato ASP.NET Bootcamp Graduation", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Repository Implementation
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ITrendRepository, TrendRepository>();
builder.Services.AddScoped<IMovieViewRepository, MovieViewRepository>();
// Auto Mapper Implementation
builder.Services.AddAutoMapper(typeof(Mappings));
// Redis Helper Implementation
builder.Services.AddSingleton<IRedisHelper, RedisHelper>();
// Connection Multiplexer Implementation
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    EndPoints = { "localhost" },
                    Ssl = false,
                    AbortOnConnectFail = false,
                }));
// Application DB Context Implementation
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//JWT Token Configurations
var key = Encoding.ASCII.GetBytes(builder.Configuration["Application:JWTSymetricKey"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.Audience = "BootcampGraduation";
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.ClaimsIssuer = "BootcampGraduation.Issuer.Development";
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {

        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero // To make default toxen expiration time (5 mins) to 0 minutes
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
