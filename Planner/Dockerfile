# Use a imagem base do .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Planner.csproj", "."]
RUN dotnet restore "Planner.csproj"
COPY . .
RUN dotnet build "Planner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Planner.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Planner.dll"]
