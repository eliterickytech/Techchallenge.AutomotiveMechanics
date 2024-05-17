#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/1-Presentation/1.3-Web/TechChallenge.AutomotiveMechanics.Presentation.Web/TechChallenge.AutomotiveMechanics.Presentation.Web.csproj", "src/1-Presentation/1.3-Web/TechChallenge.AutomotiveMechanics.Presentation.Web/"]
RUN dotnet restore "./src/1-Presentation/1.3-Web/TechChallenge.AutomotiveMechanics.Presentation.Web/TechChallenge.AutomotiveMechanics.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/src/1-Presentation/1.3-Web/TechChallenge.AutomotiveMechanics.Presentation.Web"
RUN dotnet build "./TechChallenge.AutomotiveMechanics.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallenge.AutomotiveMechanics.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge.AutomotiveMechanics.Presentation.Web.dll"]