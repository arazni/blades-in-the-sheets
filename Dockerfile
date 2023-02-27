# This docker file uses multi-stage build strategy
# to ensure optimal image build times and sizes
# End result container image requires .NET runtime,
# however creating it requires .NET SDK.
#FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
#WORKDIR /src
#
#FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
#WORKDIR /src
#COPY . .
#RUN dotnet restore
#RUN dotnet build -c Release -o /out
#
#FROM build AS publish
#RUN dotnet publish -c Release -o /out
#
## Building final image used in running container
#FROM base AS final
#RUN apk update \
    #&& apk add unzip procps
#WORKDIR /src
#COPY --from=publish /out .
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet Server.dll
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet UI.dll

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Server/Server.csproj", "Server/"]
RUN dotnet restore "Server/Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]