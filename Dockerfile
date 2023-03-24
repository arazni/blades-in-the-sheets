#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN apt-get update
RUN apt-get install --no-install-recommends --yes python3
WORKDIR /src
COPY ["Server/Server.csproj", "Server/"]
COPY ["UI/UI.csproj", "UI/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Models/Models.csproj", "Models/"]
RUN dotnet restore "Server/Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet workload restore
RUN dotnet build "Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]