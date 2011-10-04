Write-TestExpectation "Single value with foo = 5"
$foo = New-Object psobject -Property @{ foo = 5; bar = 6 }
New-Observable -Value $foo | Select-ObservableObject -Property foo | Start-Observable
