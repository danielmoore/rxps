Write-TestExpectation "Empty line"
New-Observable -Value $null | Start-Observable

Write-TestExpectation "1 - 10"
New-Observable -Min 1 -Max 10 | Start-Observable
