# Develoment setup

As a basic prequesitise one must have installed .Net Core 7.0 ([guide](https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net70)) and .Net Entity Framework Core tools ([guide](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)).

Application has several different run options for development process ([config file](SoftwareEngineering2/Properties/launchSettings.json)) described above:
- *inmemdb_http_create_and_drop_db* - Using plain HTTP. Database is created in memory and does not persists (dies when the app dies) and it's whole schema (and population) is done automatically, based on code. Therefore this is the easiest setup, since no db configuration is required. Development settings, i.e. Swagger is up and endpoints returns error messages.
- *sqlserver_http_create_and_drop_db* - Using plain HTTP and development settings. Database is running on SQL Server instance but it is recreated based on code each time app is started (old database is dropped).
- *sqlserver_https* - Still using development settings but HTTPS. Database is running on SQL Server instance and it is persisted betweeen application runs. When schema of db changes, migrations are required to be prepared (if appropriate miogration exists, it is applied during boot up).

## SQL Server local instance setup

Target platform for it will be a Docker container, thus this way is described here too. As a prerequisite, one have to install Docker on his/her machine - [Instalation Guide](https://docs.docker.com/get-docker/).

When this is provided, the following command must be executed to run the container (replace <password> and <username> with your values and write thme down for the next step):
```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -e "MSSQL_USER=<username>" -p 1433:1433 --name flowershop-sql -d mcr.microsoft.com/mssql/server:2022-latest
```

Application must have SQL credentials provided in secure manner, hence ASP.NET Core tool Safe storage is utilized in this project. It stores data on user's local machine (outside of repo). To add SQL credential the following commands may be used (replace <password> and <username> with previously used values):
```
dotnet user-secrets set "DbUser" "<username>"
dotnet user-secrets set "DbPassword" "<password>"
```
To learn more about safe store follow this [link](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0).

Docker SQL Server container may be stopped and started when needed (`run` is needed only once, assuming container won't be removed or broken):
```
docker stop flowershop-sql
docker start flowershop-sql
```

## Migrations

When handling persistent db confing, migrations have to be used. They must be also used for production data. Therefore at most one migration file should be provided for one Pull Reqeust, debbuging migrations shouldn't be commited. Moreover, create and drop API approach is prefered for development stage. To test some scenarios, populating SQL scripts should be provided.
Migration in CLI (many GUI interfaces) is done by those two commands:
```
dotnet ef migrations add <migrationName>
dotnet ef database update (not necessary since the same step is initiated on app startup if necessary)
```
