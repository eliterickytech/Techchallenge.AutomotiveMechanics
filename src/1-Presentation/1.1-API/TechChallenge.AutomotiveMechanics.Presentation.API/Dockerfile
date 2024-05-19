#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

version: "3.8"
services:

  rabbitmq:
    image: masstransit/rabbitmq
    ports:
      - 5672:5672
      - 15672:15672

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/1-Presentation/1.1-API/TechChallenge.AutomotiveMechanics.Presentation.API/TechChallenge.AutomotiveMechanics.Presentation.API.csproj", "src/1-Presentation/1.1-API/TechChallenge.AutomotiveMechanics.Presentation.API/"]
COPY ["src/2-Services/2.1-Business/TechChallenge.AutomotiveMechanics.Services.Business/TechChallenge.AutomotiveMechanics.Services.Business.csproj", "src/2-Services/2.1-Business/TechChallenge.AutomotiveMechanics.Services.Business/"]
COPY ["src/3-Domain/TechChallenge.AutomotiveMechanics.Domain/TechChallenge.AutomotiveMechanics.Domain.csproj", "src/3-Domain/TechChallenge.AutomotiveMechanics.Domain/"]
COPY ["src/5-Crosscutting/5.1-IOC/TechChallenge.AutomotiveMechanics.Crosscutting.Ioc/TechChallenge.AutomotiveMechanics.Crosscutting.Ioc.csproj", "src/5-Crosscutting/5.1-IOC/TechChallenge.AutomotiveMechanics.Crosscutting.Ioc/"]
COPY ["src/4-Infrastructure/4.1-Data/TechChallenge.AutomotiveMechanics.Infrastructure.Data/TechChallenge.AutomotiveMechanics.Infrastructure.Data.csproj", "src/4-Infrastructure/4.1-Data/TechChallenge.AutomotiveMechanics.Infrastructure.Data/"]
RUN dotnet restore "./src/1-Presentation/1.1-API/TechChallenge.AutomotiveMechanics.Presentation.API/./TechChallenge.AutomotiveMechanics.Presentation.API.csproj"
COPY . .
WORKDIR "/src/src/1-Presentation/1.1-API/TechChallenge.AutomotiveMechanics.Presentation.API"
RUN dotnet build "./TechChallenge.AutomotiveMechanics.Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallenge.AutomotiveMechanics.Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge.AutomotiveMechanics.Presentation.API.dll"]