# Set the base image for the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 443
#EXPOSE 8081

# Set the ASP.NET Core environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production

# Set the base image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG BUILD_CONFIGURATION=Release

COPY ["./Api/DatingApp.Api/DatingApp.Api.csproj", "Api/DatingApp.Api/"]
COPY ["./Api/Core/DatingApp.Domain/DatingApp.Domain.csproj", "Api/Core/DatingApp.Domain/"]
COPY ["./Api/Core/DatingApp.Application/DatingApp.Application.csproj", "Api/Core/DatingApp.Application/"]
COPY ["./Api/Infrastructure/DatingApp.Infrastructure/DatingApp.Infrastructure.csproj", "Api/Infrastructure/DatingApp.Infrastructure/"]
COPY ["./Api/Infrastructure/DatingApp.Persistence/DatingApp.Persistence.csproj", "Api/Infrastructure/DatingApp.Persistence/"]
COPY ["./Shared/DatingApp.Common/DatingApp.Common.csproj", "Shared/DatingApp.Common/"]
#COPY ["Api/DatingApp.Api/DatingApp.Api.csproj", "Api/DatingApp.Api/"]
RUN dotnet restore "./Api/DatingApp.Api/DatingApp.Api.csproj"
COPY . .
WORKDIR "/src/Api/DatingApp.Api"
RUN dotnet build "./DatingApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./DatingApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DatingApp.Api.dll"]

HEALTHCHECK --interval=30s --timeout=10s --start-period=5s CMD curl --fail http://localhost:8080/health || exit 1

