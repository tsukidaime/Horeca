#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Horeca.Blazor/Horeca.Blazor.csproj", "src/Horeca.Blazor/"]
COPY ["src/Horeca.HttpApi.Client/Horeca.HttpApi.Client.csproj", "src/Horeca.HttpApi.Client/"]
COPY ["src/Horeca.Application.Contracts/Horeca.Application.Contracts.csproj", "src/Horeca.Application.Contracts/"]
COPY ["src/Horeca.Domain.Shared/Horeca.Domain.Shared.csproj", "src/Horeca.Domain.Shared/"]
RUN dotnet restore "src/Horeca.Blazor/Horeca.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Horeca.Blazor"
RUN dotnet build "Horeca.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Horeca.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Horeca.Blazor.dll"]
