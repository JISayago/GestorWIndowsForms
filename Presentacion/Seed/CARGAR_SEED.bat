@echo off
setlocal
cd /d "%~dp0"
set SERVER=.\SQLEXPRESS
set DATABASE=EjemploBase
echo QueryData3.0 - CARGAR SEED
echo Servidor: %SERVER%  Base: %DATABASE%
echo Flujo: LIMPIAR_DB_TOTAL -> app 1 vez -> este script
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0run_all.ps1" -Server "%SERVER%" -Database "%DATABASE%"
set ERR=%ERRORLEVEL%
if %ERR% neq 0 (echo ERROR: carga fallo) else (echo OK: carga finalizada)
pause
exit /b %ERR%
