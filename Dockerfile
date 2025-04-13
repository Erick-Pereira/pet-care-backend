# Use a base image for ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use a base image for .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and restore dependencies
COPY WebApi/pet-care-backend.sln ./
COPY WebApi/WebApi.csproj WebApi/
COPY BLL/BLL.csproj BLL/
COPY Commons/Commons.csproj Commons/
COPY DAL/DAL.csproj DAL/
COPY Entities/Entities.csproj Entities/
RUN dotnet restore WebApi/pet-care-backend.sln

# Copy the entire source code and publish the application
COPY . .
RUN dotnet publish WebApi/WebApi.csproj -c Release -o /app/publish

# Use the runtime image to run the application
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]

