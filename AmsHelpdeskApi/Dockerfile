# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# copia csproj primeiro
COPY *.csproj ./

RUN dotnet restore

# copia o restante
COPY . .

RUN dotnet publish -c Release -o /app/publish

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "AmsHelpdeskApi.dll"]