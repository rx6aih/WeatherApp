FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WeatherApp.API/WeatherApp.API.csproj", "WeatherApp.API/"]
COPY ["AuthService/AuthService.csproj", "AuthService/"]
COPY ["AuthService.DAL/AuthService.DAL.csproj", "AuthService.DAL/"]
COPY ["WeatherApp.BL/WeatherApp.BL.csproj", "WeatherApp.BL/"]
COPY ["WeatherApp.DAL/WeatherApp.DAL.csproj", "WeatherApp.DAL/"]
RUN dotnet restore "WeatherApp.API/WeatherApp.API.csproj"
COPY . .
WORKDIR "/src/WeatherApp.API"
RUN dotnet build "WeatherApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WeatherApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherApp.API.dll"]
