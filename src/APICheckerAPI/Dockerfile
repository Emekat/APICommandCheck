FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/APICheckerAPI/APICheckerAPI.csproj", "APICheckerAPI/"]
RUN dotnet restore "src/APICheckerAPI/APICheckerAPI.csproj"
COPY . .
WORKDIR "/src/APICheckerAPI"
RUN dotnet build "APICheckerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APICheckerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APICheckerAPI.dll"]
