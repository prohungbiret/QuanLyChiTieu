# ============================================================
# build.ps1 — TV1 sở hữu
# Build toàn solution bằng MSBuild. Dùng khi:
#   - Kiểm tra build trước khi push/PR (docs/05: code phải build trước commit)
#   - Kiểm tra tích hợp nhanh sau mỗi lần merge
# Cách dùng (PowerShell, chạy tại thư mục gốc repo):
#   .\build.ps1                  # Debug (mặc định)
#   .\build.ps1 -Configuration Release
# ============================================================
param(
    [string]$Configuration = "Debug",
    [string]$Target = "Rebuild"
)

$ErrorActionPreference = "Continue"

# 1. Tìm MSBuild qua vswhere (kèm theo Visual Studio)
$vswhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"
$msbuild = $null
if (Test-Path $vswhere) {
    $msbuild = (& $vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe |
        Select-Object -First 1)
}
if (-not $msbuild) {
    # 2. Fallback: MSBuild mặc định của .NET Framework
    $candidates = @(
        "${env:ProgramFiles}\Microsoft Visual Server\18\Community\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles}\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\MSBuild\14.0\Bin\MSBuild.exe"
    ) | Where-Object { Test-Path $_ } | Select-Object -First 1
    $msbuild = $candidates
}
if (-not $msbuild) {
    throw "Khong tim thay MSBuild. Hay cai Visual Studio (workload .NET desktop)."
}

Write-Host ("== MSBuild: " + $msbuild) -ForegroundColor Cyan
& $msbuild (Join-Path $PSScriptRoot "QuanLyChiTieu\QuanLyChiTieu.csproj") `
    /p:Configuration=$Configuration /t:$Target /v:minimal /nologo

if ($LASTEXITCODE -eq 0) {
    Write-Host "BUILD OK" -ForegroundColor Green
} else {
    Write-Host "BUILD THAT BAI" -ForegroundColor Red
}
exit $LASTEXITCODE
