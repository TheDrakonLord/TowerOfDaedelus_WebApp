#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TowerOfDaedelus_WebApp/TowerOfDaedelus_WebApp.csproj", "TowerOfDaedelus_WebApp/"]
COPY ["TowerOfDaedalus_WebApp_DiscordBot/TowerOfDaedalus_WebApp_DiscordBot.csproj", "TowerOfDaedalus_WebApp_DiscordBot/"]
RUN dotnet restore "TowerOfDaedelus_WebApp/TowerOfDaedelus_WebApp.csproj"
COPY . .
WORKDIR "/src/TowerOfDaedelus_WebApp"
RUN dotnet build "TowerOfDaedelus_WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TowerOfDaedelus_WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TowerOfDaedelus_WebApp.dll"]