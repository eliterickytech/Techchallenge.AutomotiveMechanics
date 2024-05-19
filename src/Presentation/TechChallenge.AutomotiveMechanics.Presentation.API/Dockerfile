#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/TechChallenge.AutomotiveMechanics.Presentation.API/TechChallenge.AutomotiveMechanics.Presentation.API.csproj", "src/Presentation/TechChallenge.AutomotiveMechanics.Presentation.API/"]
COPY ["src/Crosscutting/TechChallenge.AutomotiveMechanics.Crosscutting.Ioc/TechChallenge.AutomotiveMechanics.Crosscutting.Ioc.csproj", "src/Crosscutting/TechChallenge.AutomotiveMechanics.Crosscutting.Ioc/"]
COPY ["src/Infrastructure/TechChallenge.AutomotiveMechanics.Infrastructure.Data/TechChallenge.AutomotiveMechanics.Infrastructure.Data.csproj", "src/Infrastructure/TechChallenge.AutomotiveMechanics.Infrastructure.Data/"]
COPY ["src/Domain/TechChallenge.AutomotiveMechanics.Domain/TechChallenge.AutomotiveMechanics.Domain.csproj", "src/Domain/TechChallenge.AutomotiveMechanics.Domain/"]
COPY ["src/Services/TechChallenge.AutomotiveMechanics.Services.Business/TechChallenge.AutomotiveMechanics.Services.Business.csproj", "src/Services/TechChallenge.AutomotiveMechanics.Services.Business/"]
COPY ["src/Crosscutting/Techchallenge.AutomotiveMechanics.Crosscutting.Shared/Techchallenge.AutomotiveMechanics.Crosscutting.Shared.csproj", "src/Crosscutting/Techchallenge.AutomotiveMechanics.Crosscutting.Shared/"]
RUN dotnet restore "./src/Presentation/TechChallenge.AutomotiveMechanics.Presentation.API/./TechChallenge.AutomotiveMechanics.Presentation.API.csproj"
COPY . .
WORKDIR "/src/src/Presentation/TechChallenge.AutomotiveMechanics.Presentation.API"
RUN dotnet build "./TechChallenge.AutomotiveMechanics.Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallenge.AutomotiveMechanics.Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge.AutomotiveMechanics.Presentation.API.dll"]