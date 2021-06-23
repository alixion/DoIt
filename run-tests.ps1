# Taken from psake https://github.com/psake/psake
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


Push-Location -Path ./tests/DoIt.DomainTests
exec {& dotnet test}

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
#    exec { & dotnet build DoIt.sln -c Release --version-suffix=$buildSuffix -v q /nologo }
    
    Write-Host "Publish DbUp Console App"
    Write-Host "========================"
    # Todo: set runtime depending on where it runs
    # Does not run with --no-build
    exec { & dotnet publish ./src/DoIt.DbMigrator/DoIt.DbMigrator.csproj -r win-x64 -c Release --nologo -o ./DbUp /p:PublishSingleFile=true }
    
    Write-Host "Running DbUp Sql Scripts"
    Write-Host "========================"
    exec {& ./DbUp/DoIt.DbMigrator.exe "Host=localhost;Database=DoItDb;Uid=postgres;Pwd=Parola.1" "src/DoIt.DbMigrator/Sql/" drop-if-exists}
    
    Write-Host "Running Application IntegrationTests"
    Write-Host "===================================="

    $env:ConnectionStrings:DefaultConnection='Host=localhost;Database=DoItDb;Uid=postgres;Pwd=Parola.1'
    Push-Location -Path ./tests/DoIt.Application.IntegrationTests
    exec {& dotnet test}
}
finally
{
    Pop-Location
    Write-Host "Finalizing docker containers"
    Write-Host "============================="
    exec { & docker-compose down --remove-orphans}
}