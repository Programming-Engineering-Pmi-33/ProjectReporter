## PROJECT REPORTER

**Start Docker**

- Path to Docker scripts - `../build/local/docker`.
- Run `environment.yml` (There is an instruction for running in file).

**Open database**

- `docker exec -it docker_project_reporter_1 mysql -udeveloper -p` - To open container (Password is required).
- `USE project_reporter;` - To open database.

**Users Service**

- `Add-Migration "Name" -Project ProjectReporter.Main.Modules -Context UsersStorage -O UsersService/Storage/Migrations` - To add migration for UsersStorage.
- `Update-Database -Project ProjectReporter.Modules -Context UsersStorage` - To apply migrations for UsersStorage.

**Groups Service**

- `Add-Migration "Name" -Project ProjectReporter.Main.Modules -Context GroupsStorage -O GroupsService/Storage/Migrations` - To add migration for GroupsStorage.
- `Update-Database -Project ProjectReporter.Modules -Context GroupsStorage` - To apply migrations for GroupsStorage.

**Service Arguments**

- `dotnet run --project ./ProjectReporter.Service/ProjectReporter.Service.csproj --update-database` - To start Service for DatabaseUpdater only.
- `dotnet run --project ./ProjectReporter.Service/ProjectReporter.Service.csproj --upload-faculties ../../../build/local/initial_data/faculties.txt` - To start Service for DatabaseUploader only.
