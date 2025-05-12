FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FazanaChat2.Server/FazanaChat2.Server.csproj", "FazanaChat2.Server/"]
RUN dotnet restore "FazanaChat2.Server/FazanaChat2.Server.csproj"
COPY . .
WORKDIR "/src/FazanaChat2.Server"
RUN dotnet build "./FazanaChat2.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FazanaChat2.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FazanaChat2.Server.dll"]
