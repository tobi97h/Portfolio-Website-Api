#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PortfolioApi.csproj", "PortfolioApi/"]
RUN dotnet restore "PortfolioApi/PortfolioApi.csproj"
WORKDIR "/src/PortfolioApi"
COPY . .
RUN dotnet build "PortfolioApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PortfolioApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN apt update -y && apt install git -y
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortfolioApi.dll"]