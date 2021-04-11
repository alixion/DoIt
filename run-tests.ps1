# Taken from psake https://github.com/psake/psake

<#
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

Write-Host "Starting postgres container"
Write-Host "==========================="
exec { & docker-compose up -d}


try
{
#    Write-Host "Restoring solution"
#    Write-Host "=================="
#    Exec {& dotnet restore }
#    
#    $suffix = "-ci-local"
#    $commitHash = $(git rev-parse --short HEAD)
#    $buildSuffix = "$($suffix)-$($commitHash)"
#    
#    Write-Host "Building solution. Version suffix is $buildSuffix"
#    Write-Host "================================================="
#    exec { & dotnet build CleanApi.sln -c Release --version-suffix=$buildSuffix -v q /nologo }
    
    Write-Host "Publish DbUp Console App"
    Write-Host "========================"
    # Todo: set runtime depending on where it runs
    # Does not run with --no-build
    exec { & dotnet publish ./src/CleanApi.DbMigrator/CleanApi.DbMigrator.csproj -r win-x64 -c Release --nologo -o ./DbUp /p:PublishSingleFile=true }
    
    Write-Host "Running DbUp Sql Scripts"
    Write-Host "========================"
    exec {& ./DbUp/CleanApi.DbMigrator.exe "Host=localhost;Database=CleanApiDb;Uid=postgres;Pwd=Parola.1" "src/CleanApi.DbMigrator/Sql/" drop-if-exists}
    
    Write-Host "Running Application IntegrationTests"
    Write-Host "===================================="

    $env:ConnectionStrings:DefaultConnection='Host=localhost;Database=CleanApiDb;Uid=postgres;Pwd=Parola.1'
    Push-Location -Path ./tests/CleanApi.Application.IntegrationTests
    exec {& dotnet test}
}
finally
{
    Pop-Location
    Write-Host "Finalizing docker containers"
    Write-Host "============================="
    exec { & docker-compose down --remove-orphans}
}