Write-TestExpectation "Single value with foo = 5"
$foo = [PSCustomObject] @{ foo = 5; bar = 6 }
New-Observable -Value $foo | Select-ObservableObject -Property foo | Start-Observable
