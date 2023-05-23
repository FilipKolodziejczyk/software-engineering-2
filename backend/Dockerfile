FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY SoftwareEngineering2/*.csproj SoftwareEngineering2/
COPY SoftwareEngineering2Test/*.csproj SoftwareEngineering2Test/
RUN dotnet restore

COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS test
WORKDIR /src/SoftwareEngineering2Test
RUN dotnet test

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoftwareEngineering2.dll"]