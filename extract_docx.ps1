$word = New-Object -ComObject Word.Application
$doc = $word.Documents.Open("C:\Users\Ndae\Documents\GitHub\CLDV6211POE\CLDV6211POE.docx")
$content = $doc.Content.Text
$doc.Close($false)
$word.Quit()
$content | Out-File -FilePath "C:\Users\Ndae\Documents\GitHub\CLDV6211POE\CLDV6211POE.txt" -Encoding UTF8
Write-Host "Done"
