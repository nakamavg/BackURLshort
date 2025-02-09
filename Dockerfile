# Fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia el archivo del proyecto
COPY *.csproj ./
RUN dotnet restore

# Copia el resto del código
COPY . ./
RUN dotnet publish -c Release -o out

# Fase de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "UrlShorteners.dll"]