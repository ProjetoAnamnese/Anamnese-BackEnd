# Etapa base de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Render usa a porta definida pela variável PORT
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Etapa de build com SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia apenas o csproj e restaura dependências
COPY ["Anamnese.API.csproj", "."]
RUN dotnet restore "Anamnese.API.csproj"

# Copia o restante do código
COPY . .

# Build do projeto
RUN dotnet build "Anamnese.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publica o projeto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Anamnese.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Anamnese.API.dll"]
