#############################################################################
#region New-TestDatabase
<#
.SYNOPSIS
Create a new SQL Server Database using dbatools library, apply the Flyway migrations to the new database
.DESCRIPTION
Sets up the values for a single database within the instance, to be used in the UnitTests for StronglyTypedIds repository. Most Unit Tests will be run within a transaction, and the datbase will rollback to a pristine state.
The pristine state is defined by the flyway migrations whihc prepoulate the never-changing data
.PARAMETER SqlInstance
The ServerName\InstanceName pair. Defaults to 'localhost\MSSQLSERVER'
.PARAMETER Name
The name of the new Database, typically will be the name of the repository followed by `UnitTestsDatabase`
.PARAMETER Owner
The User that owns the database schema. Defaults to 'sa'
.PARAMETER DataFilePath
No Default, if none provided, defaults to the value in use by the SQL Server instance
.PARAMETER PrimaryFilesize
The appropriate size for a dataase to be used in a CI pipieline for unit and integration tests for the projects in the repository. Defaults to 1024
.PARAMETER PrimaryFileGrowth
Defaults to 256
.PARAMETER LogFilePath
No Default, if none provided, defaults to the value in use by the SQL Server instance
.PARAMETER LogSize
Defaults to 512
.PARAMETER LogGrowth
Defaults to 128
.PARAMETER Recoverymodel
Defaults to 'simple'
.OUTPUTS
ToDo: write Help For the function's outputs
.EXAMPLE
New-TestDatabase -Name StronglyTypedIdTestDatabase
.EXAMPLE
New-TestDatabase -Name StronglyTypedIdTestDatabase -SqlInstance 'localhost\MSSQLSERVER'
.EXAMPLE
ToDo: write Help For example 2 of using this function
.ATTRIBUTION
ToDo: write text describing the ideas and codes that are attributed to others
.LINK
[DBA Tools documentation]()
.LINK
ToDo: insert link to internet articles that contributed ideas / code used in this function e.g. http://www.somewhere.com/attribution.html
.SCM
ToDo: insert SCM keywords markers that are automatically inserted <Configuration Management Keywords>
#>
Function New-TestDatabase {
  #region FunctionParameters
  [CmdletBinding(SupportsShouldProcess = $true)]
  param (
    [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [string] $SqlInstance
    , [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [string] $Name
    , [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [string] $Owner
    , [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [string] $DataFilePath
    , [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [int] $PrimaryFilesize
    , [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [int] $PrimaryFileGrowth
    , [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [string] $LogFilePath
    , [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [int] $LogSize
    , [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [int] $LogGrowth
    , [parameter(ValueFromPipeline = $True, ValueFromPipelineByPropertyName = $True)]
    [string] $Recoverymodel
  )
  #endregion FunctionParameters
  #region FunctionBeginBlock
  ########################################
  BEGIN {
    Write-Verbose -Message "Starting $($MyInvocation.Mycommand)"

    # default values for settings
    $settings = @{
      SqlInstance       = 'localhost\MSSQLSERVER'            # SQL Server name
      Name              = 'StronglyTypedIdTestDatabase'      # database name
      Owner             = 'sa'                               # database owner
      DataFilePath      = Join-Path $DropBoxBaseDir 'whertzing\GitHub\StronglyTypedPhilote\tests\TestDataBase\SQLData' # data file path
      PrimaryFilesize   = 1024                                                             # data file initial size
      PrimaryFileGrowth = 256                                                            # data file autrogrowth amount
      LogFilePath       = Join-Path $DropBoxBaseDir 'whertzing\GitHub\StronglyTypedPhilote\tests\TestDataBase\SQLLog'  # log file path
      LogSize           = 512                                                                      # data file initial size
      LogGrowth         = 128                                                                    # data file autrogrowth amount
      Recoverymodel     = 'Simple'                                                           # recovery model
    }

    # Things to be initialized after settings are processed
    if ($SqlInstance) { $Settings.SqlInstance = $SqlInstance }
    if ($Name) { $Settings.Name = $Name }
    if ($Owner) { $Settings.Owner = $Owner }
    if ($DataFilePath) { $Settings.DataFilePath = $DataFilePath }
    if ($PrimaryFilesize) { $Settings.PrimaryFilesize = $PrimaryFilesize }
    if ($PrimaryFileGrowth) { $Settings.PrimaryFileGrowth = $PrimaryFileGrowth }
    if ($LogFilePath) { $Settings.LogFilePath = $LogFilePath }
    if ($LogSize) { $Settings.LogSize = $LogSize }
    if ($LogGrowth) { $Settings.LogGrowth = $LogGrowth }
    if ($Recoverymodel) { $Settings.Recoverymodel = $Recoverymodel }
  }
  #endregion FunctionBeginBlock

  #region FunctionProcessBlock
  ########################################
  PROCESS {
    # This function does not expect to be a middle part of a pipeline
  }
  #endregion FunctionProcessBlock

  #region FunctionEndBlock
  ########################################
  END {
    if ($PSCmdlet.ShouldProcess("$Settings.Name", "New-DbaDatabase -SqlInstance $($Settings.SqlInstance) -Name $($Settings.Name) -Owner $($Settings.Owner) -DataFilePath $Settings.DataFilePath -Recoverymodel $Settings.Recoverymodel -PrimaryFilesize $Settings.PrimaryFilesize -PrimaryFileGrowth $Settings.PrimaryFileGrowth -LogFilePath $Settings.LogFilePath -LogSize $Settings.LogSize -LogGrowth $Settings.LogGrowth -Recoverymodel $($Settings.Recoverymodel) -WhatIf: $WhatIfPreference -Verbose: $Verbosepreference")) {
      $stdouterr = New-DbaDatabase -SqlInstance $Settings.SqlInstance -Name $Settings.Name -Owner $Settings.Owner -DataFilePath $Settings.DataFilePath -RecoveryModel $Settings.Recoverymodel -PrimaryFilesize $Settings.PrimaryFilesize -PrimaryFileGrowth $Settings.PrimaryFileGrowth -LogFilePath $Settings.LogFilePath -LogSize $Settings.LogSize -LogGrowth $Settings.LogGrowth -RecoveryModel $Settings.Recoverymodel -WhatIf:$WhatIfPreference -Verbose:$Verbosepreference
    }
    Write-Verbose -Message $stdouterr
    Write-Verbose -Message "Ending $($MyInvocation.Mycommand)"
  }
  #endregion FunctionEndBlock
}
#endregion FunctionName
#############################################################################




