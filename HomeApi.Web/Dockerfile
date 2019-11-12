FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["HomeApi.Web/HomeApi.Web.csproj", "HomeApi.Web/"]
RUN dotnet restore "HomeApi.Web/HomeApi.Web.csproj"
COPY . .
WORKDIR "/src/HomeApi.Web"
RUN dotnet build "HomeApi.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeApi.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeApi.Web.dll"]