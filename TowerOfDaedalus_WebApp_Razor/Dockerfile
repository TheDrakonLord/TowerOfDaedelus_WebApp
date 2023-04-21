#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TowerOfDaedalus_WebApp_Razor/TowerOfDaedalus_WebApp_Razor.csproj", "TowerOfDaedalus_WebApp_Razor/"]
RUN dotnet restore "TowerOfDaedalus_WebApp_Razor/TowerOfDaedalus_WebApp_Razor.csproj"
COPY . .
WORKDIR "/src/TowerOfDaedalus_WebApp_Razor"
RUN dotnet build "TowerOfDaedalus_WebApp_Razor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TowerOfDaedalus_WebApp_Razor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TowerOfDaedalus_WebApp_Razor.dll"]