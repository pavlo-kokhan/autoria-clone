FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AutoriaClone.Api/AutoriaClone.Api.csproj", "AutoriaClone.Api/"]
COPY ["AutoriaClone.Domain/AutoriaClone.Domain.csproj", "AutoriaClone.Domain/"]
COPY ["AutoriaClone.Infrastructure/AutoriaClone.Infrastructure.csproj", "AutoriaClone.Infrastructure/"]
RUN dotnet restore "AutoriaClone.Api/AutoriaClone.Api.csproj"
COPY . .
WORKDIR "/src/AutoriaClone.Api"
RUN dotnet build "./AutoriaClone.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AutoriaClone.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoriaClone.Api.dll"]
