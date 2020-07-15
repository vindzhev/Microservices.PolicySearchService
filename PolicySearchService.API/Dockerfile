#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PolicySearchService/PolicySearchService.API/PolicySearchService.API.csproj", "PolicySearchService/PolicySearchService.API/"]
COPY ["PolicySearchService/PolicySearchService.Infrastructure/PolicySearchService.Infrastructure.csproj", "PolicySearchService/PolicySearchService.Infrastructure/"]
COPY ["PolicySearchService/PolicySearchService.Application/PolicySearchService.Application.csproj", "PolicySearchService/PolicySearchService.Application/"]
COPY ["PolicySearchService/PolicySearchService.Domain/PolicySearchService.Domain.csproj", "PolicySearchService/PolicySearchService.Domain/"]
RUN dotnet restore "PolicySearchService/PolicySearchService.API/PolicySearchService.API.csproj"
COPY . .
WORKDIR "/src/PolicySearchService/PolicySearchService.API"
RUN dotnet build "PolicySearchService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PolicySearchService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PolicySearchService.API.dll"]
