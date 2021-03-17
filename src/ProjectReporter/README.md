## PROJECT REPORTER

**Start Docker**
* Path to Docker scripts - `../build/local/docker`.
* Run `environment.yml` (There is an instruction for running in file).

**Users Service**
* `Add-Migration "Name" -Project ProjectReporter.Main.Modules -Context UsersStorage -O UsersService/Storage/Migrations` - To add migration for UsersStorage.
* `Update-Database -Project ProjectReporter.Modules -Context UsersStorage` - To apply migrations for UsersStorage.


**Groups Service**
* `Add-Migration "Name" -Project ProjectReporter.Main.Modules -Context GroupsStorage -O GroupsService/Storage/Migrations` - To add migration for GroupsStorage.
* `Update-Database -Project ProjectReporter.Modules -Context GroupsStorage` - To apply migrations for GroupsStorage.


**Service Arguments**
* `dotnet run --project ./ProjectReporter.Service/ProjectReporter.Service.csproj --update-database` - To start Service for DatabaseUpdater only.