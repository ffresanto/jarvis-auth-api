FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /root
COPY ["src/JarvisAuth.API/JarvisAuth.API.csproj", "src/JarvisAuth.API/"]
COPY ["src/JarvisAuth.Application/JarvisAuth.Application.csproj", "src/JarvisAuth.Application/"]
COPY ["src/JarvisAuth.Core/JarvisAuth.Core.csproj", "src/JarvisAuth.Core/"]
COPY ["src/JarvisAuth.Domain/JarvisAuth.Domain.csproj", "src/JarvisAuth.Domain/"]
COPY ["src/JarvisAuth.Infrastructure/JarvisAuth.Infrastructure.csproj", "src/JarvisAuth.Infrastructure/"]
COPY ["tests/JarvisAuth.Tests/JarvisAuth.Tests.csproj", "tests/JarvisAuth.Tests/"]
RUN dotnet restore "src/JarvisAuth.API/JarvisAuth.API.csproj"
COPY . .
RUN dotnet build "src/JarvisAuth.API/JarvisAuth.API.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet test "tests/JarvisAuth.Tests/JarvisAuth.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/JarvisAuth.API/JarvisAuth.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JarvisAuth.API.dll"]