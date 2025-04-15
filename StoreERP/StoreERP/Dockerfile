FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StoreERP/StoreERP/StoreERP.csproj", "StoreERP/StoreERP/"]
COPY ["StoreERP/StoreERP.Client/StoreERP.Client.csproj", "StoreERP/StoreERP.Client/"]
RUN dotnet restore "StoreERP/StoreERP/StoreERP.csproj"
COPY . .
WORKDIR "/src/StoreERP/StoreERP"
RUN dotnet build "StoreERP.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StoreERP.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreERP.dll"]
