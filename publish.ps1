# Variables
$ProjectPath = "src\Install.App"
$OutputPath = "publish"
$Runtime = "win-x64"
$zipName = "v8.zip"
$MinioAlias = "mystore"
$MinioUrl = "https://api.storage.kztek.io.vn"
$BucketName = "deployments"
$AccessKey = "admin"
$SecretKey = "Pass123456@!"

# Save current directory
$CurrentDir = Get-Location

$fullProjectPath = Resolve-Path $ProjectPath
Set-Location $fullProjectPath

# Publish the app
dotnet publish -c Release -r $Runtime --self-contained false -o $OutputPath

# Full zip file path
$zipFile = Join-Path (Get-Location) $zipName

# Create zip
Compress-Archive -Path ".\$OutputPath\*" -DestinationPath $zipFile

Write-Host "✅ Publish complete. Zipped to $zipFile"

# Return to original directory (to find mc.exe)
Set-Location $CurrentDir

# Add MinIO alias (idempotent)
.\mc.exe alias set $MinioAlias $MinioUrl $AccessKey $SecretKey

# Upload zip to MinIO
.\mc.exe cp $zipFile "$MinioAlias/$BucketName/"

Write-Host "✅ Uploaded to MinIO: $MinioUrl/$BucketName/$zipName"

# Delete zip after upload
if (Test-Path $zipFile) {
    Remove-Item $zipFile -Force
    Write-Host "🗑️ Deleted local zip file: $zipFile"
}
