$pdfPath = "C:\Users\Ndae\Documents\GitHub\CLDV6211POE\CLDV6211POE.pdf"
$outputPath = "C:\Users\Ndae\Documents\GitHub\CLDV6211POE\CLDV6211POE.txt"

$pdfContent = [System.IO.File]::ReadAllText($pdfPath)

$textContent = ""
$streamPattern = "stream\s*(.*?)\s*endstream"
$matches = [regex]::Matches($pdfContent, $streamPattern, [System.Text.RegularExpressions.RegexOptions]::Singleline)

foreach ($match in $matches) {
    $stream = $match.Groups[1].Value
    $cleanStream = $stream -replace '[^\x20-\x7E\r\n\t]', ' '
    $textContent += $cleanStream + "`n"
}

if ($textContent.Length -eq 0) {
    $lines = $pdfContent -split "`n"
    foreach ($line in $lines) {
        if ($line -match '\(([^)]+)\)' -and $line.Length -gt 10) {
            $textContent += $line + "`n"
        }
    }
}

$textContent | Out-File -FilePath $outputPath -Encoding UTF8
Write-Host "Done. Text extracted to $outputPath"
Write-Host "Content length: $($textContent.Length)"
