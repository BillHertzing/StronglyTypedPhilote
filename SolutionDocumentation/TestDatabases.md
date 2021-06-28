# Test Database Creation and useage

This document discusses how to setup temporary databases used in some of this solution's tests.

## Select Databse(s) to setup

### Setup Microsoft SQL Server (mssql)

This Open Source Software repository could use the MS sqL Community Edition for test purposes. However, The Developer Edition has a licens that allows it ot be used for non-production databases.

#### Download the SQL Database installer

### Run the installation program

Open a Powershell prompt in Administrator mode. Navigate to the downloads directory. Double Click on teh downloaded SQL server installation file. Ensure the pre-requisites pass. Install the Database engine.
Mixed mode; sa password, and 'this user'
Maximum Memory = 2GB
maximum parallelism = 3

## Download the Powershell dbatools package

`choco install dbatools -y

## Create a database using a dbatool script

Create a new file named `tests\TestDataBase\New-TestDatabase.ps1`

See this repository for a copy of the script. Modify to fit your needs.

Run the script using an elevated prompt.

### Troubleshooting

 encountered operating system error 5(Access is denied.) while attempting to open or create the physical file. - ToDo: Figure out why it is not using 'this user' to create the files. Workaround: set permissions on the `data` and `log`direcotyr for `everyone` to `full access`

## enable TCP/IP to localhost

`sqlservermanager.msc`

ToDo: Insert jpg and detailed instructions

## Download and install Flyway

`choco install flyway.commandline -y

## Create flyway conf file

Create directory tests/
tests/TestDataBase/Flyway/conf/flyway.conf

```text
flyway.driver=com.microsoft.sqlserver.jdbc.SQLServerDriver
flyway.url=jdbc:sqlserver://localhost:1433;databaseName=StronglyTypedIdTestDatabase;integratedSecurity=true
flyway.user=whertzing
```

Troubleshoot: message from flyway that 'driver is ot confgured for integrated security', have to uee sa login

## Run Flyway against the new empty database

Run `flyway -configFiles="<path-to-flyway-conf-file>/flyway.conf" migrate`

Expect to see a lot of text, ending with the lines

```text
Successfully validated 0 migrations (execution time 00:00.018s)
WARNING: No migrations found. Are your locations set up correctly?
Creating Schema History table [StronglyTypedIdTestDatabase].[dbo].[flyway_schema_history] ...
Current version of schema [dbo]: << Empty Schema >>
Schema [dbo] is up to date. No migration necessary.
```

## Install ServiceStack ORMLight for SQLServer

Add package reference to ServiceStack ORM in the

## Install the ServiceStack SharpData tool for a UI around the test database

```powershell
  dotnet tool install -g app
```

It will install a development cert, which must be trusted..

```text
----------------
Installed an ASP.NET Core HTTPS development certificate.
To trust the certificate run 'dotnet dev-certs https --trust' (Windows and macOS only).
Learn about HTTPS: https://aka.ms/dotnet-https
----------------
```
run `dotnet dev-certs https --trust`

## Install dotnet-script

run ` dotnet tool install -g dotnet-script`

## Create c# script file

create new file under `tests/TestDatabase` called (for example) `AdHocDB.csx`

## Add the following to A1.csx

`Console.WriteLine("Hello from the Dotnet c# script interpreter");`

Save the file.

## Add a new Task to run the dotnet c# script interperter

Add the following to the `.vscocde/tasks.json file

```json
    {
      "label": "Run csx",
      "type": "shell",
      "command": "dotnet script",
      "args": [
        "${file}"
      ],
      "problemMatcher": [],
      "group": {
        "kind": "build",
        "isDefault": true
      },
      "presentation": {
        "echo": true,
        "reveal": "always",
        "focus": false,
        "panel": "dedicated",
        "showReuseMessage": true,
        "clear": false
      },
      "windows": {
        "options": {
          "shell": {
            "executable": "cmd.exe",
            "args": [
              "/d",
              "/c"
            ]
          }
        }
      }
    },
```

## Validate the dotnet script interpreter works as expected

with the cursor focus in the `AdHocDB.csx` fie, press `Ctrl-Shift-B`. You should see the message appear in the Terminal window.

## Create database fixture for ORMLite

### SQL Servewr


### MySQL


### SQLLite



## Create fixture setup (new database per test Collection)


## Create database test with transaction and rollback


## Test storage and retrieval of StrongyTypedId<TValue>


## Test storage and retrieval of  Philote<StrongyTypedId<TValue>, TValue>

## Entity Framework


### Dapper
