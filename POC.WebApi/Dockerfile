FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["POC.WebApi/POC.WebApi.csproj", "POC.WebApi/"]
RUN dotnet restore "POC.WebApi/POC.WebApi.csproj"
COPY . .
WORKDIR "/src/POC.WebApi"
RUN dotnet build "POC.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "POC.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "POC.WebApi.dll"]