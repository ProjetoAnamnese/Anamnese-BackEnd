# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Render usa a porta da env PORT
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o .csproj e restaura
COPY ["Anamnese.API/Anamnese.API.csproj", "Anamnese.API/"]
RUN dotnet restore "Anamnese.API/Anamnese.API.csproj"

# Copia o restante do código
COPY . .
WORKDIR "/src/Anamnese.API"
RUN dotnet build "Anamnese.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publica
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Anamnese.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Anamnese.API.dll"]
