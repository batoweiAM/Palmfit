#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 85
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR "/Palmfit"
COPY ["Palmfit.Api/Palmfit.Api.csproj", "Palmfit.Api/"]
COPY ["Palmfit.Core/Palmfit.Core.csproj", "Palmfit.Core/"]
COPY ["Palmfit.Data/Palmfit.Data.csproj", "Palmfit.Data/"]
COPY ["Palmfit.Infrastructure/Palmfit.Infrastructure.csproj", "Palmfit.Infrastructure/"]
RUN dotnet restore "Palmfit.Api/Palmfit.Api.csproj"
COPY . .
WORKDIR "/Palmfit/Palmfit.Api"
RUN dotnet build "Palmfit.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Palmfit.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR "/Palmfit/Palmfit.Api"
COPY --from=publish /app/publish .
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet Palmfit.Api.dll
ENTRYPOINT ["dotnet", "Palmfit.Api.dll"]