$ppt = New-Object -ComObject PowerPoint.Application
try {
    $files = Get-ChildItem -Path 'Content\topic-*\slides\*.pptx'
    Write-Host "Testing all $($files.Count) PPTX files..."
    foreach ($file in $files) {
        $deck = $ppt.Presentations.Open($file.FullName, -1, 0, 0)
        Write-Host "$($file.Name): $($deck.Slides.Count) slides - OK"
        $deck.Close()
    }
    Write-Host "ALL PPTX FILES OPENED SUCCESSFULLY IN POWERPOINT!"
} catch {
    Write-Host "Error: $($_.Exception.Message)"
} finally {
    $ppt.Quit()
    [System.Runtime.InteropServices.Marshal]::ReleaseComObject($ppt) | Out-Null
}
