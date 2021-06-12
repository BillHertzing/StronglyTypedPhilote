$SqlInstance = 'localhost\MSSQLSERVER'                                                   # SQL Server name
$Name = 'StronglyTypedIdTestDatabase'                                                                # database name
$DataFilePath = Join-path $DropBoxBaseDir 'whertzing\GitHub\StronglyTypedPhilote\tests\TestDataBase\SQLData' # data file path
$LogFilePath = Join-path $DropBoxBaseDir 'whertzing\GitHub\StronglyTypedPhilote\tests\TestDataBase\SQLLog'  # log file path
$Recoverymodel = 'Simple'                                                           # recovery model
$Owner = 'sa'                                                                       # database owner
$PrimaryFilesize = 1024                                                             # data file initial size
$PrimaryFileGrowth = 256                                                            # data file autrogrowth amount
$LogSize = 512                                                                      # data file initial size
$LogGrowth = 128                                                                    # data file autrogrowth amount

New-DbaDatabase -SqlInstance $SqlInstance -Name $Name -DataFilePath $DataFilePath -LogFilePath $LogFilePath -Recoverymodel $Recoverymodel -Owner $Owner -PrimaryFilesize $PrimaryFilesize -PrimaryFileGrowth $PrimaryFileGrowth -LogSize $LogSize -LogGrowth $LogGrowth | Out-Null

