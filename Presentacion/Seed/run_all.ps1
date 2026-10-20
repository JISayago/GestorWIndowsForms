# QueryData3.0 - carga seed (NO hace wipe total)
# Flujo: LIMPIAR_DB_TOTAL -> abrir app 1 vez -> este script
param(
  [string]$Server   = ".\SQLEXPRESS",
  [string]$Database = "EjemploBase",
  [switch]$SkipClean
)
$ErrorActionPreference = "Stop"
$root = $PSScriptRoot
Write-Host "QueryData3.0 | Server=$Server Database=$Database" -ForegroundColor Yellow
sqlcmd -S $Server -E -b -Q "IF DB_ID(N'$Database') IS NULL CREATE DATABASE [$Database];" | Out-Host
function Invoke-SqlFile($file) {
  Write-Host ">> $file" -ForegroundColor Cyan
  sqlcmd -S $Server -d $Database -E -b -i (Join-Path $root $file) | Out-Host
  if ($LASTEXITCODE -ne 0) { throw "Fallo $file (exit $LASTEXITCODE)" }
}
if (-not $SkipClean) { Invoke-SqlFile "LIMPIAR_DB_PEGAR.sql" }
Invoke-SqlFile "00_catalogo.sql"
Invoke-SqlFile "meses\2024-08.sql"
Invoke-SqlFile "meses\2024-09.sql"
Invoke-SqlFile "meses\2024-10.sql"
Invoke-SqlFile "meses\2024-11.sql"
Invoke-SqlFile "meses\2024-12.sql"
Invoke-SqlFile "meses\2025-01.sql"
Invoke-SqlFile "meses\2025-02.sql"
Invoke-SqlFile "meses\2025-03.sql"
Invoke-SqlFile "meses\2025-04.sql"
Invoke-SqlFile "meses\2025-05.sql"
Invoke-SqlFile "meses\2025-06.sql"
Invoke-SqlFile "meses\2025-07.sql"
Invoke-SqlFile "meses\2025-08.sql"
Invoke-SqlFile "meses\2025-09.sql"
Invoke-SqlFile "meses\2025-10.sql"
Invoke-SqlFile "meses\2025-11.sql"
Invoke-SqlFile "meses\2025-12.sql"
Invoke-SqlFile "meses\2026-01.sql"
Invoke-SqlFile "meses\2026-02.sql"
Invoke-SqlFile "meses\2026-03.sql"
Invoke-SqlFile "meses\2026-04.sql"
Invoke-SqlFile "meses\2026-05.sql"
Invoke-SqlFile "meses\2026-06.sql"
Invoke-SqlFile "meses\2026-07.sql"
Invoke-SqlFile "meses\2026-08.sql"
Invoke-SqlFile "98_ctacte_final.sql"
Invoke-SqlFile "97_demo_presentacion.sql"
Invoke-SqlFile "99_validaciones.sql"
Write-Host "Carga QueryData3.0 OK" -ForegroundColor Green
