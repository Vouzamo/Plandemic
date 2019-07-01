FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base

RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
    apt-get install -y build-essential nodejs

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build-dotnet
ENV BuildingDоcker true
WORKDIR /src
COPY ["Plandemic.App/Plandemic.App.csproj", "Plandemic.App/"]
RUN dotnet restore "Plandemic.App/Plandemic.App.csproj"
COPY . .
WORKDIR "/src/Plandemic.App"

RUN dotnet build "Plandemic.App.csproj" -c Release -o /app

FROM build-dotnet AS publish
RUN dotnet publish "Plandemic.App.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Plandemic.App.dll"]