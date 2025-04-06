FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN ls -la /src # Depuração para verificar o conteúdo do diretório
RUN ls -la /src/WebApi
RUN dotnet restore /src/WebApi/pet-care-backend.sln

RUN dotnet publish /src/WebApi/WebApi.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]

