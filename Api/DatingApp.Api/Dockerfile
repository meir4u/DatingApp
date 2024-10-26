# Set the base image for the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 5000 5001 

# Set the ASP.NET Core environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80;http://+:8080;https://+:443

# Set the base image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG BUILD_CONFIGURATION=Release

# Copy project files individually for each layer to leverage caching
COPY ["./Api/DatingApp.Api/DatingApp.Api.csproj", "Api/DatingApp.Api/"]
COPY ["./Api/Core/DatingApp.Domain/DatingApp.Domain.csproj", "Api/Core/DatingApp.Domain/"]
COPY ["./Api/Core/DatingApp.Application/DatingApp.Application.csproj", "Api/Core/DatingApp.Application/"]
COPY ["./Api/Infrastructure/DatingApp.Infrastructure/DatingApp.Infrastructure.csproj", "Api/Infrastructure/DatingApp.Infrastructure/"]
COPY ["./Api/Infrastructure/DatingApp.Persistence/DatingApp.Persistence.csproj", "Api/Infrastructure/DatingApp.Persistence/"]
COPY ["./Shared/DatingApp.Common/DatingApp.Common.csproj", "Shared/DatingApp.Common/"]

# Restore dependencies
RUN dotnet restore "./Api/DatingApp.Api/DatingApp.Api.csproj"

# Copy the full project contents
COPY . .

# Set working directory and build
WORKDIR "/src/Api/DatingApp.Api"
RUN dotnet build "./DatingApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./DatingApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image setup
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define the entry point for the application
ENTRYPOINT ["dotnet", "DatingApp.Api.dll"]

# Healthcheck to ensure the app is running correctly
# HEALTHCHECK --interval=30s --timeout=10s --start-period=5s CMD curl --fail http://localhost:8080/health || exit 1
