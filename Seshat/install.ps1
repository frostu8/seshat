$MonoModPath="$(Get-Location)\MonoMod.exe"
$OldPwd=Get-Location

# lor bindings
$LorBinaryPath=$args[0]
$LorBinaryBasePath=Split-Path $LorBinaryPath
$LorBinaryFilename=Split-Path $LorBinaryPath -Leaf

# seshat bindings
$SeshatBinaryPath=$args[1]

if (-not($SeshatBinaryPath)) { $SeshatBinaryPath="$(Get-Location)\Seshat.dll" }

$LorBinaryBackupPath="$LorBinaryPath.bak"

# make sure that this is a dll we are working with!
if (-not($LorBinaryPath -match ".dll$")) {
    Write-Host "Binary must be a .dll file!" -ForegroundColor Red

    exit 1
}

# Undo backup

if (Test-Path -Path $LorBinaryBackupPath) {
    Write-Host "Rolling back game files..."

    Copy-Item $LorBinaryBackupPath -Destination $LorBinaryPath
}

# Make backup

Write-Host "Making backup of binary @ $(Split-Path $LorBinaryBackupPath -Leaf)..."

Copy-Item $LorBinaryPath -Destination $LorBinaryBackupPath

# Copy resulting binary to directory

Write-Host "Copying Seshat binary to $LorBinaryBasePath..."


Copy-Item $SeshatBinaryPath -Destination "$LorBinaryBasePath\$SeshatMonoModFilename"

# Write modification

Set-Location -Path $LorBinaryBasePath

Write-Host "Making modification to binary $LorBinaryFilename..."

& $MonoModPath $LorBinaryFilename

# Copy new modded file

Write-Host "Copying new modded file..."

$LorBinaryModdedFilename="MONOMODDED_$LorBinaryFilename"

Copy-Item "$LorBinaryBasePath\$LorBinaryModdedFilename" -Destination $LorBinaryPath

# Clean up (delete modded files)

Get-ChildItem -Path $LorBinaryBasePath -File | ForEach-Object -Process {
    if ($_ -match "^MONOMODDED_") {
        Write-Output "Cleaning up $_" 
        Remove-Item "$LorBinaryBasePath\$_" 
    }
}

# finished!

Write-Host "Done!"

Set-Location -Path $OldPwd