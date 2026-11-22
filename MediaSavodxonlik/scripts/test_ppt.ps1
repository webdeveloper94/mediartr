$ppt = New-Object -ComObject PowerPoint.Application
try {
    $item = Get-Item 'Content\topic-01\slides\1-mavzu.pptx'
    Write-Host "Opening: $($item.FullName)"
    $deck = $ppt.Presentations.Open($item.FullName, -1, 0, 0)
    Write-Host "Slides count: $($deck.Slides.Count)"
    $deck.Close()
} catch {
    Write-Host "Error: $($_.Exception.Message)"
} finally {
    $ppt.Quit()
    [System.Runtime.Interopservices.Marshal]::ReleaseComObject($ppt) | Out-Null
}
