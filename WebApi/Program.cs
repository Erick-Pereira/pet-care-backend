using AspNetCoreRateLimit;
using BLL.Validation;
using DAL;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using web_api.Services;
using WebApi.Extensions;

#if DEBUG
var envPath = Path.Combine(Directory.GetCurrentDirectory(), "..", ".env");
if (File.Exists(envPath))
{
    DotNetEnv.Env.Load(envPath);
}
#endif

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure the DbContext with a connection string
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(databaseUrl))
{
    throw new InvalidOperationException("DATABASE_URL not found in environment variables");
}

// Parse database URL and create proper connection string
var uri = new Uri(databaseUrl);
var userInfo = uri.UserInfo.Split(':');
var connectionString = new StringBuilder();
connectionString.Append($"Host={uri.Host};");
connectionString.Append($"Database={uri.AbsolutePath.TrimStart('/')};");
connectionString.Append($"Username={userInfo[0]};");
connectionString.Append($"Password={userInfo[1]};");
connectionString.Append("SSL Mode=Require;");
connectionString.Append("Trust Server Certificate=true;");
connectionString.Append("Pooling=true;");
connectionString.Append("Minimum Pool Size=0;");
connectionString.Append("Maximum Pool Size=100;");

// Add debug logging
Console.WriteLine($"Connection string: {connectionString}");

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<DataBaseDbContext>(options =>
    options.UseNpgsql(connectionString.ToString()));

// Configure JWT authentication
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 16)
{
    throw new InvalidOperationException($"JWT_KEY must be at least 16 characters long. Current length: {jwtKey?.Length ?? 0}");
}

var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

#region Services

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register validators
builder.Services.AddScoped<AddressValidator>();
builder.Services.AddScoped<UserValidator>();
builder.Services.AddScoped<NeighborhoodValidator>();
builder.Services.AddScoped<CityValidator>();
builder.Services.AddScoped<StateValidator>();
builder.Services.AddScoped<PetValidator>();
builder.Services.AddScoped<BreedValidator>();
builder.Services.AddScoped<SpecieValidator>();

// Register JWT service
builder.Services.AddScoped<JwtService>();

// Register other services
builder.Services.AddApplicationServices();

#endregion Services

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Substitua pela URL do frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:InstrumentationKey"]);

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

// Configure rate limiting
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pet Care API",
        Version = "v1",
        Description = "API for managing pet care services"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication(); // Deve vir antes do UseAuthorization
app.UseAuthorization();

app.UseIpRateLimiting();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();