FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SoftwareEnginnering2/SoftwareEnginnering2.csproj", "SoftwareEnginnering2/"]
RUN dotnet restore "SoftwareEnginnering2/SoftwareEnginnering2.csproj"
COPY . .
WORKDIR "/src/SoftwareEnginnering2"
RUN dotnet build "SoftwareEnginnering2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoftwareEnginnering2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoftwareEnginnering2.dll"]
