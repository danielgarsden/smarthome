# Create a build area
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the project file specifically and restore dependencies
COPY smarthome.csproj .
RUN dotnet restore ./smarthome.csproj

# Copy the rest of the source code and build it
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose the default ASP.NET port
EXPOSE 8080
ENTRYPOINT ["dotnet", "SmartHome.dll"]