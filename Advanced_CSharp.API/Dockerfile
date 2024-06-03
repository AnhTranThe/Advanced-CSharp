#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Advanced_CSharp.API/Advanced_CSharp.API.csproj", "Advanced_CSharp.API/"]
COPY ["Advanced_CSharp.DTO/Advanced_CSharp.DTO.csproj", "Advanced_CSharp.DTO/"]
COPY ["Advanced_CSharp.Database/Advanced_CSharp.Database.csproj", "Advanced_CSharp.Database/"]
COPY ["Advanced_CSharp.Service/Advanced_CSharp.Service.csproj", "Advanced_CSharp.Service/"]
RUN dotnet restore "./Advanced_CSharp.API/./Advanced_CSharp.API.csproj"
COPY . .
WORKDIR "/src/Advanced_CSharp.API"
RUN dotnet build "./Advanced_CSharp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Advanced_CSharp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Advanced_CSharp.API.dll"]