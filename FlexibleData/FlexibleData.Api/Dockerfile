#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FlexibleData.Api/FlexibleData.Api.csproj", "FlexibleData.Api/"]
COPY ["FlexibleData.Application/FlexibleData.Application.csproj", "FlexibleData.Application/"]
COPY ["FlexibleData.Domain/FlexibleData.Domain.csproj", "FlexibleData.Domain/"]
COPY ["FlexibleData.Persistance/FlexibleData.Persistence.csproj", "FlexibleData.Persistance/"]
RUN dotnet restore "FlexibleData.Api/FlexibleData.Api.csproj"
COPY . .
WORKDIR "/src/FlexibleData.Api"
RUN dotnet build "FlexibleData.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlexibleData.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlexibleData.Api.dll"]