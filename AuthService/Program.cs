using AuthService.Controllers;
using AuthService.DAL;
using AuthService.DAL.Context;
using AuthService.DAL.Implementations;
using AuthService.Extensions;
using AuthService.Services;
using AuthService.Utility.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));

builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddBusinessService();
builder.Services.AddDal();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<UserService>();
builder.Services.AddHostedService<ConsumerService>();

builder.Services.AddControllers();
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
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<AuthContext>();


app.UseCors("ClientPermission");

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();