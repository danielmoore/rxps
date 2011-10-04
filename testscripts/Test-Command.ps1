param([Parameter(Position=0, Mandatory=$true)][string]$name)

function Write-TestExpectation([Parameter(Position=0, Mandatory=$true)][string]$messsage) {
    Write-Host "${messsage}:" -ForegroundColor Green
}

$testDir = split-path $MyInvocation.MyCommand.Path
ipmo "$testDir/../rxps/bin/debug/rxps.dll" -Force -DisableNameChecking
. "$testDir\Test-$($name.Remove($name.IndexOf('-'), 1)).ps1"

Write-Host "-- End of Test --" -Foreground Gray
