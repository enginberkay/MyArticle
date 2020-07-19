FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Article/Article.API/Content.API.csproj", "Services/Article/Article.API/"]
COPY ["Services/Article/Article.Infrastructure/Content.Infrastructure.csproj", "Services/Article/Article.Infrastructure/"]
COPY ["Services/Article/Article.Domain/Content.Domain.csproj", "Services/Article/Article.Domain/"]
RUN dotnet restore "Services/Article/Article.API/Content.API.csproj"
COPY . .
WORKDIR "/src/Services/Article/Article.API"
RUN dotnet build "Content.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Content.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Content.API.dll"]