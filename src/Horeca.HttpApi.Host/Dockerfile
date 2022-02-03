#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Horeca.HttpApi.Host/Horeca.HttpApi.Host.csproj", "src/Horeca.HttpApi.Host/"]
COPY ["src/Horeca.HttpApi/Horeca.HttpApi.csproj", "src/Horeca.HttpApi/"]
COPY ["src/Horeca.Application.Contracts/Horeca.Application.Contracts.csproj", "src/Horeca.Application.Contracts/"]
COPY ["src/Horeca.Domain.Shared/Horeca.Domain.Shared.csproj", "src/Horeca.Domain.Shared/"]
COPY ["src/Horeca.EntityFrameworkCore/Horeca.EntityFrameworkCore.csproj", "src/Horeca.EntityFrameworkCore/"]
COPY ["src/Horeca.Domain/Horeca.Domain.csproj", "src/Horeca.Domain/"]
COPY ["src/Horeca.Application/Horeca.Application.csproj", "src/Horeca.Application/"]
RUN dotnet restore "src/Horeca.HttpApi.Host/Horeca.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Horeca.HttpApi.Host"
RUN dotnet build "Horeca.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Horeca.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Horeca.HttpApi.Host.dll"]