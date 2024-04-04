FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/UserCard.API/UserCard.API.csproj", "Services/UserCard.API/"]
RUN dotnet restore "Services/UserCard.API/UserCard.API.csproj"
COPY . .
WORKDIR "/src/Services/UserCard.API"
RUN dotnet build "UserCard.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserCard.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserCard.API.dll"]
