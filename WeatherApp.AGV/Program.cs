using AuthService.DAL;
using AuthService.DAL.Implementations;
using AuthService.Extensions;
using AuthService.Services;
using AuthService.Utility.Jwt;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<JwtProvider>();
builder.Services.AddTransient<UserService>();
builder.Services.AddDal();

builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddOcelot();

var app = builder.Build();

await app.UseOcelot();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();