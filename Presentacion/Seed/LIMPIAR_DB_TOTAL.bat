@echo off
setlocal
cd /d "%~dp0"
set SERVER=.\SQLEXPRESS
set DATABASE=EjemploBase
echo QueryData3.0 - WIPE TOTAL
echo Servidor: %SERVER%  Base: %DATABASE%
choice /C SN /M "Continuar con el wipe total"
if errorlevel 2 exit /b 0
sqlcmd -S "%SERVER%" -d "%DATABASE%" -E -b -i "%~dp0LIMPIAR_DB_TOTAL.sql"
set ERR=%ERRORLEVEL%
if %ERR% neq 0 (echo ERROR: wipe fallo) else (echo OK: wipe total finalizado)
pause
exit /b %ERR%
