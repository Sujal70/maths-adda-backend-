# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY src/MathsAdda.Common/*.csproj ./MathsAdda.Common/
COPY src/MathsAdda.Data/*.csproj ./MathsAdda.Data/
COPY src/MathsAdda.Business/*.csproj ./MathsAdda.Business/
COPY src/MathsAdda.Api/*.csproj ./MathsAdda.Api/

# Restore packages
RUN dotnet restore ./MathsAdda.Api/MathsAdda.Api.csproj

# Copy all source files
COPY src/ .

# Publish the application
RUN dotnet publish ./MathsAdda.Api/MathsAdda.Api.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "MathsAdda.Api.dll"]