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

# Run Flyway against the new empty database




