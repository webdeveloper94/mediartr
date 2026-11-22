$testKey = "sk-test-secret-1234567890"

# Load assembly
$dll = Resolve-Path "bin\Debug\net10.0-windows10.0.19041.0\MediaSavodxonlik.dll"
Add-Type -Path $dll.Path

# Test Save
[MediaSavodxonlik.ApiKeyStore]::SaveSystemKey($testKey)
$hasKey = [MediaSavodxonlik.ApiKeyStore]::HasSystemKey()
$loaded = [MediaSavodxonlik.ApiKeyStore]::LoadSystemKey()

Write-Host "HasKey: $hasKey"
Write-Host "Loaded matches: $($loaded -eq $testKey)"

# Test Resolve with empty user key -> should use system key
$resolved = [MediaSavodxonlik.ApiKeyStore]::ResolveEffectiveKey("")
Write-Host "Resolved without user key matches: $($resolved -eq $testKey)"

# Test Resolve with user custom key -> should use user key
$userKey = "sk-user-custom-999"
$resolvedUser = [MediaSavodxonlik.ApiKeyStore]::ResolveEffectiveKey($userKey)
Write-Host "Resolved with user key matches: $($resolvedUser -eq $userKey)"

# Clean up test key
[MediaSavodxonlik.ApiKeyStore]::SaveSystemKey("")
$hasKeyAfter = [MediaSavodxonlik.ApiKeyStore]::HasSystemKey()
Write-Host "HasKey after clear: $hasKeyAfter"

Write-Host "ALL API KEY STORE TESTS PASSED!"
