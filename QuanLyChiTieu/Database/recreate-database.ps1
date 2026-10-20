# ============================================================
# recreate-database.ps1 — TV1 sở hữu
# Chạy 1 lệnh để dựng lại QuanLyChiTieuDB về trạng thái demo "đẹp"
# (001 → 002 → 003 → 004 → 005). 004 tự xóa sạch dữ liệu cũ trước khi seed,
# nên chạy lại bao nhiêu lần cũng an toàn.
# Cách dùng (PowerShell):
#   .\Database\recreate-database.ps1                    # LocalDB mặc định
#   .\Database\recreate-database.ps1 -Server "(localdb)\MSSQLLocalDB"
# Yêu cầu: SQL Server Express LocalDB + sqlcmd nằm trong PATH (kèm theo SQL Server).
# ============================================================
param(
    [string]$Server = "(localdb)\MSSQLLocalDB",
    [switch]$Force
)

$ErrorActionPreference = "Stop"

# Script luôn DROP rồi dựng lại DB (mục đích là "recreate").
# Với quota_prompt an toàn: dùng -Force để bỏ qua câu hỏi xác nhận.
if (-not $Force) {
    $traLoi = Read-Host "Se XOA TOAN BO du lieu QuanLyChiTieuDB va dung lai tu dau. Tiep tuc? (y/n)"
    if ($traLoi -notmatch '^[yY]') {
        Write-Host "Da huy." -ForegroundColor Yellow
        exit 0
    }
}

# 0. Drop DB nếu tồn tại (kết nối còn mở sẽ bị rollback ngay để có thể drop)
Write-Host "== Drop QuanLyChiTieuDB (neu ton tai) ..." -ForegroundColor Cyan
sqlcmd -S $Server -E -C -b -W -Q "IF DB_ID('QuanLyChiTieuDB') IS NOT NULL BEGIN ALTER DATABASE QuanLyChiTieuDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE QuanLyChiTieuDB; END;"
if ($LASTEXITCODE -ne 0) { throw "Drop DB that bai. Dong app dang mo DB roi chay lai." }

$files = @(
    "001_CreateDatabase.sql",
    "002_CreateTables.sql",
    "003_CreateConstraints.sql",
    "004_SeedData.sql",
    "005_Views.sql"
)

foreach ($f in $files) {
    $path = Join-Path $PSScriptRoot $f
    if (-not (Test-Path $path)) {
        throw "Khong tim thay file: $path"
    }
    Write-Host ("== Chay {0} ..." -f $f) -ForegroundColor Cyan
    sqlcmd -S $Server -E -C -b -f 65001 -i $path
    if ($LASTEXITCODE -ne 0) {
        throw ("{0} that bai (sqlcmd exit {1}). DB chua hoan tat." -f $f, $LASTEXITCODE)
    }
}

Write-Host ""
Write-Host "OK: QuanLyChiTieuDB da reset voi du lieu demo." -ForegroundColor Green
Write-Host "Dang nhap thu: truongnhom / chitieu123"
