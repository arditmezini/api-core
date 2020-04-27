#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AspNetCoreApi.Api/AspNetCoreApi.Api.csproj", "AspNetCoreApi.Api/"]
COPY ["AspNetCoreApi.Service/AspNetCoreApi.Service.csproj", "AspNetCoreApi.Service/"]
COPY ["AspNetCoreApi.Data/AspNetCoreApi.Dal.csproj", "AspNetCoreApi.Data/"]
COPY ["AspNetCoreApi.Models/AspNetCoreApi.Models.csproj", "AspNetCoreApi.Models/"]
COPY ["AspNetCoreApi.Common/AspNetCoreApi.Common.csproj", "AspNetCoreApi.Common/"]
RUN dotnet restore "AspNetCoreApi.Api/AspNetCoreApi.Api.csproj"
COPY . .
WORKDIR "/src/AspNetCoreApi.Api"
RUN dotnet build "AspNetCoreApi.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreApi.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreApi.Api.dll"]