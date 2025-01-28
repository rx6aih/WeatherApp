using AuthService.Utility.Jwt;

namespace WeatherApp.API.Extensions;

public class JwtMiddleware(JwtProvider provider) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = context.Request.Cookies["token"];

        if (!string.IsNullOrEmpty(token))
        {
            var userId = provider.ValidateToken(token);

            if (Int32.TryParse(userId, out int _))
            {
                context.Items["userId"] = userId;
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
        await next(context);
    }
}