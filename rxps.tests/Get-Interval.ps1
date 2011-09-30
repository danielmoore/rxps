ipmo "$(Split-Path $MyInvocation.MyCommand.Path)/../rxps/bin/debug/rxps.dll" -Force
Get-Interval 3 | Invoke-Stream
