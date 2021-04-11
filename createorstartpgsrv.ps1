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



Write-Host "Resetting postgres container"
Write-Host "==========================="
exec {& docker rm --force pgsrv }
exec {& docker run --name pgsrv -p 5432:5432 -e POSTGRES_PASSWORD=Parola.1 -d postgres }

Write-Host "Publish DbUp Console Project"
Write-Host "============================"
exec { & dotnet publish ./src/CleanApi.DbMigrator/CleanApi.DbMigrator.csproj -r win-x64 -c Release --nologo -o ./DbUp /p:PublishSingleFile=true }

# Write-Host "Waiting 10 sec"
# Start-Sleep 10

Write-Host "Restoring database"
Write-Host "==========================="

exec {& ./DbUp/CleanApi.DbMigrator.exe "Host=localhost;Database=CleanApiDb;Uid=postgres;Pwd=Parola.1" "src/CleanApi.DbMigrator/Sql/" drop-if-exists}
Write-Host
Write-Host "Done!"