Write-TestExpectation "1 - 5"
1 .. 5 | ConvertTo-Observable | Start-Observable

Wtite-TestExpection "a, 3, @{ foo = bar }"
'a', 3, @{ foo = 'bar' } | ConvertTo-Observable | Start-Observable

