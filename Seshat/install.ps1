$ErrorActionPreference="Stop"

$MonoModPath="$(Get-Location)\MonoMod.exe"
$OldPwd=Get-Location

# lor bindings
$LorBinaryPath=$args[0]

if (-not($LorBinaryPath)) {
    $LorBinaryPath="C:\Program Files (x86)\Steam\steamapps\common\Library Of Ruina\LibraryOfRuina_Data\Managed\Assembly-CSharp.dll"
    Write-Host "No path specified; using default path:"
    Write-Host "    $LorBinaryPath"
}

$LorBinaryBasePath=Split-Path $LorBinaryPath
$LorBinaryFilename=Split-Path $LorBinaryPath -Leaf

# seshat bindings
$SeshatBinaryPath=$args[1]

if (-not($SeshatBinaryPath)) { $SeshatBinaryPath="$(Get-Location)\Seshat.dll" }

$SeshatLorModPath="$($LorBinaryFilename.Substring(0, $LorBinaryFilename.Length - 4)).mm.dll"

# make sure that this is a dll we are working with!
if (-not($LorBinaryPath -match ".dll$")) {
    Write-Host "Binary must be a .dll file!" -ForegroundColor Red

    exit 1
}

$LorBinaryBackupPath="$LorBinaryPath.bak"

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

Copy-Item $SeshatBinaryPath -Destination "$LorBinaryBasePath\$SeshatLorModPath"

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
        Write-Output "Cleaning up $_..." 
        Remove-Item "$LorBinaryBasePath\$_" 
    }
}

Write-Output "Cleaning up $SeshatLorModPath..."
Remove-Item $SeshatLorModPath

# finished!

Write-Host "Done!"

Set-Location -Path $OldPwd