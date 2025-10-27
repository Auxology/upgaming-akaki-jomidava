FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Upgaming.WebApi/Upgaming.WebApi.csproj", "src/Upgaming.WebApi/"]
RUN dotnet restore "src/Upgaming.WebApi/Upgaming.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Upgaming.WebApi"
RUN dotnet build "./Upgaming.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Upgaming.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Upgaming.WebApi.dll"]
