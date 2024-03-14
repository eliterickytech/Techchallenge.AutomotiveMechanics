#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
version: "3.8"
services:

  rabbitmq:
    image: masstransit/rabbitmq
    ports:
      - 5672:5672
      - 15672:15672

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/7-Consumer/7.1-Consumer/TechChallenge.AutomotiveMechanics.Consumer/TechChallenge.AutomotiveMechanics.Consumer.csproj", "src/7-Consumer/7.1-Consumer/TechChallenge.AutomotiveMechanics.Consumer/"]
RUN dotnet restore "src/7-Consumer/7.1-Consumer/TechChallenge.AutomotiveMechanics.Consumer/TechChallenge.AutomotiveMechanics.Consumer.csproj"
COPY . .
WORKDIR "/src/src/7-Consumer/7.1-Consumer/TechChallenge.AutomotiveMechanics.Consumer"
RUN dotnet build "TechChallenge.AutomotiveMechanics.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechChallenge.AutomotiveMechanics.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge.AutomotiveMechanics.Consumer.dll"]