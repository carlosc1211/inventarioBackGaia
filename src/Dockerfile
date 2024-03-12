FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Pagos.API/Pagos.API.csproj", "Pagos.API/"]
COPY ["Pagos.Domain/Pagos.Domain.csproj", "Pagos.Domain/"]
COPY ["Pagos.Service/Pagos.Service.csproj", "Pagos.Service/"]
COPY ["Pagos.Infrastructure/Pagos.Infrastructure.csproj", "Pagos.Infrastructure/"]

ARG PAT

# download and install latest credential provider
RUN wget -qO- https://aka.ms/install-artifacts-credprovider.sh | bash

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

# Environment variable to enable seesion token cache. More on this here: https://github.com/Microsoft/artifacts-credprovider#help
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true

# Environment variable for adding endpoint credentials. More on this here: https://github.com/Microsoft/artifacts-credprovider#help
# Make sure that you *do not* hard code the "PAT" here. That is a sensitive information and must not be checked in.
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS "{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/druni/Druni/_packaging/druni/nuget/v3/index.json\", \"password\":\"${PAT}\"}]}"

RUN dotnet restore "Pagos.Domain/Pagos.Domain.csproj" -s "https://pkgs.dev.azure.com/druni/Druni/_packaging/druni/nuget/v3/index.json" -s "https://api.nuget.org/v3/index.json" 
RUN dotnet restore "Pagos.API/Pagos.API.csproj" -s "https://pkgs.dev.azure.com/druni/Druni/_packaging/druni/nuget/v3/index.json" -s "https://api.nuget.org/v3/index.json"
RUN dotnet restore "Pagos.Service/Pagos.Service.csproj" -s "https://pkgs.dev.azure.com/druni/Druni/_packaging/druni/nuget/v3/index.json" -s "https://api.nuget.org/v3/index.json" 
RUN dotnet restore "Pagos.Infrastructure/Pagos.Infrastructure.csproj" -s "https://pkgs.dev.azure.com/druni/Druni/_packaging/druni/nuget/v3/index.json" -s "https://api.nuget.org/v3/index.json"

COPY . .

WORKDIR "/src/Pagos.API"
RUN dotnet build "Pagos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pagos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pagos.API.dll"]
