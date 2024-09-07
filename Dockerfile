# Etapa 1: Compilação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build -o out

# Etapa 2: Publicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build-env /app/out .

COPY --from=build-env /app/appsettings.json ./

COPY --from=build-env /app/wait-for-it.sh ./

RUN chmod +x wait-for-it.sh

RUN ls -R

EXPOSE 5000

CMD ["./wait-for-it.sh", "sql-server:1433", "--", "dotnet", "LivrariaApi.dll" ]