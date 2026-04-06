$pdfPath = "C:\Users\Ndae\Documents\GitHub\CLDV6211POE\CLDV6211POE.pdf"
$outputPath = "C:\Users\Ndae\Documents\GitHub\CLDV6211POE\CLDV6211POE.txt"

Add-Type -AssemblyName System.Text.Encoding

function Get-PdfText {
    param([string]$Path)
    
    $byteArray = [System.IO.File]::ReadAllBytes($Path)
    $content = [System.Text.Encoding]::ASCII.GetString($byteArray)
    
    $text = ""
    $lines = $content -split "`n"
    foreach ($line in $lines) {
        if ($line -match "^BT|^ET|^/Title|^/Author|^/Subject|^/Keywords") {
            $text += $line + "`n"
        }
    }
    return $text
}

Get-PdfText -Path $pdfPath | Out-File -FilePath $outputPath -Encoding UTF8
Write-Host "Done. Text extracted to $outputPath"
