using AuthService.Utility.Jwt;
using Microsoft.Extensions.Caching.Distributed;
using WeatherApp.API.Extensions;
using WeatherApp.BL.Services;
using WeatherApp.BL.Utility;
using DistributedCacheExtensions = WeatherApp.DAL.DistributedCacheExtensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.Configure<SideApi>(builder.Configuration.GetSection(nameof(SideApi)));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "WeatherDAL_";
});
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddTransient<TemperatureService>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials();
    });
});

var app = builder.Build();


app.UseCors("ClientPermission");

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();