#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TowerOfDaedalus_WebApp_DiscordBot/TowerOfDaedalus_WebApp_DiscordBot.csproj", "TowerOfDaedalus_WebApp_DiscordBot/"]
RUN dotnet restore "TowerOfDaedalus_WebApp_DiscordBot/TowerOfDaedalus_WebApp_DiscordBot.csproj"
COPY . .
WORKDIR "/src/TowerOfDaedalus_WebApp_DiscordBot"
RUN dotnet build "TowerOfDaedalus_WebApp_DiscordBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TowerOfDaedalus_WebApp_DiscordBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TowerOfDaedalus_WebApp_DiscordBot.dll"]